using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData.Field
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Field/LevelData")]
    public class LevelData : ScriptableObject
    {
        public List<LevelInfo> levelInfo = new();
    }

    [Serializable]
    public class LevelInfo
    {
        [SerializeField] int levelId;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 3;
        [SerializeField] private int[] flatMatrix = new int[9];

        public int LevelId => levelId;
        public int Rows => rows;
        public int Columns => columns;
        public int[] FlatMatrix => flatMatrix;
        public Vector2Int GetSize() =>
          new Vector2Int(columns, rows);

        public Vector2Int GetMatrixPosition(int flatIndex)
        {
            if (flatIndex < 0 || flatIndex >= flatMatrix.Length)
                throw new IndexOutOfRangeException("Индекс вне диапазона");
            return new Vector2Int(flatIndex % columns, rows - 1 - flatIndex / columns);
        }

    }
}
