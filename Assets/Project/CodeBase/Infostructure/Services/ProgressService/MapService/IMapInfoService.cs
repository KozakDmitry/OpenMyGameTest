using Assets.Project.CodeBase.Data.Progress.SaveData;
using Assets.Project.CodeBase.StaticData.Field;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService
{
    public interface IMapInfoService : IService
    {
        void ChangeLevelToNext();
        void ClearSaveLevel();
        int GetCurrentLevel();
        LevelInfo GetCurrentLevelData();
        FieldConfigData GetFieldConfig();
        bool TryGetPositionCondition(Vector2Int post, out int id);
        bool TryGetSavedPositions(out List<LevelConditions> save);
    }
}