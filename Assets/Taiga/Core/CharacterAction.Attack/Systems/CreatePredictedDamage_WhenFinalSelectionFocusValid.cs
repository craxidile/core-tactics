using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreatePredictedDamage_WhenFinalSelectionFocusValid : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreatePredictedDamage_WhenFinalSelectionFocusValid(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var selection = entity
                    .characterAction_AttackStrategySelection
                    .value;

                if (selection is IAttackStrategy_PositionSelection)
                {
                    var position = entity
                        .characterAction_AttackStrategySelection_Focus
                        .position
                        .Value;

                    (selection as IAttackStrategy_PositionSelection)
                        .Select(position);
                }

                if (selection is IAttackStrategy_DirectionSelection)
                {
                    var direction = entity
                        .characterAction_AttackStrategySelection_Focus
                        .direction
                        .Value;

                    (selection as IAttackStrategy_DirectionSelection)
                        .Select(direction);
                }

                var finalSelection = selection as IAttackStrategy_FinalSelection;
                entity.ReplaceCharacterAction_AttackPrediction(
                    newAttackDirection: finalSelection.AttackDirection,
                    newDamageInfos: finalSelection.CharacterDamageInfos
                );
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .characterAction_AttackStrategySelection
            .value is IAttackStrategy_FinalSelection;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AllOf(
                    GameMatcher.CharacterAction_AttackStrategySelection_Focus,
                    GameMatcher.CharacterAction_AttackStrategySelection_FocusValid
                )
            );
        }

    }
}
