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
          new Vector2Int(rows, columns);
        public int GetValue(Vector2Int value)
        {
            if (value.x < 0 || value.x >= rows || value.y < 0 || value.y >= columns)
                throw new IndexOutOfRangeException($"({value.x}, {value.y}) выходит за пределы [{rows}x{columns}]");
            return flatMatrix[value.x * columns + value.y];
        }
        public Vector2Int GetMatrixPosition(int flatIndex)
        {
            if (flatIndex < 0 || flatIndex >= flatMatrix.Length)
                throw new IndexOutOfRangeException("Индекс вне диапазона");
            return new Vector2Int(flatIndex / columns, flatIndex % columns);
        }

        public void SetValue(int row, int col, int value)
        {
            if (row < 0 || row >= rows || col < 0 || col >= columns)
                throw new IndexOutOfRangeException($"({row}, {col}) выходит за пределы [{rows}x{columns}]");
            flatMatrix[row * columns + col] = value;
        }

        public void Resize(int newRows, int newCols)
        {
            newRows = Mathf.Max(1, newRows);
            newCols = Mathf.Max(1, newCols);
            int[] newFlat = new int[newRows * newCols];
            for (int r = 0; r < Math.Min(rows, newRows); r++)
            {
                for (int c = 0; c < Math.Min(columns, newCols); c++)
                {
                    newFlat[r * newCols + c] = flatMatrix[r * columns + c];
                }
            }
            rows = newRows;
            columns = newCols;
            flatMatrix = newFlat;
        }
        public void EnsureValid()
        {
            if (flatMatrix == null || flatMatrix.Length != rows * columns)
            {
                flatMatrix = new int[rows * columns];
            }
        }

      
    }
}
