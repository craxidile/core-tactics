//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight mapCell_Highlight { get { return (Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight)GetComponent(GameComponentsLookup.MapCell_Highlight); } }
    public bool hasMapCell_Highlight { get { return HasComponent(GameComponentsLookup.MapCell_Highlight); } }

    public void AddMapCell_Highlight(UnityEngine.Color newColor) {
        var index = GameComponentsLookup.MapCell_Highlight;
        var component = (Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight)CreateComponent(index, typeof(Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight));
        component.color = newColor;
        AddComponent(index, component);
    }

    public void ReplaceMapCell_Highlight(UnityEngine.Color newColor) {
        var index = GameComponentsLookup.MapCell_Highlight;
        var component = (Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight)CreateComponent(index, typeof(Taiga.Core.Unity.MapCellHighlight.MapCell_Highlight));
        component.color = newColor;
        ReplaceComponent(index, component);
    }

    public void RemoveMapCell_Highlight() {
        RemoveComponent(GameComponentsLookup.MapCell_Highlight);
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

    static Entitas.IMatcher<GameEntity> _matcherMapCell_Highlight;

    public static Entitas.IMatcher<GameEntity> MapCell_Highlight {
        get {
            if (_matcherMapCell_Highlight == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MapCell_Highlight);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMapCell_Highlight = matcher;
            }

            return _matcherMapCell_Highlight;
        }
    }
}
