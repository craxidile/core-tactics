using Taiga.Core.Character.Placement;
using UnityEngine;

namespace Taiga.Core.CharacterSequence
{

    public static class CharacterSequenceEntity_DamagedExtensions
    {
        public static CharacterSequenceEntity_Damaged AsCharacterSequence_Damaged(this IGameScopedEntity entity)
        {
            return new CharacterSequenceEntity_Damaged(entity.context, entity.entity);
        }

        public static CharacterSequenceEntity_Damaged AsCharacterSequence_Damaged(this GameEntity entity, GameContext context)
        {
            return new CharacterSequenceEntity_Damaged(context, entity);
        }

        public static bool IsDamaged(this CharacterSequenceEntity characterSequence)
        {
            return characterSequence.entity.hasCharacterSequence_Damaged;
        }
    }

    public struct CharacterSequenceEntity_Damaged : IGameScopedEntity, ICharacterSequenceEntity_DamageSource
    {
        public CharacterSequenceEntity_Damaged(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public MapDirection BumpDirection => entity.characterSequence_Damaged.bumpDirection;

        public Vector2Int? EndPosition
        {

            get
            {
                if (!entity.hasCharacterSequence_DamagedEndPosition)
                {
                    return null;
                }

                return entity.characterSequence_DamagedEndPosition.value;
            }
            set
            {
                if (value == null)
                {
                    if (entity.hasCharacterSequence_DamagedEndPosition)
                    {
                        entity.RemoveCharacterSequence_DamagedEndPosition();
                    }
                }

                entity.ReplaceCharacterSequence_DamagedEndPosition(value.Value);
            }
        }

        public Vector2Int? BumpPosition
        {

            get
            {
                if (!entity.hasCharacterSequence_DamagedBumpPosition)
                {
                    return null;
                }

                return entity.characterSequence_DamagedBumpPosition.value;
            }
            set
            {
                if (value == null)
                {
                    if (entity.hasCharacterSequence_DamagedBumpPosition)
                    {
                        entity.RemoveCharacterSequence_DamagedBumpPosition();
                    }
                }

                entity.ReplaceCharacterSequence_DamagedBumpPosition(value.Value);
            }
        }

        public int Damage => entity
            .characterSequence_Damaged
            .damage;

        public CharacterSequenceEntity SourceSequence => entity
            .characterSequence_SourceSequenceEntity
            .value
            .AsCharacterSequence(context);

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
