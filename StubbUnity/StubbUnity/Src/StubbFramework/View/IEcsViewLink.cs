using Leopotam.Ecs;
using StubbUnity.StubbFramework.Common;

namespace StubbUnity.StubbFramework.View
{
    public interface IEcsViewLink : IEntityContainer
    {
        string Name { get; }
        int TypeId { get; set; }
        /// <summary>
        /// An instance of the World where this IEcsViewLink belongs to.
        /// </summary>
        EcsWorld World { get; }
    }
}