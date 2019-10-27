using Leopotam.Ecs;
using StubbFramework.Common;

namespace StubbUnity.Contexts
{
    /// <summary>
    /// Context which is implemented this interface will be processed only on FixedUpdate loop.
    /// </summary>
    public interface IFixedUpdateContext : IDispose
    {
        EcsWorld World { get; }
        void Init(EcsWorld world);
        void Run();
    }
}