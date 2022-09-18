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
    public class ShowAskingActions_WhenPossibleActionsChanged : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ShowAskingActions_WhenPossibleActionsChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var actionContext = game.AsCharacterActionContext();
            foreach (var entity in entities)
            {
                var characterTurn = entity.AsCharacterTurn(game);
                var characterId = characterTurn.CharacterId;
                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterId);

                var characterActionsPresenter = character
                    .AsGameObject()
                    .GetComponent<CharacterActionsPresenter>();

                characterActionsPresenter.AskForActions(
                    characterTurn.PossibleActions,
                    callback: (action) =>
                    {
                        actionContext.CreateAction(characterId, actionType: action);
                    }
                );

                var architypeId = character.AsCharacter().ArchitypeId;
                var photo = game.GetProvider<ICharacterBannerPreset>()
                    .GetCharacterPhotoSprite(architypeId);
                var title = game.GetProvider<ICharacterBannerPreset>()
                    .GetCharacterTitleSprite(architypeId);

                CharacterActionsPanel.Instance?.AskForActions(
                    characterTurn.PossibleActions,
                    callback: (action) =>
                    {
                        actionContext.CreateAction(characterId, actionType: action);
                    }
                );
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurnPossibleActions);
        }
    }
}
