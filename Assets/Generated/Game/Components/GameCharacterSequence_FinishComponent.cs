//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Taiga.Core.CharacterSequence.CharacterSequence_Finish characterSequence_FinishComponent = new Taiga.Core.CharacterSequence.CharacterSequence_Finish();

    public bool isCharacterSequence_Finish {
        get { return HasComponent(GameComponentsLookup.CharacterSequence_Finish); }
        set {
            if (value != isCharacterSequence_Finish) {
                var index = GameComponentsLookup.CharacterSequence_Finish;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : characterSequence_FinishComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherCharacterSequence_Finish;

    public static Entitas.IMatcher<GameEntity> CharacterSequence_Finish {
        get {
            if (_matcherCharacterSequence_Finish == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterSequence_Finish);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterSequence_Finish = matcher;
            }

            return _matcherCharacterSequence_Finish;
        }
    }
}
