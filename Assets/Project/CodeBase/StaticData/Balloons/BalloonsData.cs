using Assets.Project.CodeBase.Data.Balloons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData.Balloons
{
 
    [CreateAssetMenu(fileName = "BalloonsData", menuName = "StaticData/BackGround/BalloonsData")]
    public class BalloonsData : ScriptableObject
    {
        public BallonsSpawnSettings spawnSettings;
        public List<MovableBalloon> ballons;
    }


    [Serializable]
    public class BallonsSpawnSettings
    {
        [Tooltip("In milliseconds between attempts to spawn")]
        public Vector2Int randomTimeRange;
        [Tooltip("Max ballons available in the same time")]
        public int maxBallonsAvailable;
    }
}
