//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity game_CharacterTurnOrderEntity { get { return GetGroup(GameMatcher.Game_CharacterTurnOrder).GetSingleEntity(); } }
    public Taiga.Core.CharacterTurn.Game_CharacterTurnOrder game_CharacterTurnOrder { get { return game_CharacterTurnOrderEntity.game_CharacterTurnOrder; } }
    public bool hasGame_CharacterTurnOrder { get { return game_CharacterTurnOrderEntity != null; } }

    public GameEntity SetGame_CharacterTurnOrder(System.Collections.Generic.IEnumerable<int> newCharacterIds) {
        if (hasGame_CharacterTurnOrder) {
            throw new Entitas.EntitasException("Could not set Game_CharacterTurnOrder!\n" + this + " already has an entity with Taiga.Core.CharacterTurn.Game_CharacterTurnOrder!",
                "You should check if the context already has a game_CharacterTurnOrderEntity before setting it or use context.ReplaceGame_CharacterTurnOrder().");
        }
        var entity = CreateEntity();
        entity.AddGame_CharacterTurnOrder(newCharacterIds);
        return entity;
    }

    public void ReplaceGame_CharacterTurnOrder(System.Collections.Generic.IEnumerable<int> newCharacterIds) {
        var entity = game_CharacterTurnOrderEntity;
        if (entity == null) {
            entity = SetGame_CharacterTurnOrder(newCharacterIds);
        } else {
            entity.ReplaceGame_CharacterTurnOrder(newCharacterIds);
        }
    }

    public void RemoveGame_CharacterTurnOrder() {
        game_CharacterTurnOrderEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.CharacterTurn.Game_CharacterTurnOrder game_CharacterTurnOrder { get { return (Taiga.Core.CharacterTurn.Game_CharacterTurnOrder)GetComponent(GameComponentsLookup.Game_CharacterTurnOrder); } }
    public bool hasGame_CharacterTurnOrder { get { return HasComponent(GameComponentsLookup.Game_CharacterTurnOrder); } }

    public void AddGame_CharacterTurnOrder(System.Collections.Generic.IEnumerable<int> newCharacterIds) {
        var index = GameComponentsLookup.Game_CharacterTurnOrder;
        var component = (Taiga.Core.CharacterTurn.Game_CharacterTurnOrder)CreateComponent(index, typeof(Taiga.Core.CharacterTurn.Game_CharacterTurnOrder));
        component.characterIds = newCharacterIds;
        AddComponent(index, component);
    }

    public void ReplaceGame_CharacterTurnOrder(System.Collections.Generic.IEnumerable<int> newCharacterIds) {
        var index = GameComponentsLookup.Game_CharacterTurnOrder;
        var component = (Taiga.Core.CharacterTurn.Game_CharacterTurnOrder)CreateComponent(index, typeof(Taiga.Core.CharacterTurn.Game_CharacterTurnOrder));
        component.characterIds = newCharacterIds;
        ReplaceComponent(index, component);
    }

    public void RemoveGame_CharacterTurnOrder() {
        RemoveComponent(GameComponentsLookup.Game_CharacterTurnOrder);
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

    static Entitas.IMatcher<GameEntity> _matcherGame_CharacterTurnOrder;

    public static Entitas.IMatcher<GameEntity> Game_CharacterTurnOrder {
        get {
            if (_matcherGame_CharacterTurnOrder == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Game_CharacterTurnOrder);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGame_CharacterTurnOrder = matcher;
            }

            return _matcherGame_CharacterTurnOrder;
        }
    }
}
