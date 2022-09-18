using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{

    internal class UpdateAttackHighlightsFocus_WhenAttackStrategySelectionFocusChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateAttackHighlightsFocus_WhenAttackStrategySelectionFocusChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var preset = game.GetProvider<IMapCellHighlightPreset>();

            var characterContext = game.AsCharacterContext();
            var placement = game.AsCharacterPlacementContext();
            var turn = game.AsCharacterTurnContext();
            var currentCharacter = characterContext
                .GetCharacter(turn.CurrentCharacterTurn.Value.CharacterId)
                .Value;

            foreach (var entity in entities)
            {
                var highlightEntities = game
                    .GetGroup(GameMatcher.MapCell_OverrideHighlight)
                    .GetEntities();

                foreach (var highlightEntity in highlightEntities)
                {
                    if (highlightEntity.hasMapCell_OverrideHighlight)
                    {
                        highlightEntity.RemoveMapCell_OverrideHighlight();
                    }
                }

                var mapContext = game.AsMapContext();
                var attackAction = entity.AsCharacterAction_Attack(game);

                if (!attackAction.entity.hasCharacterAction_AttackStrategySelection_Focus) continue;
                
                var selectionFocus = attackAction.entity.characterAction_AttackStrategySelection_Focus;
                var characterPosition = currentCharacter.AsCharacter_Placement().Position;
                var focusPosition = selectionFocus.position;
                
                // EDIT: Pond
                // var focusedPosition = selectionFocus.position;
                // var diff = (focusedPosition - characterPosition).Value;
                // var focusPosition = characterPosition + (Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.y))
                //     .GetFineMapDirection()
                //     .GetUnitVector();

                if (!attackAction.IsFocus)
                {
                    return;
                }

                if (!attackAction.HasSelectionFocusPredictedPositions)
                {
                    return;
                }

                var predictedPositions = attackAction.SelectionFocusPredictedPositions;
                var color = preset.GetColor(HighlightMode.Attack, highlight: true);
                foreach (var position in predictedPositions)
                {
                    var character = placement.GetCharacter(position);
                    var characterExists = character != null &&
                        character.Value.OwnerPlayerId != currentCharacter.OwnerPlayerId;

                    var mapCell = mapContext.GetMapCell(position);
                    mapCell.entity.ReplaceMapCell_OverrideHighlight(color, position == focusPosition);
                }
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterActionAttackEvents.OnAttackStrategyFocus.AddedOrRemoved()
            );
        }
    }
}
