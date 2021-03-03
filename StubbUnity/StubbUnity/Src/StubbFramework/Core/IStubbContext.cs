using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;

namespace StubbUnity.StubbFramework.Core
{
    public interface IStubbContext : IDispose
    {
        EcsWorld World { get; }

        void Init();
        void Run();

        EcsFeature HeadFeature { get; set; }
        EcsFeature UserFeature { get; set; }
        EcsFeature TailFeature { get; set; }
    }
}