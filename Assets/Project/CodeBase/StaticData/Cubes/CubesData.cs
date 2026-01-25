using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData.Cubes
{
    [CreateAssetMenu(fileName = "CubeData", menuName = "StaticData/Field/CubeData")]
    public class CubesData : ScriptableObject
    {
        public List<CubeData> cubeData;
    }


    [Serializable]
    public class CubeData
    {
        public int id;
        public Transform basePrefab;
    }
}
