//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.CharacterAction.CharacterAction_Executable characterAction_ExecutableComponent = new Taiga.Core.CharacterAction.CharacterAction_Executable();

    public bool isCharacterAction_Executable {
        get { return HasComponent(GameComponentsLookup.CharacterAction_Executable); }
        set {
            if (value != isCharacterAction_Executable) {
                var index = GameComponentsLookup.CharacterAction_Executable;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : characterAction_ExecutableComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_Executable;

    public static Entitas.IMatcher<GameEntity> CharacterAction_Executable {
        get {
            if (_matcherCharacterAction_Executable == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_Executable);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_Executable = matcher;
            }

            return _matcherCharacterAction_Executable;
        }
    }
}
