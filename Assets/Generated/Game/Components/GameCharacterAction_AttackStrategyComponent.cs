//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy characterAction_AttackStrategy { get { return (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy)GetComponent(GameComponentsLookup.CharacterAction_AttackStrategy); } }
    public bool hasCharacterAction_AttackStrategy { get { return HasComponent(GameComponentsLookup.CharacterAction_AttackStrategy); } }

    public void AddCharacterAction_AttackStrategy(Taiga.Core.Character.Attack.IAttackStrategy newValue) {
        var index = GameComponentsLookup.CharacterAction_AttackStrategy;
        var component = (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacterAction_AttackStrategy(Taiga.Core.Character.Attack.IAttackStrategy newValue) {
        var index = GameComponentsLookup.CharacterAction_AttackStrategy;
        var component = (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategy));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterAction_AttackStrategy() {
        RemoveComponent(GameComponentsLookup.CharacterAction_AttackStrategy);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_AttackStrategy;

    public static Entitas.IMatcher<GameEntity> CharacterAction_AttackStrategy {
        get {
            if (_matcherCharacterAction_AttackStrategy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_AttackStrategy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_AttackStrategy = matcher;
            }

            return _matcherCharacterAction_AttackStrategy;
        }
    }
}
