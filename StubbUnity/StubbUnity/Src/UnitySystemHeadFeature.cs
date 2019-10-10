using StubbFramework;
using StubbUnity.Services;

namespace StubbUnity
{
    public class UnitySystemHeadFeature : SystemHeadFeature
    {
        public UnitySystemHeadFeature() : base()
        {
            Add(new InitializeServiceSystem());
        }
    }
}