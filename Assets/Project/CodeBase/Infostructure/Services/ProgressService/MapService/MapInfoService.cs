using Assets.Project.CodeBase.Data;
using Assets.Project.CodeBase.Data.Progress;
using System.Linq;
using System.Numerics;
using UnityEngine;
using static Assets.Project.CodeBase.Data.Progress.MapInfo;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService
{
    public class MapInfoService : IMapInfoService
    {
        public MapInfo mapInfo;

        public MapInfoService()
        {
            mapInfo = new MapInfo();

        }


        public bool TryGetPositionCondition(Vector2Int post, out int id)
        {
            LevelConditions item = mapInfo.levelCondition.SingleOrDefault(x => x.pos == post);
            if (item.Equals(default(LevelConditions)) && !mapInfo.levelCondition.Contains(default))
            {
                id = -1;
                return false;
            }

            id = item.id;
            return true;
        }
    }
}

