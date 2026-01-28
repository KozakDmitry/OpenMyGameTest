using Assets.Project.CodeBase.Infostructure.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public interface IField : IObject
    {
        List<FieldCell> GetFieldCells { get; }
        Grid<FieldCell> GetMatrix { get; }

        event Field.OnFieldCellMoved OnCellChanged;
        void AddCell(FieldCell viewCell, Vector2Int position);
        Vector2Int GetCellByPos(Vector3 worldPosition);
        Vector2 GridToPosition(Vector2Int cell);
        void RemoveView(FieldCell viewCell);
        void SetupFieldSize(Bounds rect);
        void InitializeGrid(Vector2Int size);
        void TrySwapTwoCubes(FieldCell result, Vector2 secondDirection);
        void StartToFall((FieldCell cell, int height) value);
        void OnMoveEnd(FieldCell fieldCell);
        void StartDestroyCells(List<FieldCell> fieldCell);
    }
}