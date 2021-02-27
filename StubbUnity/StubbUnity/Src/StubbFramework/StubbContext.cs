using Leopotam.Ecs;
using StubbUnity.StubbFramework.Debugging;

namespace StubbUnity.StubbFramework
{
    public class StubbContext : IStubbContext
    {
        public bool IsDisposed { get; }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public EcsWorld World { get; }
        public void Init(EcsWorld world, IStubbDebug debug = null)
        {
            throw new System.NotImplementedException();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}