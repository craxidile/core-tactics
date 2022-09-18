using Entitas;

namespace Taiga.Core
{
    public interface IScopedEntity<TContext, TEntity>
        where TContext : class, IContext<TEntity>
        where TEntity : class, IEntity
    {
        TContext context { get; }
        TEntity entity { get; }
    }

    public interface IGameScopedEntity : IScopedEntity<GameContext, GameEntity>
    {
    }

}
