using System.Linq;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterAction
{
    public static class CharacterActionContextExtensions
    {
        public static CharacterActionContext AsCharacterActionContext(this GameContext game)
        {
            return new CharacterActionContext(game);
        }
    }

    public struct CharacterActionContext
    {
        GameContext game;

        public CharacterActionContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterActionEntity? CurrentAction
        {
            get
            {
                return game
                    .GetGroup(GameMatcher.CharacterAction)
                    .GetEntities()
                    .FirstOrDefault(e => !e.isCharacterAction_Cancel && !e.isCharacterAction_Finish)
                    ?.AsCharacterAction(game);
            }
        }

        public CharacterActionEntity CreateAction(int characterId, CharacterActionType actionType)
        {
            Assert.IsTrue(CurrentAction == null);
            var entity = game.CreateEntity();
            entity.AddCharacterAction(
                newCharacterId: characterId,
                newType: actionType
            );
            return entity.AsCharacterAction(game);
        }
    }

}
