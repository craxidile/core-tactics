namespace Taiga.Core.Player
{

    public static class PlayerEntityExtensions
    {
        public static PlayerEntity AsPlayer(this IGameScopedEntity entity)
        {
            return new PlayerEntity(entity.context, entity.entity);
        }

        public static PlayerEntity AsPlayer(this GameEntity entity, GameContext context)
        {
            return new PlayerEntity(context, entity);
        }
    }

    public struct PlayerEntity : IGameScopedEntity
    {
        public PlayerEntity(GameContext context, GameEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public GameContext context { get; private set; }

        public GameEntity entity { get; private set; }

    }
}
