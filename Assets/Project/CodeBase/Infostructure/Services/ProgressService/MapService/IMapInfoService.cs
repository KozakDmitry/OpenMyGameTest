using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService
{
    public interface IMapInfoService : IService
    {
        bool TryGetPositionCondition(Vector2Int post, out int id);
    }
}