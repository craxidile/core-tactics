//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.Character.Health.Character_HealthDead character_HealthDeadComponent = new Taiga.Core.Character.Health.Character_HealthDead();

    public bool isCharacter_HealthDead {
        get { return HasComponent(GameComponentsLookup.Character_HealthDead); }
        set {
            if (value != isCharacter_HealthDead) {
                var index = GameComponentsLookup.Character_HealthDead;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : character_HealthDeadComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherCharacter_HealthDead;

    public static Entitas.IMatcher<GameEntity> Character_HealthDead {
        get {
            if (_matcherCharacter_HealthDead == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Character_HealthDead);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacter_HealthDead = matcher;
            }

            return _matcherCharacter_HealthDead;
        }
    }
}
