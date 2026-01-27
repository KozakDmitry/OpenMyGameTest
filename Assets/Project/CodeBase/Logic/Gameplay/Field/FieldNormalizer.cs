using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldNormalizer : InitializableWindow
    {
        private IField _field;

        private List<FieldCell> lastCheckedCells;
        private List<FieldCell> potencialCombinations;
        private List<FieldCell> cellsToLand;
        private bool IsNormalazing;

        public override UniTask Initialize()
        {
            _field = GetComponent<IField>();
            IsNormalazing = false;
            lastCheckedCells = new();
            potencialCombinations = new();
            cellsToLand = new();
            _field.OnCellChanged += TryToNormalize;
            return base.Initialize();

        }

        private void OnDestroy()
        {
            if (_field != null)
            {
                _field.OnCellChanged -= TryToNormalize;
            }
        }

        public void TryToNormalize()
        {
            if (IsNormalazing)
            {
                return;
            }
            else
            {
                IsNormalazing = true;
                lastCheckedCells.Clear();
                potencialCombinations.Clear();
            }
            var _matrix = _field.GetMatrix;
            for (int i = 0; i < _matrix.Height; i++)
            {
                for (int k = 0; k < _matrix.Width; k++)
                {
                    if (_matrix[k, i] == null)
                    {
                        CheckCells();
                        continue;
                    }
                    Debug.Log(_matrix[k, i].MatrixPosition);
                    if (_matrix[k, i].CubeStatus == CubeStatus.Idle &&
                        (lastCheckedCells.Count == 0 || _matrix[k, i].Id == lastCheckedCells[^1].Id))
                    {
                        lastCheckedCells.Add(_matrix[k, i]);
                    }
                    else
                    {
                        CheckCells();
                        lastCheckedCells.Add(_matrix[k, i]);
                    }
                }
                CheckCells();
            }

            for (int k = 0; k < _matrix.Width; k++)
            {
                for (int i = 0; i < _matrix.Height; i++)
                {
                    if (_matrix[k, i] == null)
                    {
                        CheckCells();
                        continue;
                    }
                    if (_matrix[k, i].CubeStatus == CubeStatus.Idle &&
                        (lastCheckedCells.Count == 0 || _matrix[k, i].Id == lastCheckedCells[^1].Id))
                    {
                        lastCheckedCells.Add(_matrix[k, i]);
                    }
                    else
                    {
                        CheckCells();
                        lastCheckedCells.Add(_matrix[k, i]);
                    }
                }
                CheckCells();
            }
            for (int i = 0; i < potencialCombinations.Count; i++)
            {
                _field.StartDestroy(potencialCombinations[i]);
            }

            int r = 0;
            for (int i = 0; i < _matrix.Width; i++)
            {
                r = 0;
                if (_matrix[i, r] == null)
                {
                    SetAllToDown(_matrix, i, ref r);
                }
            }
            for (int i = 0; i < cellsToLand.Count; i++)
            {
                _field.StartToFall(cellsToLand[i]);
            }

            IsNormalazing = false;
        }

        private void SetAllToDown(Grid<FieldCell> matrix, int i, ref int k)
        {
            for (; k < matrix.Height; i++)
            {
                if (matrix[i, k] != null && matrix[i, k].IsCubeAvailable())
                {
                    cellsToLand.Add(matrix[i, k]);
                }
                else
                {
                    return;
                }
            }
        }

        private void CheckCells()
        {
            if (lastCheckedCells.Count >= 3)
            {
                Debug.Log("Cells to destroy: " + lastCheckedCells.Count);
                for (int i = 0; i < lastCheckedCells.Count; i++)
                {
                    potencialCombinations.Add(lastCheckedCells[i]);
                    lastCheckedCells[i].SetCubeStasus(CubeStatus.Destroying);
                }
            }

            lastCheckedCells.Clear();
            return;
        }
    }
}
