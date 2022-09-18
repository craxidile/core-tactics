//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity game_StartEntity { get { return GetGroup(GameMatcher.Game_Start).GetSingleEntity(); } }

    public bool isGame_Start {
        get { return game_StartEntity != null; }
        set {
            var entity = game_StartEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isGame_Start = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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

    static readonly Taiga.Core.GameRound.Game_Start game_StartComponent = new Taiga.Core.GameRound.Game_Start();

    public bool isGame_Start {
        get { return HasComponent(GameComponentsLookup.Game_Start); }
        set {
            if (value != isGame_Start) {
                var index = GameComponentsLookup.Game_Start;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : game_StartComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherGame_Start;

    public static Entitas.IMatcher<GameEntity> Game_Start {
        get {
            if (_matcherGame_Start == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Game_Start);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGame_Start = matcher;
            }

            return _matcherGame_Start;
        }
    }
}
