using Taiga.Core.Character.Attack;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{

    public static class CharacterSequenceEntity_AttackExtensions
    {
        public static CharacterSequenceEntity_Attack AsCharacterSequence_Attack(this IGameScopedEntity entity)
        {
            return new CharacterSequenceEntity_Attack(entity.context, entity.entity);
        }

        public static CharacterSequenceEntity_Attack AsCharacterSequence_Attack(this GameEntity entity, GameContext context)
        {
            return new CharacterSequenceEntity_Attack(context, entity);
        }

        public static bool IsAttack(this CharacterSequenceEntity characterSequence)
        {
            return characterSequence.entity.hasCharacterSequence_Attack;
        }
    }

    public struct CharacterSequenceEntity_Attack : IGameScopedEntity, ICharacterSequenceEntity_DamageSource
    {
        public CharacterSequenceEntity_Attack(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public AttackType AttackType => entity.characterSequence_Attack.attackType;

        public MapDirection Direction => entity.characterSequence_Attack.direction;

        public CharacterSequenceEntity_Damaged CreateDamaged(
            int characterId,
            int damage,
            MapDirection bumpDirection
        )
        {
            var newSequenceEntity = context.CreateEntity();
            newSequenceEntity.AddCharacterSequence(
                newCharacterId: characterId
            );

            newSequenceEntity.AddCharacterSequence_Damaged(
                newDamage: damage,
                newBumpDirection: bumpDirection
            );

            newSequenceEntity.AddCharacterSequence_SourceSequenceEntity(entity);

            return newSequenceEntity.AsCharacterSequence_Damaged(context);
        }

        public CharacterSequenceEntity_Blocked CreateBlocked(
            int characterId,
            MapDirection bumpDirection
        )
        {
            var newSequenceEntity = context.CreateEntity();
            newSequenceEntity.AddCharacterSequence(
                newCharacterId: characterId
            );

            newSequenceEntity.AddCharacterSequence_Block(
                newBumpDirection: bumpDirection
            );

            newSequenceEntity.AddCharacterSequence_SourceSequenceEntity(entity);

            return newSequenceEntity.AsCharacterSequence_Blocked(context);
        }
    }
}
