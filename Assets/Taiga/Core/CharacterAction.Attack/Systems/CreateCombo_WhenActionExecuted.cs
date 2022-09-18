using System.Collections.Generic;
using Entitas;
using Taiga.Core.Character;
using Taiga.Core.Character.Placement;
using Taiga.Core.CharacterSequence;
using Taiga.Core.Unity;
using Taiga.Core.Unity.Character;
using UnityEngine;

namespace Taiga.Core.CharacterAction.Attack
{
    internal class UpdateCombo_WhenActionExecuted : ReactiveSystem<GameEntity>
    {

        GameContext game;

        private int currentVictimId = int.MinValue;
        private int combo;

        public UpdateCombo_WhenActionExecuted(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var characterContext = game.AsCharacterContext();

            foreach (var entity in entities)
            {
                var sequence = entity.AsCharacterSequence(game);
                var damageSequence = entity.AsCharacterSequence_Damaged(game);

                var character = characterContext
                    .GetCharacter(sequence.CharacterId)
                    .Value;

                var victim = sequence.Character;
                Debug.Log($"Attacker: {damageSequence.SourceSequence.AsCharacterSequence().Character.Name}");
                Debug.Log($"Victim: {victim.Name} {victim.Id}");

                // TODO: Reset combo when character is not in the same team?
                if (currentVictimId != victim.Id)
                {
                    currentVictimId = victim.Id;
                    combo = 1;
                }
                else
                {
                    combo += 1; //TODO: add combo multiplier by hit count
                }

                var comboMultiplier = GetComboMultiplier(combo);
                var damage = GetDamageMultiplier(damageSequence.Damage, comboMultiplier);
                Debug.Log($"Combo multiplier: {comboMultiplier * 100}% current combo: {combo} , normalDmg {damageSequence.Damage}, final damage: {damage}");

                var victimCsp = victim.AsGameObject().GetComponent<CharacterStatusPresenter>();
                var attackerFacing = damageSequence.SourceSequence.Character.AsCharacter_Placement().Facing;
                
                // victimCsp.SetComboText(combo, attackerFacing);
                victimCsp.SetDamageText((int)damage);
            }
        }

        protected override bool Filter(GameEntity entity) => entity
            .AsCharacterSequence(game)
            .IsDamaged();

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CharacterSequence_Damaged);
        }

        float GetComboMultiplier(int combo)
        {
            // increse multiplier by 10% for each combo (start at 2 combo)
            return (combo * 0.1f) - 0.1f;
        }

        float GetDamageMultiplier(float baseDmg, float multiplier)
        {
            return baseDmg + (baseDmg * multiplier);
        }
    }
}