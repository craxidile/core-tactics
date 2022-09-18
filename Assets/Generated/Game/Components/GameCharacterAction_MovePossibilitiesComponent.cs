//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities characterAction_MovePossibilities { get { return (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities)GetComponent(GameComponentsLookup.CharacterAction_MovePossibilities); } }
    public bool hasCharacterAction_MovePossibilities { get { return HasComponent(GameComponentsLookup.CharacterAction_MovePossibilities); } }

    public void AddCharacterAction_MovePossibilities(System.Collections.Generic.ICollection<UnityEngine.Vector2Int> newPositions, object newPathwayCalculationCache) {
        var index = GameComponentsLookup.CharacterAction_MovePossibilities;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities));
        component.positions = newPositions;
        component.pathwayCalculationCache = newPathwayCalculationCache;
        AddComponent(index, component);
    }

    public void ReplaceCharacterAction_MovePossibilities(System.Collections.Generic.ICollection<UnityEngine.Vector2Int> newPositions, object newPathwayCalculationCache) {
        var index = GameComponentsLookup.CharacterAction_MovePossibilities;
        var component = (Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Move.CharacterAction_MovePossibilities));
        component.positions = newPositions;
        component.pathwayCalculationCache = newPathwayCalculationCache;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterAction_MovePossibilities() {
        RemoveComponent(GameComponentsLookup.CharacterAction_MovePossibilities);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_MovePossibilities;

    public static Entitas.IMatcher<GameEntity> CharacterAction_MovePossibilities {
        get {
            if (_matcherCharacterAction_MovePossibilities == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_MovePossibilities);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_MovePossibilities = matcher;
            }

            return _matcherCharacterAction_MovePossibilities;
        }
    }
}
