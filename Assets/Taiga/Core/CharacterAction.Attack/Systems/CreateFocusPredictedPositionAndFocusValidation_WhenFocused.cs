using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreateFocusPredictedPositionAndFocusValidation_WhenFocused : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateFocusPredictedPositionAndFocusValidation_WhenFocused(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {

            // Debug.Log(">>predicted<<");
            foreach (var entity in entities)
            {

                if (!entity.hasCharacterAction_AttackStrategySelection) continue;

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

                    // TODO: Fix this later
                    // (selection as IAttackStrategy_DirectionSelection)
                    //     .Select(direction);
                    (selection as IAttackStrategy_DirectionSelection)
                        .Select(direction);
                }

                entity.ReplaceCharacterAction_AttackStrategySelection_FocusPredictedPositions(
                    selection.SelectedPositions
                );

                entity.isCharacterAction_AttackStrategySelection_FocusValid = selection.IsSelectedPositionsValid;
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.CharacterAction_AttackStrategySelection_Focus
            );
        }

    }
}
