using Assets.Project.CodeBase.Logic.Gameplay.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.Progress.SaveData
{
    [Serializable]
    public class MapInfo 
    {
        public int currentLevelId;
        public List<LevelConditions> levelCondition;

        public MapInfo()
        {
            currentLevelId = 0;
            levelCondition = new();
        }
        public void FromSave(MapInfo mapInfo)
        {
            if (mapInfo == null)
            {
                return;
            }
            if (mapInfo.currentLevelId != default)
            {
                currentLevelId = mapInfo.currentLevelId;
            }
            if (mapInfo.levelCondition != null && mapInfo.levelCondition.Count > 0)
            {
                levelCondition = new List<LevelConditions>(mapInfo.levelCondition);
            }
        }

       
    }

    [Serializable]
    public struct LevelConditions
    {
        public Vector2Int pos;
        public int id;
        public CubeStatus status;
        public LevelConditions(Vector2Int position, int idCurr,CubeStatus cubeStatus)
        {
            pos = position;
            id = idCurr;
            status = cubeStatus;
        }
    }
}
