//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.Unity.Entity_GameObjectUnlink entity_GameObjectUnlinkComponent = new Taiga.Core.Unity.Entity_GameObjectUnlink();

    public bool isEntity_GameObjectUnlink {
        get { return HasComponent(GameComponentsLookup.Entity_GameObjectUnlink); }
        set {
            if (value != isEntity_GameObjectUnlink) {
                var index = GameComponentsLookup.Entity_GameObjectUnlink;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : entity_GameObjectUnlinkComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherEntity_GameObjectUnlink;

    public static Entitas.IMatcher<GameEntity> Entity_GameObjectUnlink {
        get {
            if (_matcherEntity_GameObjectUnlink == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Entity_GameObjectUnlink);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEntity_GameObjectUnlink = matcher;
            }

            return _matcherEntity_GameObjectUnlink;
        }
    }
}
