//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterTurn.CharacterTurn characterTurn { get { return (Taiga.Core.CharacterTurn.CharacterTurn)GetComponent(GameComponentsLookup.CharacterTurn); } }
    public bool hasCharacterTurn { get { return HasComponent(GameComponentsLookup.CharacterTurn); } }

    public void AddCharacterTurn(int newCharacterId) {
        var index = GameComponentsLookup.CharacterTurn;
        var component = (Taiga.Core.CharacterTurn.CharacterTurn)CreateComponent(index, typeof(Taiga.Core.CharacterTurn.CharacterTurn));
        component.characterId = newCharacterId;
        AddComponent(index, component);
    }

    public void ReplaceCharacterTurn(int newCharacterId) {
        var index = GameComponentsLookup.CharacterTurn;
        var component = (Taiga.Core.CharacterTurn.CharacterTurn)CreateComponent(index, typeof(Taiga.Core.CharacterTurn.CharacterTurn));
        component.characterId = newCharacterId;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterTurn() {
        RemoveComponent(GameComponentsLookup.CharacterTurn);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterTurn;

    public static Entitas.IMatcher<GameEntity> CharacterTurn {
        get {
            if (_matcherCharacterTurn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterTurn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterTurn = matcher;
            }

            return _matcherCharacterTurn;
        }
    }
}