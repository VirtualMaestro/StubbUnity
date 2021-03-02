using System;
using Leopotam.Ecs;

namespace StubbUnity.StubbFramework
{
    public class EcsFeature : IEcsSystem
    {
        private bool _isEnable;
        private EcsSystems _parentSystems;
        private EcsSystems _rootSystems;
        private readonly EcsSystems _internalSystems;

        private readonly EcsWorld _world;

        /// <summary>
        /// For internal use.
        /// </summary>
        public EcsSystems InternalSystems => _internalSystems;

        public string Name { get; }
        public EcsWorld World => _world;

        public EcsFeature(EcsWorld world, string name = null, bool isEnable = true)
        {
            _world = world;
            Name = name ?? GetType().Name;
            _isEnable = isEnable;

            _internalSystems = new EcsSystems(_world, $"{Name}Systems");
        }

        public void Init(EcsSystems parentSystems, EcsSystems rootSystems)
        {
            _parentSystems = parentSystems;
            _rootSystems = rootSystems;

            _parentSystems.Add(this);
            _parentSystems.Add(_internalSystems);

            SetupSystems();

            if (!_isEnable)
                Enable = _isEnable;
        }

        public bool Enable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;

                _EnableSystems(_internalSystems.Name, _isEnable);
            }
        }

        protected void Add(IEcsSystem system)
        {
            if (system is EcsFeature feature)
                feature.Init(_internalSystems, _rootSystems);
            else
                _internalSystems.Add(system);
        }

        /// <summary>
        /// Injects only in scope of this feature (so all children systems).
        /// </summary>
        protected void Inject(object data, Type overridenType = null)
        {
            _internalSystems.Inject(data, overridenType);
        }

        protected void InjectGlobal(object data, Type overridenType = null)
        {
            _rootSystems.Inject(data, overridenType);
        }

        protected void OneFrame<T>() where T : struct
        {
            _internalSystems.OneFrame<T>();
        }

        /// <summary>
        /// Method where all the systems should be created and added.
        /// </summary>
        protected virtual void SetupSystems()
        {
        }

        private void _EnableSystems(string systemsName, bool isEnable)
        {
            var idx = _parentSystems.GetNamedRunSystem(systemsName);
            _parentSystems.SetRunSystemState(idx, isEnable);
        }
    }
}