namespace StubbUnity.StubbFramework.Scenes.Components
{
    /// <summary>
    /// One-frame component which should be attached to the scene controller entity if this scene needs to be activated.
    /// For convenience use World.ActivateScene().  
    /// </summary>
    public struct ActivateSceneComponent
    {
        public bool IsMain;
    }
}