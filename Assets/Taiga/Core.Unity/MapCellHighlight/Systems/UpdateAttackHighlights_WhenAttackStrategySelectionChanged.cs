using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapCellHighlight
{

    internal class UpdateAttackHighlights_WhenAttackStrategySelectionChanged : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public UpdateAttackHighlights_WhenAttackStrategySelectionChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            var placement = game.AsCharacterPlacementContext();
            var turn = game.AsCharacterTurnContext();
            var currentCharacter = characterContext
                .GetCharacter(turn.CurrentCharacterTurn.Value.CharacterId)
                .Value;

            foreach (var entity in entities)
            {
                var preset = game.GetProvider<IMapCellHighlightPreset>();

                var highlightEntities = game
                    .GetGroup(GameMatcher.MapCell_Highlight)
                    .GetEntities();

                foreach (var highlightEntity in highlightEntities)
                {
                    highlightEntity.RemoveMapCell_Highlight();
                }

                var mapContext = game.AsMapContext();
                var attackAction = entity.AsCharacterAction_Attack(game);
                var previousSelectionPositions = attackAction.PreviousSelectionSelectedPositions;
                var color = preset.GetColor(
                    HighlightMode.PreAttack,
                    highlight: false
                );

                foreach (var position in previousSelectionPositions)
                {
                    var mapCell = mapContext
                        .GetMapCell(position);
                    Debug.Log(">>position_not_to_render<< " + position);
                    mapCell.entity.ReplaceMapCell_Highlight(color);
                }

                var possiblePositions = attackAction.SelectionPossiblePositions;
                color = preset.GetColor(
                    HighlightMode.Attack,
                    highlight: false
                );
                foreach (var position in possiblePositions)
                {
                    var character = placement.GetCharacter(position);
                    var characterExists = character != null &&
                        character.Value.OwnerPlayerId != currentCharacter.OwnerPlayerId;

                    var mapCell = mapContext.GetMapCell(position);
                    // TODO: Fix this
                    mapCell.entity.ReplaceMapCell_Highlight(Color.clear);

                    // if (!characterExists)
                    // {
                    //     mapCell.entity.ReplaceMapCell_Highlight(Color.clear);
                    // }
                    // else if (!mapCell.entity.hasMapCell_Highlight)
                    // {
                    //     mapCell.entity.ReplaceMapCell_Highlight(color);
                    // }
                }
            }

            // TODO: Fix this
            // var map = game.AsMapContext();
            // var cell = map
            //     .GetMapCell(currentCharacter.AsCharacter_Placement().Position);
            //
            // Debug.Log($">>map_cell<< ${cell}");
            // cell.entity.ReplaceMapCell_Highlight(Color.red);
        }

        protected override bool Filter(GameEntity entity)
        {
            var actionType = entity
                .AsCharacterAction(game)
                .ActionType;

            return actionType == CharacterActionType.Attack ||
                actionType == CharacterActionType.SpecialAttack;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                CharacterActionAttackEvents.OnAttackStrategySelection.Added(),
                CharacterActionAttackEvents.OnAttackStrategyPreviousSelectionPositions.Added()
            );
        }
    }

}
