//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Character.Placement.Character_FacingDirection character_FacingDirection { get { return (Taiga.Core.Character.Placement.Character_FacingDirection)GetComponent(GameComponentsLookup.Character_FacingDirection); } }
    public bool hasCharacter_FacingDirection { get { return HasComponent(GameComponentsLookup.Character_FacingDirection); } }

    public void AddCharacter_FacingDirection(Taiga.Core.MapDirection newValue) {
        var index = GameComponentsLookup.Character_FacingDirection;
        var component = (Taiga.Core.Character.Placement.Character_FacingDirection)CreateComponent(index, typeof(Taiga.Core.Character.Placement.Character_FacingDirection));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacter_FacingDirection(Taiga.Core.MapDirection newValue) {
        var index = GameComponentsLookup.Character_FacingDirection;
        var component = (Taiga.Core.Character.Placement.Character_FacingDirection)CreateComponent(index, typeof(Taiga.Core.Character.Placement.Character_FacingDirection));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacter_FacingDirection() {
        RemoveComponent(GameComponentsLookup.Character_FacingDirection);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacter_FacingDirection;

    public static Entitas.IMatcher<GameEntity> Character_FacingDirection {
        get {
            if (_matcherCharacter_FacingDirection == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Character_FacingDirection);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacter_FacingDirection = matcher;
            }

            return _matcherCharacter_FacingDirection;
        }
    }
}
