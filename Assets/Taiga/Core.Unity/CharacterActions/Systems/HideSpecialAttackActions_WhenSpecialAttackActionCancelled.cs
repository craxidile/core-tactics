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
    public class HideSpecialAttackActions_WhenSpecialAttackActionCancelled : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public HideSpecialAttackActions_WhenSpecialAttackActionCancelled(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                SpecialAttacksPanel.Instance?.ClearAsking();
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var actionType = entity.AsCharacterAction(game).ActionType;
            return actionType == CharacterActionType.SpecialAttack && !entity.hasCharacterAction_SpecialAttackReady;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionCanceled);
        }
    }
}
