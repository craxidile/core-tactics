using System;
using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Attack;
using Taiga.Core.Character.Health;
using Taiga.Core.Character.SpecialAttack.Entities;
using Taiga.Core.CharacterTurn;
using UnityEngine;

namespace Taiga.Core.Unity.CharacterBanner
{
    public class UpdateBanner_WhenCharacterTurnChanged : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public UpdateBanner_WhenCharacterTurnChanged(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var characterTurn = entity.AsCharacterTurn(game);
                var character = game
                    .AsCharacterContext()
                    .GetCharacter(characterTurn.CharacterId)
                    .Value;

                var characterHealth = character.AsCharacter_Health();
                var characterAttack = character.AsCharacter_Attack();
                var characterSpecialAttack = character.AsCharacter_SpecialAttack();
                var attackContext = game.AsCharacterAttackContext();

                var bannerPresenter = CharacterBannerPresenter.Instance;
                bannerPresenter.ShowCharacterValues(
                    architypeId: character.ArchitypeId,
                    isLocalPlayer: character.IsLocalPlayer,
                    level: character.Level,
                    attack: characterAttack.Attack,
                    defend: characterAttack.Defend,
                    dexerity: characterAttack.Accuracy,
                    evasion: characterAttack.Evasion,
                    critical: characterAttack.Critical,
                    maxHealth: characterHealth.MaxHealth,
                    health: characterHealth.Health
                );

                Debug.Log($">>special_attack_point<< updated_character {character.Id}");

                if (characterAttack.HasSpecialAttack)
                {
                    bannerPresenter.ShowSpecialAttack(
                        specialAttackPoint: characterSpecialAttack.Point,
                        specialAttackPointPerUnit: CharacterEntity_SpecialAttack.GaugePoint
                    );
                }
                else
                {
                    bannerPresenter.HideSpecialAttack();
                }
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(CharacterTurnEvents.OnCharacterTurn);
        }
    }
}