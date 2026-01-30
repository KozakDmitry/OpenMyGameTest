using Assets.Project.CodeBase.Infostructure.Services;
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
    public class FieldNormalizer : InitializableWindow, IFieldNormalizer
    {
        private IField _field;

        private List<FieldCell> lastCheckedCells;
        private List<FieldCell> potencialCombinations;
        private List<(FieldCell, int)> cellsToLand;
        private bool IsNormalazing, isSmtgNormalized;

        public override UniTask Initialize()
        {
            AllServices.Container.RegisterObject(this as IFieldNormalizer);
            _field = GetComponent<IField>();
            IsNormalazing = false;
            lastCheckedCells = new();
            potencialCombinations = new();
            cellsToLand = new();
            return base.Initialize();

        }

        private void OnDestroy()
        {
            if (_field != null)
            {
                AllServices.Container.DeleteObject(this as IFieldNormalizer);
                AllServices.Container.DeleteObject(_field);
            }
        }


        public bool TryToNormalize()
        {
            if (IsNormalazing)
            {
                return true;
            }
            else
            {
                IsNormalazing = true;
                isSmtgNormalized = false;
                lastCheckedCells.Clear();
                potencialCombinations.Clear();
                cellsToLand.Clear();
            }
            var _matrix = _field.GetMatrix;


            for (int i = 0; i < _matrix.Width; i++)
            {
                for (int k = 0; k < _matrix.Height - 1; k++)
                {
                    if (_matrix[i, k] == null)
                    {
                        SetAllToDown(_matrix, i, ref k);
                    }

                }
            }

            if (cellsToLand.Count > 0)
            {
                isSmtgNormalized = true;
                _field.StartToFall(cellsToLand);
            }


            for (int y = 0; y < _matrix.Height; y++)
            {
                CheckCombinationsInLine(_matrix, y, true);
            }

            for (int x = 0; x < _matrix.Width; x++)
            {
                CheckCombinationsInLine(_matrix, x, false);
            }
            if (potencialCombinations.Count > 0)
            {
                isSmtgNormalized = true;
                _field.StartDestroyCells(potencialCombinations);
            }



            IsNormalazing = false;
            return isSmtgNormalized;
        }

        private void SetAllToDown(Grid<FieldCell> matrix, int i, ref int k)
        {
            int startHeight = k;
            k++;
            for (; k < matrix.Height; k++)
            {
                if (matrix[i, k] != null)
                {
                    if (matrix[i, k].IsCubeAvailable())
                    {
                        cellsToLand.Add((matrix[i, k], startHeight));
                        matrix[i, k].SetCubeStasus(CubeStatus.Falling);
                        startHeight++;
                    }
                    else
                    {
                        startHeight = k;
                    }
                }

            }
        }
        private void CheckCombinationsInLine(Grid<FieldCell> matrix, int index, bool isHorizontal)
        {
            lastCheckedCells.Clear();
            for (int i = 0; i < (isHorizontal ? matrix.Width : matrix.Height); i++)
            {
                FieldCell cell = matrix[isHorizontal ? i : index,
                                        isHorizontal ? index : i];

                if (cell == null || cell.CubeStatus != CubeStatus.Idle)
                {
                    CheckCells();
                    continue;
                }

                if (lastCheckedCells.Count == 0 || lastCheckedCells[0].Id == cell.Id)
                {
                    lastCheckedCells.Add(cell);
                }
                else
                {
                    CheckCells();
                    lastCheckedCells.Add(cell);
                }
            }
            CheckCells();
        }

        private void CheckCells()
        {
            if (lastCheckedCells.Count >= 3)
            {
                potencialCombinations.AddRange(lastCheckedCells);
            }
            lastCheckedCells.Clear();
        }
    }
}
