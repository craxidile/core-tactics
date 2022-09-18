using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Unity.CharacterBanner;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{
    public class ShowSpecialAttackActions_WhenSpecialAttackActionSelected : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ShowSpecialAttackActions_WhenSpecialAttackActionSelected(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {

            var bannerPreset = Contexts.sharedInstance.GetProvider<ICharacterBannerPreset>();

            var characterTurn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn
                .Value;
            var characterContext = game
                .AsCharacterContext();

            var character = characterContext.GetCharacter(characterTurn.CharacterId);
            var architypeId = character.Value.ArchitypeId;
            var characterAttack = character.AsCharacter_Attack();

            foreach (var entity in entities)
            {
                SpecialAttacksPanel.Instance?.AskForActions(
                    architypeId: architypeId,
                    allAttackTypes: characterAttack.AllSpecialAttackTypes,
                    eligibleAttackTypes: characterAttack.EligibleSpecialAttackTypes,
                    callback: (attackType) =>
                    {
                        Debug.Log($">>attack_type<< {attackType}");
                        entity.ReplaceCharacterAction_SpecialAttackReady(attackType);
                    }
                );
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var actionType = entity.AsCharacterAction(game).ActionType;
            return actionType == CharacterActionType.SpecialAttack && !entity.hasCharacterAction_SpecialAttackReady;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionCreated);
        }
    }
}
