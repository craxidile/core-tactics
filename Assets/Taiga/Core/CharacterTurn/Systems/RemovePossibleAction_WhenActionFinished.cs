using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.CharacterTurn
{
    internal class RemovePossibleAction_WhenActionFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public RemovePossibleAction_WhenActionFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterTurn = game
                .AsCharacterTurnContext()
                .CurrentCharacterTurn
                .Value;
            var characterContext = game
                .AsCharacterContext();
            var turnEntity = characterTurn.entity;

            foreach (var entity in entities)
            {
                var action = entity.AsCharacterAction(game).ActionType;
                var character = characterContext.GetCharacter(characterTurn.CharacterId);
                var characterAttack = character.AsCharacter_Attack();

                var sameTeamCharacterPositions = characterContext
                    .GetCharactersByPlayerId(character.AsCharacter().OwnerPlayerId)
                    .Select(c => c.AsCharacter_Placement().Position);

                CharacterActionType[] newPossibleActions;
                if (action == CharacterActionType.SpecialAttack || action == CharacterActionType.Attack)
                {
                    newPossibleActions = characterTurn
                        .PossibleActions
                        .Where(possibleAction => possibleAction != CharacterActionType.Move)
                        .Where(possibleAction => possibleAction != CharacterActionType.SpecialAttack)
                        .Where(possibleAction => possibleAction != CharacterActionType.Attack)
                        .ToArray();
                }
                else
                {
                    var possibleActions = new List<CharacterActionType>
                    {
                        CharacterActionType.Attack,
                        CharacterActionType.SpecialAttack,
                        CharacterActionType.EndTurn
                    };
                    // TODO: Remvoe this later
                    // if (characterAttack.CanUseSpecialAttack)
                    // {
                    //     possibleActions.Add(CharacterActionType.SpecialAttack);
                    // }

                    // var newPossibleActionsEnum = characterTurn
                    //     .PossibleActions
                    //     .Where(possibleAction => possibleAction != action);

                    // var selection = characterAttack.CreateAttackStrategy().Selection;
                    // var specialSelection = characterAttack.CreateSpecialAttackStrategy().Selection;
                    // if (selection.SelectablePositions.Count == 0)
                    // {
                    //     possibleActions.Remove(CharacterActionType.Attack);
                    // }
                    // if (specialSelection.SelectablePositions.Count == 0)
                    // {
                    //     possibleActions.Remove(CharacterActionType.SpecialAttack);
                    // }

                    var selection = characterAttack.CreateAttackStrategy().Selection;
                    var selectablePositions = selection
                        .SelectablePositions
                        .Where(sp => !sameTeamCharacterPositions.Any(cp => sp.x == cp.x && sp.y == cp.y));

                    var specialSelection = characterAttack.CreateSpecialAttackStrategy().Selection;
                    var specialSelectablePositions = specialSelection
                        .SelectablePositions
                        .Where(sp => !sameTeamCharacterPositions.Any(cp => sp.x == cp.x && sp.y == cp.y));


                    if (selectablePositions.Count() == 0)
                    {
                        possibleActions.Remove(CharacterActionType.Attack);
                    }

                    if (specialSelectablePositions.Count() == 0)
                    {
                        possibleActions.Remove(CharacterActionType.SpecialAttack);
                    }

                    newPossibleActions = possibleActions.ToArray();
                }

                turnEntity.ReplaceCharacterTurn_PossibleActions(
                    newActions: newPossibleActions
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionFinished);
        }

    }

}
