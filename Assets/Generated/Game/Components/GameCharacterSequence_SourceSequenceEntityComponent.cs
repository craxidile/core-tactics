//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity characterSequence_SourceSequenceEntity { get { return (Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity)GetComponent(GameComponentsLookup.CharacterSequence_SourceSequenceEntity); } }
    public bool hasCharacterSequence_SourceSequenceEntity { get { return HasComponent(GameComponentsLookup.CharacterSequence_SourceSequenceEntity); } }

    public void AddCharacterSequence_SourceSequenceEntity(GameEntity newValue) {
        var index = GameComponentsLookup.CharacterSequence_SourceSequenceEntity;
        var component = (Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity)CreateComponent(index, typeof(Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacterSequence_SourceSequenceEntity(GameEntity newValue) {
        var index = GameComponentsLookup.CharacterSequence_SourceSequenceEntity;
        var component = (Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity)CreateComponent(index, typeof(Taiga.Core.CharacterSequence.CharacterSequence_SourceSequenceEntity));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterSequence_SourceSequenceEntity() {
        RemoveComponent(GameComponentsLookup.CharacterSequence_SourceSequenceEntity);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterSequence_SourceSequenceEntity;

    public static Entitas.IMatcher<GameEntity> CharacterSequence_SourceSequenceEntity {
        get {
            if (_matcherCharacterSequence_SourceSequenceEntity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterSequence_SourceSequenceEntity);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterSequence_SourceSequenceEntity = matcher;
            }

            return _matcherCharacterSequence_SourceSequenceEntity;
        }
    }
}
