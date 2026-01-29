using Assets.Project.CodeBase.Data.Progress.SaveData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.CodeBase.Data.Progress
{
    public class PlayerProgress
    {
        public MapInfo mapInfo;

        public PlayerProgress()
        {
            mapInfo = new MapInfo();
        }

        public void SetFromSave(PlayerProgress progress)
        {
            if (progress == null)
            {
                return;
            }
            mapInfo?.FromSave(progress.mapInfo);
        }
    }
}
