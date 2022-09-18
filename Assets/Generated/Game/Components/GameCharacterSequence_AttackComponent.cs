//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterSequence.CharacterSequence_Attack characterSequence_Attack { get { return (Taiga.Core.CharacterSequence.CharacterSequence_Attack)GetComponent(GameComponentsLookup.CharacterSequence_Attack); } }
    public bool hasCharacterSequence_Attack { get { return HasComponent(GameComponentsLookup.CharacterSequence_Attack); } }

    public void AddCharacterSequence_Attack(Taiga.Core.Character.Attack.AttackType newAttackType, Taiga.Core.MapDirection newDirection) {
        var index = GameComponentsLookup.CharacterSequence_Attack;
        var component = (Taiga.Core.CharacterSequence.CharacterSequence_Attack)CreateComponent(index, typeof(Taiga.Core.CharacterSequence.CharacterSequence_Attack));
        component.attackType = newAttackType;
        component.direction = newDirection;
        AddComponent(index, component);
    }

    public void ReplaceCharacterSequence_Attack(Taiga.Core.Character.Attack.AttackType newAttackType, Taiga.Core.MapDirection newDirection) {
        var index = GameComponentsLookup.CharacterSequence_Attack;
        var component = (Taiga.Core.CharacterSequence.CharacterSequence_Attack)CreateComponent(index, typeof(Taiga.Core.CharacterSequence.CharacterSequence_Attack));
        component.attackType = newAttackType;
        component.direction = newDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterSequence_Attack() {
        RemoveComponent(GameComponentsLookup.CharacterSequence_Attack);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCharacterSequence_Attack;

    public static Entitas.IMatcher<GameEntity> CharacterSequence_Attack {
        get {
            if (_matcherCharacterSequence_Attack == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterSequence_Attack);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterSequence_Attack = matcher;
            }

            return _matcherCharacterSequence_Attack;
        }
    }
}
