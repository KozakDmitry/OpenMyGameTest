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
                for (int i = 0; i < cellsToLand.Count; i++)
                {
                    _field.StartToFall(cellsToLand[i]);
                }

            }




            for (int i = 0; i < _matrix.Height; i++)
            {
                for (int k = 0; k < _matrix.Width; k++)
                {
                    if (_matrix[k, i] == null)
                    {
                        CheckCells();
                        continue;
                    }
                    if ((lastCheckedCells.Count == 0 || _matrix[k, i].Id == lastCheckedCells[^1].Id))
                    {
                        if (_matrix[k, i].CubeStatus == CubeStatus.Idle)
                        {
                            lastCheckedCells.Add(_matrix[k, i]);
                        }
                        else
                        {
                            CheckCells();
                        }
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
                    }
                    startHeight = k;
                }

            }
        }

        private void CheckCells()
        {
            if (lastCheckedCells.Count >= 3)
            {
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
