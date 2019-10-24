using StubbFramework;
using StubbUnity.Services;

namespace StubbUnity
{
    public class UnitySystemHeadFeature : SystemHeadFeature
    {
        protected override void SetupSystems()
        {
            base.SetupSystems();
            Add(new InitializeServiceSystem());
        }
    }
}