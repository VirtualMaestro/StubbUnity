using Leopotam.Ecs;
using StubbUnity.StubbFramework.Scenes.Configurations;

namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// Event-component is sent when need to load one or bunch scenes. Will be removed at the end of the loop
    /// For convenience use World.LoadScenes().
    /// </summary>
    public struct ProcessScenesEvent : IEcsAutoReset<ProcessScenesEvent>
    {
        public ScenesLoadingConfiguration Configuration;

        public void AutoReset(ref ProcessScenesEvent c)
        {
            c.Configuration = null;
        }
    }
}