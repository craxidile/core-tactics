using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Character.Attack;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taiga.Core.CharacterSequence
{
    public static class CharacterSequenceContextExtensions
    {
        public static CharacterSequenceContext AsCharacterSequenceContext(this GameContext game)
        {
            return new CharacterSequenceContext(game);
        }
    }

    public struct CharacterSequenceContext
    {
        GameContext game;

        public CharacterSequenceContext(GameContext game)
        {
            this.game = game;
        }

        public CharacterSequenceEntity? InitialSequence => game.characterSequence_InitialEntity?.AsCharacterSequence(game);

        public CharacterSequenceEntity_Move CreateInitialMove(int characterId, IEnumerable<Movement> movementSteps)
        {
            var entity = CreateIntitialSequence(characterId);
            entity.AddCharacterSequence_Move(
                newMovementSteps: movementSteps
            );
            return entity.AsCharacterSequence_Move(game);
        }

        public CharacterSequenceEntity_Attack CreateInitialAttack(int characterId, AttackType attackType, MapDirection direction)
        {
            var entity = CreateIntitialSequence(characterId);
            entity.AddCharacterSequence_Attack(
                newAttackType: attackType,
                newDirection: direction
            );
            return entity.AsCharacterSequence_Attack(game);
        }

        GameEntity CreateIntitialSequence(int characterId)
        {
            Assert.IsTrue(InitialSequence == null);
            var entity = game.CreateEntity();
            entity.isCharacterSequence_Initial = true;
            entity.AddCharacterSequence(characterId);
            return entity;
        }

        public IEnumerable<CharacterSequenceEntity> Sequences
        {
            get
            {
                var game = this.game;
                return game
                    .GetGroup(GameMatcher.CharacterSequence)
                    .GetEntities()
                    .Select(e => e.AsCharacterSequence(game));
            }
        }

        public bool IsAllSequencesCommited => game
            .GetGroup(GameMatcher.CharacterSequence)
            .GetEntities()
            .All(e => e.isCharacterSequence_Commit);

    }

}
