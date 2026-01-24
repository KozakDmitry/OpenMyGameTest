using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.LevelInfo
{
    [Serializable]
    public class LevelState
    {
        public int currentLevelId;
        public List<LevelConditions> levelCondition;

        public LevelState()
        {
            currentLevelId = 0;
            levelCondition = new();
        }

        public void FromSave(LevelState levelInfo)
        {
            if (levelInfo == null)
            {
                return;
            }
            if (levelInfo.currentLevelId != default)
            {
                currentLevelId = levelInfo.currentLevelId;
            }
            if (levelInfo.levelCondition != null && levelInfo.levelCondition.Count > 0)
            {
                levelCondition = new List<LevelConditions>();
            }
        }
    }

    [Serializable]
    public struct LevelConditions
    {
        public Vector2Int pos;
        public int id;
    }


}
