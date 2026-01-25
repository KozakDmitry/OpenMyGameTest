using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public interface IField
    {
        void AddCell(FieldCell viewCell, Vector2Int position);
        void ClearCells();
        Vector2Int GetCellByPos(Vector3 worldPosition);
        Vector2 GridToPosition(Vector2Int cell);
        void RemoveView(FieldCell viewCell);
        void SetupFieldSize(Bounds rect);
        void  InitializeGrid(Vector2Int size);
    }
}