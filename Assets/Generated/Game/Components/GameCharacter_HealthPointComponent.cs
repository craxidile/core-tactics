//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Character.Health.Character_HealthPoint character_HealthPoint { get { return (Taiga.Core.Character.Health.Character_HealthPoint)GetComponent(GameComponentsLookup.Character_HealthPoint); } }
    public bool hasCharacter_HealthPoint { get { return HasComponent(GameComponentsLookup.Character_HealthPoint); } }

    public void AddCharacter_HealthPoint(int newValue) {
        var index = GameComponentsLookup.Character_HealthPoint;
        var component = (Taiga.Core.Character.Health.Character_HealthPoint)CreateComponent(index, typeof(Taiga.Core.Character.Health.Character_HealthPoint));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacter_HealthPoint(int newValue) {
        var index = GameComponentsLookup.Character_HealthPoint;
        var component = (Taiga.Core.Character.Health.Character_HealthPoint)CreateComponent(index, typeof(Taiga.Core.Character.Health.Character_HealthPoint));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacter_HealthPoint() {
        RemoveComponent(GameComponentsLookup.Character_HealthPoint);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacter_HealthPoint;

    public static Entitas.IMatcher<GameEntity> Character_HealthPoint {
        get {
            if (_matcherCharacter_HealthPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Character_HealthPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacter_HealthPoint = matcher;
            }

            return _matcherCharacter_HealthPoint;
        }
    }
}
