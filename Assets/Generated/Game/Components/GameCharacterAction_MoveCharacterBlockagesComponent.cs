//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages characterAction_MoveCharacterBlockages { get { return (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages)GetComponent(GameComponentsLookup.CharacterAction_MoveCharacterBlockages); } }
    public bool hasCharacterAction_MoveCharacterBlockages { get { return HasComponent(GameComponentsLookup.CharacterAction_MoveCharacterBlockages); } }

    public void AddCharacterAction_MoveCharacterBlockages(System.Collections.Generic.ICollection<UnityEngine.Vector2Int> newPositions) {
        var index = GameComponentsLookup.CharacterAction_MoveCharacterBlockages;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages));
        component.positions = newPositions;
        AddComponent(index, component);
    }

    public void ReplaceCharacterAction_MoveCharacterBlockages(System.Collections.Generic.ICollection<UnityEngine.Vector2Int> newPositions) {
        var index = GameComponentsLookup.CharacterAction_MoveCharacterBlockages;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MoveCharacterBlockages));
        component.positions = newPositions;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterAction_MoveCharacterBlockages() {
        RemoveComponent(GameComponentsLookup.CharacterAction_MoveCharacterBlockages);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_MoveCharacterBlockages;

    public static Entitas.IMatcher<GameEntity> CharacterAction_MoveCharacterBlockages {
        get {
            if (_matcherCharacterAction_MoveCharacterBlockages == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_MoveCharacterBlockages);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_MoveCharacterBlockages = matcher;
            }

            return _matcherCharacterAction_MoveCharacterBlockages;
        }
    }
}
