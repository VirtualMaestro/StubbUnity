using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;

namespace StubbUnity.StubbFramework
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