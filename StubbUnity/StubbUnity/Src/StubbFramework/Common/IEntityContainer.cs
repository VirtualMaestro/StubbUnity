using Leopotam.Ecs;

namespace StubbUnity.StubbFramework.Common
{
    public interface IEntityContainer : IDestroy
    {
        void Initialize();
        bool HasEntity { get; }
        ref EcsEntity GetEntity();
        void SetEntity(ref EcsEntity entity);
    }
}