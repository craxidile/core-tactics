//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection characterAction_AttackStrategySelection { get { return (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection)GetComponent(GameComponentsLookup.CharacterAction_AttackStrategySelection); } }
    public bool hasCharacterAction_AttackStrategySelection { get { return HasComponent(GameComponentsLookup.CharacterAction_AttackStrategySelection); } }

    public void AddCharacterAction_AttackStrategySelection(Taiga.Core.Character.Attack.IAttackStrategy_Selection newValue) {
        var index = GameComponentsLookup.CharacterAction_AttackStrategySelection;
        var component = (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacterAction_AttackStrategySelection(Taiga.Core.Character.Attack.IAttackStrategy_Selection newValue) {
        var index = GameComponentsLookup.CharacterAction_AttackStrategySelection;
        var component = (Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection)CreateComponent(index, typeof(Taiga.Core.CharacterActionPrediction.Attack.CharacterAction_AttackStrategySelection));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterAction_AttackStrategySelection() {
        RemoveComponent(GameComponentsLookup.CharacterAction_AttackStrategySelection);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterAction_AttackStrategySelection;

    public static Entitas.IMatcher<GameEntity> CharacterAction_AttackStrategySelection {
        get {
            if (_matcherCharacterAction_AttackStrategySelection == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterAction_AttackStrategySelection);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterAction_AttackStrategySelection = matcher;
            }

            return _matcherCharacterAction_AttackStrategySelection;
        }
    }
}
