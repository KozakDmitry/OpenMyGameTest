using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public interface IField
    {
        List<FieldCell> GetFieldCells { get; }
        Grid<FieldCell> GetMatrix { get; }

        event Field.OnFieldCellMoved OnCellChanged;
        void AddCell(FieldCell viewCell, Vector2Int position);
        Vector2Int GetCellByPos(Vector3 worldPosition);
        Vector2 GridToPosition(Vector2Int cell);
        void RemoveView(FieldCell viewCell);
        void SetupFieldSize(Bounds rect);
        void  InitializeGrid(Vector2Int size);
        void TrySwapTwoCubes(FieldCell result, Vector2 secondDirection);
        void StartDestroy(FieldCell fieldCell);
        void StartToFall(FieldCell fieldCell);
        void OnMoveEnd(FieldCell fieldCell);
    }
}