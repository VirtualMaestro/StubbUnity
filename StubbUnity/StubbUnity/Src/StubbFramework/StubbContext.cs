using Leopotam.Ecs;
using StubbUnity.StubbFramework.Debugging;

namespace StubbUnity.StubbFramework
{
    public class StubbContext : IStubbContext
    {
        private EcsWorld _world;
        private EcsSystems _rootSystems;
        private IStubbDebug _debugger;

        public EcsFeature HeadSystemFeature { get; set; }
        public EcsFeature UsersFeature { get; set; }
        public EcsFeature TailSystemFeature { get; set; }

        public bool IsDisposed => _world == null;
        public EcsWorld World => _world;

        public StubbContext()
        {
            Stubb.AddContext(this);

            HeadSystemFeature = new SystemHeadFeature();
            TailSystemFeature = new SystemTailFeature();
        }

        public void Init(EcsWorld world, IStubbDebug debug = null)
        {
            _world = world;
            _debugger = debug;
            _rootSystems = new EcsSystems(World, "RootSystems");

            HeadSystemFeature?.Init(_world, _rootSystems, _rootSystems);
            UsersFeature?.Init(_world, _rootSystems, _rootSystems);
            TailSystemFeature?.Init(_world, _rootSystems, _rootSystems);

            _debugger?.Init(_rootSystems, _world);

            _rootSystems.ProcessInjects();
            _rootSystems.Init();
        }

        public void Run()
        {
            _rootSystems.Run();
            _debugger?.Debug();
        }

        public void Dispose()
        {
            _rootSystems.Destroy();
            _world.Destroy();

            _world = null;
            _rootSystems = null;
        }
    }
}