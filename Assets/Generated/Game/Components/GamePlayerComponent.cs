//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Taiga.Core.Player.Player player { get { return (Taiga.Core.Player.Player)GetComponent(GameComponentsLookup.Player); } }
    public bool hasPlayer { get { return HasComponent(GameComponentsLookup.Player); } }

    public void AddPlayer(int newId) {
        var index = GameComponentsLookup.Player;
        var component = (Taiga.Core.Player.Player)CreateComponent(index, typeof(Taiga.Core.Player.Player));
        component.id = newId;
        AddComponent(index, component);
    }

    public void ReplacePlayer(int newId) {
        var index = GameComponentsLookup.Player;
        var component = (Taiga.Core.Player.Player)CreateComponent(index, typeof(Taiga.Core.Player.Player));
        component.id = newId;
        ReplaceComponent(index, component);
    }

    public void RemovePlayer() {
        RemoveComponent(GameComponentsLookup.Player);
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

    static Entitas.IMatcher<GameEntity> _matcherPlayer;

    public static Entitas.IMatcher<GameEntity> Player {
        get {
            if (_matcherPlayer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Player);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayer = matcher;
            }

            return _matcherPlayer;
        }
    }
}
