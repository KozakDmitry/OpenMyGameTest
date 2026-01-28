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

        public LevelState()
        {
            currentLevelId = 0;
           
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

        }

    }

 


}
