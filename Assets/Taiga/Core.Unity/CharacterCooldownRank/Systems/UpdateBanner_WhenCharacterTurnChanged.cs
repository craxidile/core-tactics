using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Health;
using Taiga.Core.CharacterTurn;

namespace Taiga.Core.Unity.CharacterCooldownRank
{
    public class UpdateCooldownRank_WhenCharacterOrderChanged : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public UpdateCooldownRank_WhenCharacterOrderChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var presenter = CharacterCooldownRankPresenter.Instance;
            var characterContext = game.AsCharacterContext();
            var rankItems = game.AsCharacterTurnContext()
                .CharacterIdsOrder
                .Select(characterId =>
                    {
                        var character = characterContext
                            .GetCharacter(characterId)
                            .Value;

                        var affinity = character
                            .AsCharacter_Turn()
                            .TurnAffinity;

                        var characterHealth = character.AsCharacter_Health();
                        return new CharacterCooldownRankItem
                        {
                            id = characterId,
                            architypeId = character.ArchitypeId,
                            name = character.Name,
                            cooldown = affinity,
                            isLocalPlayer = character.IsLocalPlayer,
                            group = character.Group,
                            level = character.Level,
                            maxHealth = characterHealth.MaxHealth,
                            health = characterHealth.Health,
                        };
                    }
                );
            presenter.SetItems(rankItems);
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnGameCharacterTurnOrder);
        }
    }

}
