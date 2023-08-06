using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common.Components;
using StubbUnity.StubbFramework.Core.Events;
using StubbUnity.StubbFramework.Destroy.Systems;
using StubbUnity.StubbFramework.Pause.Events;
using StubbUnity.StubbFramework.Pause.Systems;
using StubbUnity.StubbFramework.Scenes;
using StubbUnity.StubbFramework.View.Systems;

namespace StubbUnity.StubbFramework.Core
{
    public class SystemTailFeature : EcsFeature
    {
        public SystemTailFeature(EcsWorld world, string name = "TailSystems") : base(world, name)
        {
            Add(new SceneFeature(World));
            Add(new DestroyViewSystem());
            Add(new DestroyEntitySystem());
            Add(new PauseGameSystem());
            Add(new ResumeGameSystem());
            
            OneFrame<IsViewJustConstructed>();
            OneFrame<IsViewJustInitialized>();
            
            OneFrame<PauseGameEvent>();
            OneFrame<ResumeGameEvent>();
            
            OneFrame<ApplicationFocusOnEvent>();
            OneFrame<ApplicationFocusOffEvent>();
            OneFrame<ApplicationPauseOnEvent>();
            OneFrame<ApplicationPauseOffEvent>();
            OneFrame<ApplicationQuitEvent>();
        }
    }
}