//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.Roslyn.CodeGeneration.Plugins.CleanupSystemsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class GameCleanupSystems : Feature {

    public GameCleanupSystems(Contexts contexts) {
        Add(new RemoveMapCell_HighlightOffGameSystem(contexts));
        Add(new RemoveMapCell_PointerTriggerGameSystem(contexts));
        Add(new RemoveMap_PointerTriggerGameSystem(contexts));
        Add(new RemoveEntity_GameObjectUnlinkGameSystem(contexts));
        Add(new DestroyCharacterDamageGameSystem(contexts));
        Add(new DestroyCharacterAction_CancelGameSystem(contexts));
        Add(new DestroyCharacterAction_RemoveGameSystem(contexts));
        Add(new DestroyCharacterTurn_FinishedGameSystem(contexts));
        Add(new DestroyCharacterSequence_DestroyGameSystem(contexts));
    }
}
