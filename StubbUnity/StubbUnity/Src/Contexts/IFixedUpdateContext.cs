using StubbFramework;

namespace StubbUnity.Contexts
{
    /// <summary>
    /// Context which is implemented this interface will be processed only on FixedUpdate loop.
    /// </summary>
    public interface IFixedUpdateContext : IStubbContext
    {
    }
}