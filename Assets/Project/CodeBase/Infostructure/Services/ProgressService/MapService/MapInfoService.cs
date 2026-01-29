using Assets.Project.CodeBase.Data;
using Assets.Project.CodeBase.Data.Progress;
using Assets.Project.CodeBase.Data.Progress.SaveData;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Field;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using static Assets.Project.CodeBase.Data.Progress.SaveData.MapInfo;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService
{
    public class MapInfoService : IMapInfoService
    {
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticDataService;

        private readonly LevelData _levelData;
        private readonly FieldConfigData _fieldConfigData;

        public MapInfoService(IProgressService progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _levelData = _staticDataService.ForFieldData();
            _fieldConfigData = _staticDataService.ForFieldConfig();
        }
        public LevelInfo GetCurrentLevelData()
        {
            return _staticDataService.ForFieldData().levelInfo.SingleOrDefault(x => x.LevelId == GetCurrentLevel())
                 ?? _staticDataService.ForFieldData().levelInfo[0];
        }
        public void ChangeLevelToNext()
        {
            if (_progressService.Progress.mapInfo.currentLevelId >= _levelData.levelInfo.Count - 1)
            {
                _progressService.Progress.mapInfo.currentLevelId = 0;
            }
            else
            {
                _progressService.Progress.mapInfo.currentLevelId++;
            }
            _progressService.Progress.mapInfo.levelCondition.Clear();
        }

        public int GetCurrentLevel() =>
            _progressService.Progress.mapInfo.currentLevelId;

        public FieldConfigData GetFieldConfig() =>
            _fieldConfigData;

        public bool TryGetPositionCondition(Vector2Int post, out int id)
        {
            LevelConditions item = _progressService.Progress.mapInfo.levelCondition.SingleOrDefault(x => x.pos == post);
            if (item.Equals(default(LevelConditions)) && !_progressService.Progress.mapInfo.levelCondition.Contains(default))
            {
                id = -1;
                return false;
            }

            id = item.id;
            return true;
        }

        public bool TryGetSavedPositions(out List<LevelConditions> save)
        {
            save = _progressService.Progress.mapInfo.levelCondition;
            return _progressService.Progress.mapInfo.levelCondition.Count > 0;
        }

        public void ClearSaveLevel()
        {
            _progressService.Progress.mapInfo.levelCondition.Clear();
        }
    }
}

