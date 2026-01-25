using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.Progress
{
    public class MapInfo
    {
        public List<LevelConditions> levelCondition;

        public MapInfo()
        {
            levelCondition = new();
        }
        public void FromSave(MapInfo mapInfo)
        {
            if (mapInfo == null)
            {
                return;
            }
            if (mapInfo.levelCondition != null && mapInfo.levelCondition.Count > 0)
            {
                levelCondition = new List<LevelConditions>();
            }
        }

        [Serializable]
        public struct LevelConditions
        {
            public Vector2Int pos;
            public int id;
        }
    }
}
