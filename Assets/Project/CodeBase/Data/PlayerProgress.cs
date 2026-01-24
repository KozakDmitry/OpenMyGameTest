using Assets.Project.CodeBase.Data.LevelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.CodeBase.Data
{
    public class PlayerProgress
    {
        public LevelState levelInfo;
        public PlayerProgress()
        {
            levelInfo = new LevelState();
        }

        public void SetFromSave(PlayerProgress progress)
        {
            levelInfo?.FromSave(progress.levelInfo);
        }
    }
}
