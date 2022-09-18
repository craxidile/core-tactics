using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;
using Taiga.Core.Player;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{
    internal class CreatePossibleActions_WhenCharacterTurnCreated : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreatePossibleActions_WhenCharacterTurnCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var characterId = entity.characterTurn.characterId;
                var character = characterContext.GetCharacter(characterId);
                var characterAttack = character.AsCharacter_Attack();

                var sameTeamCharacterPositions = characterContext
                    .GetCharactersByPlayerId(character.AsCharacter().OwnerPlayerId)
                    .Select(c => c.AsCharacter_Placement().Position);

                var possibleActions = new List<CharacterActionType>
                {
                    CharacterActionType.Move,
                    CharacterActionType.Attack,
                    CharacterActionType.SpecialAttack,
                    CharacterActionType.EndTurn
                };

                Debug.Log(">>create_possible_actions<<");

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

                // TODO: Special Attack
                // if (characterAttack.CanUseSpecialAttack)
                // {
                //     possibleActions.Add(CharacterActionType.SpecialAttack);
                // }

                entity.AddCharacterTurn_PossibleActions(
                    newActions: possibleActions.ToArray()
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurn);
        }

    }

}
