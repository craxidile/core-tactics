//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.Unity.CharacterAnimation.Effect.Character_Effects character_EffectsComponent = new Taiga.Core.Unity.CharacterAnimation.Effect.Character_Effects();

    public bool isCharacter_Effects {
        get { return HasComponent(GameComponentsLookup.Character_Effects); }
        set {
            if (value != isCharacter_Effects) {
                var index = GameComponentsLookup.Character_Effects;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : character_EffectsComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherCharacter_Effects;

    public static Entitas.IMatcher<GameEntity> Character_Effects {
        get {
            if (_matcherCharacter_Effects == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Character_Effects);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacter_Effects = matcher;
            }

            return _matcherCharacter_Effects;
        }
    }
}
