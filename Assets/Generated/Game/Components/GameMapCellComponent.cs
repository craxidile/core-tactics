//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Map.MapCell mapCell { get { return (Taiga.Core.Map.MapCell)GetComponent(GameComponentsLookup.MapCell); } }
    public bool hasMapCell { get { return HasComponent(GameComponentsLookup.MapCell); } }

    public void AddMapCell(UnityEngine.Vector2Int newPosition) {
        var index = GameComponentsLookup.MapCell;
        var component = (Taiga.Core.Map.MapCell)CreateComponent(index, typeof(Taiga.Core.Map.MapCell));
        component.position = newPosition;
        AddComponent(index, component);
    }

    public void ReplaceMapCell(UnityEngine.Vector2Int newPosition) {
        var index = GameComponentsLookup.MapCell;
        var component = (Taiga.Core.Map.MapCell)CreateComponent(index, typeof(Taiga.Core.Map.MapCell));
        component.position = newPosition;
        ReplaceComponent(index, component);
    }

    public void RemoveMapCell() {
        RemoveComponent(GameComponentsLookup.MapCell);
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

    static Entitas.IMatcher<GameEntity> _matcherMapCell;

    public static Entitas.IMatcher<GameEntity> MapCell {
        get {
            if (_matcherMapCell == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MapCell);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMapCell = matcher;
            }

            return _matcherMapCell;
        }
    }
}
