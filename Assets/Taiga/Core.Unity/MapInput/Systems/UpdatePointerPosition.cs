using System;
using Entitas;
using UnityEngine;

namespace Taiga.Core.Unity.MapInput
{
    internal class UpdatePointerPosition : IInitializeSystem
    {

        GameContext game;

        public UpdatePointerPosition(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var groundInput = GameObject.FindObjectOfType<MapGroundInput>();
            groundInput.OnPointerUpdate += HandlePositionMoved;
        }

        private void HandlePositionMoved(Vector2? position)
        {
            if (position == null)
            {
                if (game.hasMap_PointerPosition)
                {
                    game.RemoveMap_PointerPosition();
                }
                return;
            }

            if (!game.hasMap_PointerPosition)
            {
                var entity = game.CreateEntity();
                entity.AddMap_PointerPosition(position.Value);
                return;
            }

            if (game.map_PointerPosition.position != position)
            {
                game.ReplaceMap_PointerPosition(position.Value);
            }
        }

    }
}
