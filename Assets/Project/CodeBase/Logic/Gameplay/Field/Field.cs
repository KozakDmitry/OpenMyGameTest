using Assets.Project.CodeBase.Data.Progress;
using Assets.Project.CodeBase.Data.Progress.SaveData;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class Field : MonoBehaviour, IField
    {

        [SerializeField] private Transform container;

        private Bounds fieldBounds;

        private readonly List<FieldCell> _cells = new List<FieldCell>();
        private Grid<FieldCell> _matrix;
        private float _cellWidth;
        private Vector2 _placeOffset;
        private Vector2 _cellSize;

        public delegate void OnFieldCellMoved();
        public event OnFieldCellMoved OnCellChanged;

        public List<FieldCell> GetFieldCells =>
            _cells;

        public Grid<FieldCell> GetMatrix =>
            _matrix;

        public void SetupFieldSize(Bounds rect) =>
            fieldBounds = rect;
        public void InitializeGrid(Vector2Int size)
        {
            ClearCells();
            _matrix = new(size);
            Vector2 fieldSize = fieldBounds.size;
            _cellWidth = Math.Min(fieldBounds.size.x / size.x, fieldBounds.size.y / size.y);
            _cellSize = new Vector2(_cellWidth, _cellWidth);
            _placeOffset = (Vector2)fieldBounds.min + (fieldSize - _cellSize * size) * 0.5f;
            AllServices.Container.RegisterObject(this as IField);

        }


        public void AddCell(FieldCell viewCell, Vector2Int position)
        {
            if (_cells.Contains(viewCell))
            {
                return;
            }
            viewCell.transform.SetParent(container);
            viewCell.SetDependency(this);
            viewCell.SetGridPosition(position);
            viewCell.SetLayer(GetLayerForCell(position));
            viewCell.SetSize(_cellWidth * 0.5f);
            _matrix[position] = viewCell;
            _cells.Add(viewCell);
        }

        public Vector2 GridToPosition(Vector2Int cell)
        {
            float halfCellWidth = _cellWidth * 0.5f;
            return new Vector2(cell.x * _cellWidth + halfCellWidth, cell.y * _cellWidth + halfCellWidth) + _placeOffset;
        }

        private (Vector2Int result, bool isNotUp) newDirection;
        public void TrySwapTwoCubes(FieldCell firstCell, Vector2 secondDirection)
        {
            newDirection = GetDirection(firstCell.MatrixPosition, secondDirection);
            if ((IsThereCubeAvailable(newDirection, out FieldCell secondCell)))
            {
                if (secondCell != null)
                {
                    if (secondCell.IsCubeAvailable())
                    {
                        SwapCubes(firstCell, secondCell);
                    }
                }
                else
                {
                    SendCubeToSide(firstCell, newDirection.result);
                }
            }
        }

        private async void SendCubeToSide(FieldCell firstCell, Vector2Int secondDirection)
        {
            _matrix[secondDirection] = firstCell;
            _matrix[firstCell.MatrixPosition] = null;
            await firstCell.MoveToPointAsync(GetLayerForCell(secondDirection), secondDirection, CubeStatus.Moving);
            OnCellChanged?.Invoke();

        }


        private List<UniTask> uniTasks;
        private async void SwapCubes(FieldCell firstCell, FieldCell secondCell)
        {
            uniTasks = new();
            _matrix[firstCell.MatrixPosition] = secondCell;
            _matrix[secondCell.MatrixPosition] = firstCell;
            uniTasks.Add(firstCell.MoveToPointAsync(GetLayerForCell(secondCell.MatrixPosition), secondCell.MatrixPosition, CubeStatus.Moving));
            uniTasks.Add(secondCell.MoveToPointAsync(GetLayerForCell(firstCell.MatrixPosition), firstCell.MatrixPosition, CubeStatus.Moving));
            await UniTask.WhenAll(uniTasks);
            OnCellChanged?.Invoke();
        }

        private bool IsThereCubeAvailable((Vector2Int nextPosition, bool isNotUp) vect, out FieldCell fieldCell)
        {
            fieldCell = null;
            if (vect.nextPosition.x >= 0 && vect.nextPosition.y >= 0 &&
                vect.nextPosition.x < _matrix.Width && vect.nextPosition.y < _matrix.Height)
            {
                fieldCell = _matrix[vect.nextPosition];
                if (fieldCell != null || vect.isNotUp)
                {
                    return true;
                }
            }

            return false;
        }


        public async void StartDestroyCells(List<FieldCell> fieldCell)
        {

            List<UniTask> awaitDestroy = new List<UniTask>();
            for (int i = 0; i < fieldCell.Count; i++)
            {
                awaitDestroy.Add(fieldCell[i].StartDestroing());
            }
            await UniTask.WhenAll(awaitDestroy);
            OnCellChanged?.Invoke();
        }


        public async void StartToFall((FieldCell cell, int height) fieldCell)
        {
            Vector2Int newPosition = new Vector2Int(fieldCell.cell.MatrixPosition.x, fieldCell.height);
            _matrix[fieldCell.cell.MatrixPosition] = null;
            _matrix[newPosition] = fieldCell.cell;
            await fieldCell.cell.MoveToFallAsync(GetLayerForCell(newPosition), newPosition);
            OnCellChanged?.Invoke();
        }

        private int GetLayerForCell(Vector2Int newPosition) =>
            newPosition.x + 1 + newPosition.y * (_matrix.Width - 1);

        public void OnMoveEnd(FieldCell fieldCell) =>
            OnCellChanged?.Invoke();

        private (Vector2Int result, bool isNotUp) GetDirection(Vector2Int position, Vector2 dir)
        {
            bool isNotUp = true;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0) position.x++;
                else position.x--;
            }
            else
            {
                if (dir.y > 0)
                {
                    isNotUp = false;
                    position.y++;
                }
                else position.y--;
            }
            return (position, isNotUp);
        }

        public Vector2Int GetCellByPos(Vector3 worldPosition)
        {
            Vector2 deltaCell = ((Vector2)worldPosition - _placeOffset) / _cellWidth;
            if (deltaCell.x < 0 || deltaCell.y < 0)
            {
                return new Vector2Int(-1, -1);
            }
            return new Vector2Int((int)deltaCell.x, (int)deltaCell.y);
        }

        public void RemoveView(FieldCell viewCell)
        {
            var removeIndex = _cells.IndexOf(viewCell);
            if (removeIndex >= 0)
            {
                _matrix[viewCell.MatrixPosition] = null;
                _cells.RemoveAt(removeIndex);
            }
        }

        private void ClearCells()
        {
            for (int i = _cells.Count - 1; i >= 0; i--)
            {
                _cells[i].Remove();
            }
            _cells.Clear();
        }



    }
}
