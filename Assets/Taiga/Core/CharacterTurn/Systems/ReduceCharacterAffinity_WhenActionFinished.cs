using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.CharacterAction;
using UnityEngine;

namespace Taiga.Core.CharacterTurn
{
    internal class ReduceCharacterAffinity_WhenActionFinished : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public ReduceCharacterAffinity_WhenActionFinished(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var preset = game.GetProvider<ICharacterTurnPreset>();
            var characterContext = game.AsCharacterContext();
            foreach (var entity in entities)
            {
                var action = entity.AsCharacterAction(game);
                var actionType = action.ActionType;
                var affinityUsage = preset.GetAffinityUsage(actionType);

                var characterEntity = characterContext
                    .GetCharacter(action.CharacterId)
                    .Value
                    .entity;

                var currentAffinity = characterEntity
                    .character_TurnAffinity
                    .value;

                var newAffinity = currentAffinity - affinityUsage;
                newAffinity = Mathf.Max(newAffinity, 0);
                characterEntity.ReplaceCharacter_TurnAffinity(newAffinity);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterActionEvents.OnActionFinished);
        }

    }

}
