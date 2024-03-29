namespace StubbUnity.StubbFramework.Scenes.Events
{
    /// <summary>
    /// This event is sent when all the scenes in given config were loaded.
    /// </summary>
    public struct ScenesLoadingConfigurationCompleteEvent
    {
        public string ConfigurationName;
    }
}