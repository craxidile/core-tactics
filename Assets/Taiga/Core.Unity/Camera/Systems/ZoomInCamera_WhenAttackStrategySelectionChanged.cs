using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterAction;
using Taiga.Core.CharacterAction.Attack;
using Taiga.Core.CharacterSequence;
using Taiga.Core.CharacterTurn;
using Taiga.Core.Unity.CharacterAnimation;
using Taiga.Utils;
using UnityEngine;

namespace Taiga.Core.Unity.Camera
{
    internal class ZoomInCamera_WhenAttackStrategySelectionChanged : ReactiveSystem<GameEntity>
    {
        public GameContext game;

        public ZoomInCamera_WhenAttackStrategySelectionChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();

            var gameCamera = game.cameraEntity
                .AsGameObject()
                .GetComponent<GameCamera>();

            foreach (var damagedEntity in entities)
            {
                var damagedSequence = damagedEntity.AsCharacterSequence_Damaged(game);

                if (damagedSequence.SourceSequence.IsAttack())
                {
                    var attackSequence = damagedSequence
                        .SourceSequence
                        .AsCharacterSequence_Attack();
                    var attackEntity = attackSequence.entity;


                    int? cameraAngle = 45;
                    var attackerDirection = attackSequence.Direction;
                    if (attackerDirection == MapDirection.East)
                    {
                        cameraAngle += 90;
                    }
                    else if (attackerDirection == MapDirection.North)
                    {
                        cameraAngle += 270;
                    }
                    else if (attackerDirection == MapDirection.West)
                    {
                        cameraAngle += 180;
                    }
                    else if (attackerDirection == MapDirection.South)
                    {
                        cameraAngle += 90;
                    }

                    cameraAngle %= 360;


                    Debug.Log($">>attack_direction<< {gameCamera.Angle} {attackerDirection}");

                    // TODO TEMPORARY USE FOR TESTING SPECIAL ATTACKS
                    cameraAngle = attackSequence.AttackType == AttackType.Attack1 ? null : cameraAngle;

                    if (attackSequence.AttackType != AttackType.Attack1)
                    {
                        attackEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Rotating);
                        damagedEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Rotating);
                        game.cameraEntity
                            .AsGameObject()
                            .GetComponent<GameCamera>()
                            .ZoomInAndDim(cameraAngle, () =>
                            {
                                SpriteRearranger.Rearrange();
                                attackEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Ready);
                                damagedEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Ready);
                            });
                    }
                    else
                    {
                        attackEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Ready);
                        damagedEntity.ReplaceCamera_AttackStatusChanged(CameraAttackStatus.Ready);
                    }
                }
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsDamaged();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Animating);
        }
    }
}