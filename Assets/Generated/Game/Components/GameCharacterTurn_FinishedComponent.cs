//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.CharacterTurn.CharacterTurn_Finished characterTurn_FinishedComponent = new Taiga.Core.CharacterTurn.CharacterTurn_Finished();

    public bool isCharacterTurn_Finished {
        get { return HasComponent(GameComponentsLookup.CharacterTurn_Finished); }
        set {
            if (value != isCharacterTurn_Finished) {
                var index = GameComponentsLookup.CharacterTurn_Finished;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : characterTurn_FinishedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherCharacterTurn_Finished;

    public static Entitas.IMatcher<GameEntity> CharacterTurn_Finished {
        get {
            if (_matcherCharacterTurn_Finished == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterTurn_Finished);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterTurn_Finished = matcher;
            }

            return _matcherCharacterTurn_Finished;
        }
    }
}
