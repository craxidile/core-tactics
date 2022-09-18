using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class CreateAttackStrategy_WhenActionCreated : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public CreateAttackStrategy_WhenActionCreated(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var action = entity
                    .AsCharacterAction(game);

                var character = game
                    .AsCharacterContext()
                    .GetCharacter(action.CharacterId);

                var characterAttack = character
                    .AsCharacter_Attack();

                var actionType = action.ActionType;
                IAttackStrategy characterAttackStrategy;
                AttackType characterAttackType;

                if (actionType == CharacterActionType.SpecialAttack)
                {
                    characterAttackType = entity.characterAction_SpecialAttackReady.specialAttackType;
                    characterAttackStrategy = characterAttack.GetSpecialAttackStrategy(characterAttackType) ??
                                              characterAttack.CreateSpecialAttackStrategy(characterAttackType);
                    Debug.Log(
                        $">>special_attack_strategy<< 1{characterAttack.GetSpecialAttackStrategy(characterAttackType)} 2{characterAttackStrategy}");
                    // characterAttackType = characterAttack.SpecialAttackType;
                }
                else
                {
                    characterAttackStrategy = characterAttack
                        .CreateAttackStrategy();
                    characterAttackType = characterAttack.AttackType;
                }

                action.entity.ReplaceCharacterAction_AttackType(
                    characterAttackType
                );

                action.entity.ReplaceCharacterAction_AttackStrategy(
                    characterAttackStrategy
                );
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            var actionType = entity.AsCharacterAction(game).ActionType;
            return actionType == CharacterActionType.Attack ||
                   (actionType == CharacterActionType.SpecialAttack && entity.hasCharacterAction_SpecialAttackReady);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return new Collector<GameEntity>(
                new[]
                {
                    context.GetGroup(CharacterActionEvents.OnActionCreated),
                    context.GetGroup(CharacterActionAttackEvents.OnSpecialAttackReady)
                },
                new[]
                {
                    GroupEvent.Added,
                    GroupEvent.Added
                }
            );
            // return context.CreateCollector(
            //     GameMatcher.AnyOf(
            //         CharacterActionEvents.OnActionCreated,
            //         CharacterActionAttackEvents.OnSpecialAttackReady
            //     )
            // );
        }
    }
}