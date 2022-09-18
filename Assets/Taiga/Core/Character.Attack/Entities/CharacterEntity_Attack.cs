using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterFactory;
using Taiga.Core.Unity.CharacterAnimation.Base;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Assertions;
using Assembly = System.Reflection.Assembly;

namespace Taiga.Core.Character.Attack
{
    public static class CharacterEntity_AttackExtensions
    {
        public static CharacterEntity_Attack AsCharacter_Attack(this IGameScopedEntity entity)
        {
            return new CharacterEntity_Attack(entity.context, entity.entity);
        }

        public static CharacterEntity_Attack AsCharacter_Attack(this GameEntity entity, GameContext context)
        {
            return new CharacterEntity_Attack(context, entity);
        }
    }

    public struct CharacterEntity_Attack : IGameScopedEntity
    {
        public CharacterEntity_Attack(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public AttackType AttackType => entity.character_AttackProperty.attackType;

        public int Attack => entity.character_AttackProperty.attack;

        public int Defend => entity.character_AttackProperty.defend;

        public int Accuracy => entity.character_AttackProperty.accuracy;

        public int Evasion => entity.character_AttackProperty.evasion;

        public int Critical => entity.character_AttackProperty.critical;

        public bool HasSpecialAttack => entity.hasCharacter_SpecialAttackProperty;

        public int SpecialAttackPoint => entity.character_SpecialAttackPoint.value;

        public AttackType SpecialAttackType => entity.character_SpecialAttackProperty.attackType;

        public int SpecialAttack => entity.character_SpecialAttackProperty.attack;

        public void SetupProperty(
            AttackType attackType,
            int attack,
            int defend,
            int accuracy,
            int evasion,
            int critical
        )
        {
            entity.AddCharacter_AttackProperty(
                newAttackType: attackType,
                newAttack: attack,
                newDefend: defend,
                newAccuracy: accuracy,
                newEvasion: evasion,
                newCritical: critical
            );
        }

        public IAttackStrategy CreateAttackStrategy()
        {
            return context
                .GetProvider<IAttackStrategyPreset>()
                .CreateStrategy(
                    attackType: AttackType,
                    attackPoint: Attack,
                    character: this.AsCharacter()
                );
        }

        public void SetupSpecialAttackProperty(
            AttackType attackType,
            int attack,
            int specialAttackUnitUsage,
            int maxSpecialAttackUnit,
            Dictionary<AttackType, SpecialAttackControllers> attackControllers,
            List<SpecialAttackUnitBound?> specialAttackUnitBounds
        )
        {
            entity.AddCharacter_SpecialAttackProperty(
                newAttackType: attackType,
                newAttack: attack,
                newSpecialAttackUnitUsage: specialAttackUnitUsage,
                newMaxSpecialAttackUnit: maxSpecialAttackUnit,
                newAttackControllers: attackControllers,
                newSpecialAttackUnitBounds: specialAttackUnitBounds
            );

            entity.AddCharacter_SpecialAttackPoint(0);
        }

        public IAttackStrategy CreateSpecialAttackStrategy()
        {
            return context
                .GetProvider<IAttackStrategyPreset>()
                .CreateStrategy(
                    attackType: SpecialAttackType,
                    attackPoint: SpecialAttack,
                    character: this.AsCharacter()
                );
        }

        public IAttackStrategy CreateSpecialAttackStrategy(AttackType attackType)
        {
            return context
                .GetProvider<IAttackStrategyPreset>()
                .CreateStrategy(
                    attackType: attackType,
                    attackPoint: SpecialAttack,
                    character: this.AsCharacter()
                );
        }

        public bool CanUseSpecialAttack
        {
            get
            {
                var usage = context
                    .GetProvider<ICharacterAttackPreset>()
                    .SpecialAttackPointPerUnit;
                return entity.character_SpecialAttackPoint.value >= usage;
            }
        }

        public List<SpecialAttackUnitBound?> SpecialAttackUnitBounds
        {
            get
            {
                return entity
                    .character_SpecialAttackProperty
                    .specialAttackUnitBounds;
            }
        }

        public List<AttackType> AllSpecialAttackTypes
        {
            get
            {
                return SpecialAttackUnitBounds
                    .Select(sab => sab.Value.attackType)
                    .ToList();
            }
        }

        public List<AttackType> EligibleSpecialAttackTypes
        {
            get
            {
                var pointPerUnit = context
                    .GetProvider<ICharacterAttackPreset>()
                    .SpecialAttackPointPerUnit;
                var currentPoint = entity.character_SpecialAttackPoint.value;
                return SpecialAttackUnitBounds
                    .Where(sab => sab != null && currentPoint >= sab.Value.minUnit * pointPerUnit)
                    .Where(sab =>
                    {
                        return sab?.attackType == AttackType.SpecialAttack1 ||
                               sab?.attackType == AttackType.SpecialAttack2 ||
                               sab?.attackType == AttackType.SpecialAttack3 ||
                               sab?.attackType == AttackType.SpecialAttack4 ||
                               sab?.attackType == AttackType.SpecialAttack5 ||
                               sab?.attackType == AttackType.SpecialAttack6;
                    })
                    .Select(sab => sab.Value.attackType)
                    .ToList();
            }
        }

        public int MaxSpecialAttackPoint
        {
            get
            {
                var preset = context.GetProvider<ICharacterAttackPreset>();
                return entity.character_SpecialAttackProperty.maxSpecialAttackUnit * preset.SpecialAttackPointPerUnit;
            }
        }

        public void GainSpecialAttackPoint(int point)
        {
            var newPoint = entity.character_SpecialAttackPoint.value + point;
            newPoint = Mathf.Min(MaxSpecialAttackPoint, newPoint);
            entity.ReplaceCharacter_SpecialAttackPoint(newPoint);
        }

        private SpecialAttackControllers? GetSpecialAttackControllers(AttackType attackType)
        {
            var controllers = entity.character_SpecialAttackProperty.attackControllers;
            if (controllers == null || !controllers.ContainsKey(attackType)) return null;
            return controllers[attackType];
        }

        public IAttackStrategy GetSpecialAttackStrategy(AttackType attackType)
        {
            try
            {
                var controllers = GetSpecialAttackControllers(attackType);
                var attackStrategyType = controllers?.attackStrategy;
                if (attackStrategyType == null ||
                    !typeof(IAttackStrategy).IsAssignableFrom(attackStrategyType)) return null;

                var character = this.AsCharacter();
                var attackPoint = SpecialAttack;
                
                if (attackStrategyType == typeof(SimpleAttack_Strategy))
                {
                    return new SimpleAttack_Strategy(character, attackPoint);
                }

                if (attackStrategyType == typeof(AroundAttack_Strategy))
                {
                    return new AroundAttack_Strategy(character, attackPoint);
                }
                else if (attackStrategyType == typeof(RowAttack_Strategy))
                {
                    return new RowAttack_Strategy(character, attackPoint, bumpDistance: 1, bumpType: BumpType.Drag);
                }
                else if (attackStrategyType == typeof(SelectionAttack_Strategy))
                {
                    return new SelectionAttack_Strategy(character, attackPoint,
                        new[]
                        {
                            new Vector2Int(0, 1),
                            new Vector2Int(0, 2),
                            new Vector2Int(0, 3)
                        }, false
                    );
                }
                else if (attackStrategyType == typeof(Area2x2_Attack_Strategy))
                {
                    return new Area2x2_Attack_Strategy(character, attackPoint);
                }
                else if (attackStrategyType == typeof(Area3x3_Attack_Strategy))
                {
                    return new Area3x3_Attack_Strategy(character, attackPoint);
                }
                else if (attackStrategyType == typeof(Area4x4_Attack_Strategy))
                {
                    return new Area4x4_Attack_Strategy(character, attackPoint);
                }
                else if (attackStrategyType == typeof(Area5x5_Attack_Strategy))
                {
                    return new Area5x5_Attack_Strategy(character, attackPoint);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"{ex.Message} ${ex.StackTrace}");
                return null;
            }
        }

        public Type GetAttackingControllerType(AttackType attackType)
        {
            var controllers = GetSpecialAttackControllers(attackType);
            var attackingControllerType = controllers?.attackingController;
            if (attackingControllerType == null ||
                !attackingControllerType.IsSubclassOf(typeof(BaseCharacterTimelineController))) return null;
            return attackingControllerType;
        }

        public Type GetDamagedControllerType(AttackType attackType)
        {
            var controllers = GetSpecialAttackControllers(attackType);
            var damagedControllerType = controllers?.damagedController;
            if (damagedControllerType == null ||
                !damagedControllerType.IsSubclassOf(typeof(BaseCharacterTimelineController))) return null;
            return damagedControllerType;
        }

        public AnimationClip GetAttackTimeline(AttackType attackType)
        {
            var controllers = GetSpecialAttackControllers(attackType);
            return controllers?.timeline;
        }

        public void UseSpecialAttack(AttackType attackType)
        {
            if (!EligibleSpecialAttackTypes.Contains(attackType))
            {
                throw new ArgumentOutOfRangeException();
            }

            var preset = context.GetProvider<ICharacterAttackPreset>();
            var specialAttackUnitBound = entity
                .character_SpecialAttackProperty
                .specialAttackUnitBounds
                .FirstOrDefault(sab => sab != null && sab.Value.attackType == attackType);
            if (specialAttackUnitBound == null)
            {
                return;
            }

            var unitToUse = specialAttackUnitBound.Value.minUnit;
            var usedPoints = preset.SpecialAttackPointPerUnit * unitToUse;
            var updatedPoint = Mathf.Max(0, entity.character_SpecialAttackPoint.value - usedPoints);
            entity.ReplaceCharacter_SpecialAttackPoint(updatedPoint);
        }

        public void UseSpecialAttack()
        {
            var preset = context.GetProvider<ICharacterAttackPreset>();
            var unitUsage = entity.character_SpecialAttackProperty.specialAttackUnitUsage;
            var usage = preset.SpecialAttackPointPerUnit * unitUsage;
            var newPoint = entity.character_SpecialAttackPoint.value - usage;
            newPoint = Mathf.Max(0, newPoint);
            entity.ReplaceCharacter_SpecialAttackPoint(newPoint);
        }

        public CharacterEntity? GetDamagableCharacter(Vector2Int position)
        {
            var targetCharacter = context
                .AsCharacterPlacementContext()
                .GetCharacter(position);

            if (targetCharacter == null)
            {
                return null;
            }

            if (targetCharacter.Value.OwnerPlayerId == this.AsCharacter().OwnerPlayerId)
            {
                return null;
            }

            if (!targetCharacter.AsCharacter_Health().IsAlive)
            {
                return null;
            }

            return targetCharacter;
        }

        public int CalculateDamage(
            int targetCharacterId,
            int attackPoint,
            out DamageLevel level
        )
        {
            var character = this.AsCharacter();

            var targetCharacter = context
                .AsCharacterContext()
                .GetCharacter(targetCharacterId)
                .Value;

            var targetCharacterDefend = targetCharacter
                .entity
                .character_AttackProperty
                .defend;

            var characterAccuracy = entity
                .character_AttackProperty
                .accuracy;

            var characterCritical = entity
                .character_AttackProperty
                .critical;

            var charcaterLevel = character.Level;

            var targetcharacterEvasion = entity
                .character_AttackProperty
                .evasion;

            var targetCharcaterLevel = targetCharacter.Level;

            var attackPreset = context
                .GetProvider<ICharacterAttackPreset>();

            var criticalFactor = attackPreset
                .CriticalAttackFactor;

            var groupRelativeAttackRate = 1f;
            var hasCustomGroupRelativeAttackRate = attackPreset
                .GetGroupRelativeAttackRate(
                    attacker: character.Group,
                    damaged: targetCharacter.Group,
                    out var customGroupRelativeAttackRate
                );

            if (hasCustomGroupRelativeAttackRate)
            {
                groupRelativeAttackRate = customGroupRelativeAttackRate;
            }

            var damage = attackPoint *
                         groupRelativeAttackRate *
                         ((100 - (targetCharacter.Level - character.Level) - targetCharacterDefend) * 1f / 100);

            level = DamageLevel.Normal;
            return Mathf.FloorToInt(damage);
        }
    }
}