//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Unity.Entity_GameObject entity_GameObject { get { return (Taiga.Core.Unity.Entity_GameObject)GetComponent(GameComponentsLookup.Entity_GameObject); } }
    public bool hasEntity_GameObject { get { return HasComponent(GameComponentsLookup.Entity_GameObject); } }

    public void AddEntity_GameObject(UnityEngine.GameObject newValue) {
        var index = GameComponentsLookup.Entity_GameObject;
        var component = (Taiga.Core.Unity.Entity_GameObject)CreateComponent(index, typeof(Taiga.Core.Unity.Entity_GameObject));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceEntity_GameObject(UnityEngine.GameObject newValue) {
        var index = GameComponentsLookup.Entity_GameObject;
        var component = (Taiga.Core.Unity.Entity_GameObject)CreateComponent(index, typeof(Taiga.Core.Unity.Entity_GameObject));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveEntity_GameObject() {
        RemoveComponent(GameComponentsLookup.Entity_GameObject);
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

    static Entitas.IMatcher<GameEntity> _matcherEntity_GameObject;

    public static Entitas.IMatcher<GameEntity> Entity_GameObject {
        get {
            if (_matcherEntity_GameObject == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Entity_GameObject);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEntity_GameObject = matcher;
            }

            return _matcherEntity_GameObject;
        }
    }
}