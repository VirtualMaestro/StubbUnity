using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;
using StubbUnity.StubbFramework.Debugging;

namespace StubbUnity.StubbFramework
{
    public interface IStubbContext : IDispose
    {
        EcsWorld World { get; }

        void Init(EcsWorld world, IStubbDebug debug = null);
        void Run();
    }
}