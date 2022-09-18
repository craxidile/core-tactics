using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterAction;

namespace Taiga.Core.Unity.CharacterActions
{
    public class ClearAskingActions_WhenActionCreated : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ClearAskingActions_WhenActionCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterAction = entity
                    .AsCharacterAction(game);

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterAction.CharacterId);

                var characterActionsPresenter = character
                    .AsGameObject()
                    .GetComponent<CharacterActionsPresenter>();

                characterActionsPresenter.ClearAsking();

                CharacterActionsPanel.Instance?.ClearAsking();
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionCreated);
        }
    }
}


//using System.Collections.Generic;
//using Entitas;
//using Taiga.Core.Character;
//using Taiga.Core.CharacterAction;
//using Taiga.Core.CharacterTurn;
//using Taiga.Core.Player;

//namespace Taiga.Core.Unity.CharacterActions
//{

//    public class CharacterActionsSystems : Feature
//    {
//        public CharacterActionsSystems(Contexts contexts)
//        {
//            Add(new ShowPossibleActions_WhenPossibleActionsValidAndNoActions(contexts));
//        }
//    }

//    public class ShowPossibleActions_WhenPossibleActionsValidAndNoActions : ReactiveSystem<GameEntity>
//    {
//        GameContext game;

//        public ShowPossibleActions_WhenPossibleActionsValidAndNoActions(Contexts contexts): base(contexts.game)
//        {
//            this.game = contexts.game;
//        }

//        protected override void Execute(List<GameEntity> entities)
//        {
//            foreach(var entity in entities)
//            {
//                var characterAction = entity.AsCharacterAction(game);
//                var character = game.AsCharacterContext().GetCharacter(characterAction.CharacterId);
//                var characterActionsPresenter = character
//                    .AsGameObject()
//                    .GetComponent<CharacterActionsPresenter>();
//                characterActionsPresenter.Clear
//            }
//            //var turnContext = game.AsCharacterTurnContext();
//            //var characterTurn = turnContext.CurrentCharacterTurn;
//            ////if (characterTurn == null)
//            ////{

//            ////}

//            //var characterId = characterTurn.CharacterId;
//            //var actionContext = game.AsCharacterActionContext();
//            //var possibleActions = characterTurn.PossibleActions;
//            //var character = game
//            //    .AsCharacterContext()
//            //    .GetCharacter(characterTurn.CharacterId);
//            //var characterGameObject = character.AsGameObject();
//            //var characterActionsPresenter = characterGameObject.GetComponent<CharacterActionsPresenter>();

//            //if (characterTurn == null)
//            //{
//            //    return false;
//            //}

//            //var characterAction = game
//            //    .AsCharacterActionContext()
//            //    .CurrentAction;

//            //if (characterAction != null)
//            //{
//            //    characterActionsPresenter.AskForActions(
//            //        possibleActions,
//            //        callback: (action) =>
//            //        {
//            //            actionContext.CreateAction(characterId, actionType: action);
//            //        }
//            //    );
//            //}


//            //var characterGameObject = character.AsGameObject();
//            //var characterActionsPresenter = characterGameObject.GetComponent<CharacterActionsPresenter>();

//        }

//        protected override bool Filter(GameEntity entity) => true;

//        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
//        {
//            return context.CreateCollector(
//              CharacterActionEvents.OnActionExecuted.Added()
//            );
//        }
//    }

//}
