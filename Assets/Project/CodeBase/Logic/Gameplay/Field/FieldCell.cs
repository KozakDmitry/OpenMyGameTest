using Assets.Project.CodeBase.StaticData.Cubes;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldCell : BaseObject
    {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private IField _field;
        private CubeStatus cubeStatus;
        private Vector2Int matrixPosition;
        private int layer;


        public int Layer => layer;
        public CubeStatus CubeStatus => cubeStatus;
        public Vector2Int MatrixPosition => matrixPosition;

        public void SetDependency(IField field) =>
            _field = field;
        
        public void SetLayer(int layer)
        {
            this.layer = layer;
            spriteRenderer.sortingOrder = this.layer;
        }

        public bool IsCubeAvailable() =>
            cubeStatus == CubeStatus.Idle;
        public void SetCubeStasus(CubeStatus status) =>
            cubeStatus = status;


        public void SetSize(float size) =>
            transform.localScale = new Vector3(size, size, size);
        public void SetPosition(Vector2 gridToPosition) => 
            localPosition = gridToPosition;


        public void SetGridPosition(Vector2Int position)
        {
            matrixPosition = position;
            SetPosition(_field.GridToPosition(position));
        }

        public override void Remove()
        {
            if (_field != null)
            {
                _field.RemoveView(this);
            }

            base.Remove();
        }

        public void MoveToPoint(int Layer,Vector2Int matrixPosition)
        {
            cubeStatus = CubeStatus.Moving;
            SetLayer(Layer);
            transform.DOMove(_field.GridToPosition(matrixPosition), 0.5f).OnComplete(() =>
            {
                this.matrixPosition = matrixPosition;
                cubeStatus = CubeStatus.Idle;

                //Тут надо буде чекать на нормалайз
            });

        }
    }

    public enum CubeStatus
    {
        Idle,
        Moving,
        Falling,
        Destroying
    }


}
