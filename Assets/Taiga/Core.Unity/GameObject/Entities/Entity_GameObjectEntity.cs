using Entitas.Unity;
using UnityEngine;

namespace Taiga.Core.Unity
{

    public static class Entity_GameObjectExtensions
    {
        public static Entity_GameObjectEntity AsEntity_GameObject(this IGameScopedEntity entity)
        {
            return new Entity_GameObjectEntity(entity.context, entity.entity);
        }

        public static Entity_GameObjectEntity AsEntity_GameObject(this GameEntity entity, GameContext context)
        {
            return new Entity_GameObjectEntity(context, entity);
        }

        public static GameObject AsGameObject(this IGameScopedEntity entity)
        {
            return entity.entity.AsGameObject();
        }

        public static GameObject AsGameObject(this GameEntity entity)
        {
            if (!entity.hasEntity_GameObject)
            {
                return null;
            }

            return entity.entity_GameObject.value;
        }
    }

    public struct Entity_GameObjectEntity : IGameScopedEntity
    {
        public Entity_GameObjectEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

        public GameObject GameObject => entity.entity_GameObject.value;

        public void Link(GameObject gameObject)
        {
            entity.AddEntity_GameObject(gameObject);
            gameObject.Link(entity);
        }

        public void Unlink()
        {
            entity.isEntity_GameObjectUnlink = true;
        }
    }

}
