//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus characterAction_MoveFocus { get { return (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus)GetComponent(GameComponentsLookup.CharacterAction_MoveFocus); } }
    public bool hasCharacterAction_MoveFocus { get { return HasComponent(GameComponentsLookup.CharacterAction_MoveFocus); } }

    public void AddCharacterAction_MoveFocus(UnityEngine.Vector2Int newPosition) {
        var index = GameComponentsLookup.CharacterAction_MoveFocus;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus));
        component.position = newPosition;
        AddComponent(index, component);
    }

    public void ReplaceCharacterAction_MoveFocus(UnityEngine.Vector2Int newPosition) {
        var index = GameComponentsLookup.CharacterAction_MoveFocus;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveFocus));
        component.position = newPosition;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterAction_MoveFocus() {
        RemoveComponent(GameComponentsLookup.CharacterAction_MoveFocus);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_MoveFocus;

    public static Entitas.IMatcher<GameEntity> CharacterAction_MoveFocus {
        get {
            if (_matcherCharacterAction_MoveFocus == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_MoveFocus);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_MoveFocus = matcher;
            }

            return _matcherCharacterAction_MoveFocus;
        }
    }
}