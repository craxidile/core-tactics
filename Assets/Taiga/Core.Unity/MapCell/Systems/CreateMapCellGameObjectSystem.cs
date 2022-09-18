using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Taiga.Core.Character.Placement;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.MapCell
{
    internal class CreateMapCellGameObjectSystem : ReactiveSystem<GameEntity>
    {

        GameContext game;

        public CreateMapCellGameObjectSystem(Contexts contexts) : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var cellPrefab = game.GetProvider<IMapCellPrefabPreset>().MapCellPrefab;

            foreach (var entity in entities)
            {
                var cell = entity.AsMapCell(game);

                var unityPosition = cell.Position.GameToUnityPosition();
                var cellGameObject = GameObject.Instantiate(
                    original: cellPrefab,
                    position: unityPosition,
                    rotation: Quaternion.identity
                );
                cell.AsEntity_GameObject()
                    .Link(cellGameObject);
            }
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(MapEvents.OnCellCreated);
        }
    }
}
