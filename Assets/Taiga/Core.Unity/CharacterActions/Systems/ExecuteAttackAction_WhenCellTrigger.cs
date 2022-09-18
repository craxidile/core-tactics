using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.Unity.CharacterBanner;
using Taiga.Core.Unity.MapInput;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterActions
{
    public class ExecuteAttackAction_WhenCellTrigger : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ExecuteAttackAction_WhenCellTrigger(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var actionContext = game.AsCharacterActionContext();
            var action = actionContext.CurrentAction.Value;
            var actionAttack = action.AsCharacterAction_Attack();
            if (actionAttack.HasNextSelection)
            {
                actionAttack.NextSelection();
            }
            else
            {
                var attacker = game
                    .AsCharacterContext()
                    .GetCharacter(action.CharacterId)
                    .Value;
                var attackerInfo = GetCharacterInfo(attacker);

                var defender = GetDefender(actionAttack);
                var defenderInfo = GetCharacterInfo(defender);

                var hitChancePercentage = 0.68f;
                PreviewAttackPanel.Instance.Show(action, attackerInfo, defenderInfo, hitChancePercentage);
            }
        }

        private CharacterInfoData GetCharacterInfo(CharacterEntity characterEntity)
        {
            var bannerPreset = Contexts.sharedInstance.GetProvider<ICharacterBannerPreset>();
            var characterHealth = characterEntity.AsCharacter_Health();
            var characterAttack = characterEntity.AsCharacter_Attack();
            var characterSprite = bannerPreset.GetPreviewAttackBannerSprite(characterEntity.ArchitypeId);
            Debug.Log($">>preview_panel<< {PreviewAttackPanel.Instance}");
            var characterBgSprite = characterEntity.IsLocalPlayer ? PreviewAttackPanel.Instance.LocalPlayerSprite : PreviewAttackPanel.Instance.OpponentSprite;
            var characterInfo = new CharacterInfoData(characterEntity.ArchitypeId, characterSprite, characterEntity.Level, characterHealth.Health, characterHealth.MaxHealth, characterBgSprite, characterAttack);
            return characterInfo;
        }

        private CharacterEntity GetDefender(CharacterActionEntity_Attack actionAttack)
        {
            var possiblePositions = actionAttack.SelectionPossiblePositions;
            var placement = game.AsCharacterPlacementContext();
            var selectionFocus = actionAttack.entity.characterAction_AttackStrategySelection_Focus;
            var focusedPosition = selectionFocus.position;
            var defender = placement.GetCharacter(focusedPosition.Value).Value;
            return defender;
        }

        protected override bool Filter(GameEntity entity)
        {
            var action = game
                .AsCharacterActionContext()
                .CurrentAction;

            if (action == null)
            {
                return false;
            }

            var attackAction = action
                .AsCharacterAction_Attack();

            if (!attackAction.IsCurretSelectedValid)
            {
                return false;
            }

            var actionType = action.Value.ActionType;
            return actionType == CharacterActionType.Attack ||
                actionType == CharacterActionType.SpecialAttack;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                MapInputEvents.OnCellTrigger
            );
        }
    }
}
