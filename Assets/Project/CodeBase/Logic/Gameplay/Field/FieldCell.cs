using Assets.Project.CodeBase.StaticData.Cubes;
using System;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldCell : BaseObject
    {
        private IField _field;

        [SerializeField] private SpriteRenderer spriteRenderer;
        private int Layer;

        public void Construct(CubeData item)
        {

        }
        public void SetDependency(IField field)
        {
            _field = field;
        }

        public void SetLayer(int layer)
        {
            Layer = layer;
            spriteRenderer.sortingOrder = Layer;
        }
        public void SetSize(float size)
        {
            float newSize = size / 2;
            transform.localScale = new Vector3(size / 2, size / 2, size / 2);
        }
        public void SetPosition(Vector2 gridToPosition)
        {
            Vector3 pos = gridToPosition;
            localPosition = pos;
        }
  

        public void SetGridPosition(Vector2Int position)
        {
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


    }
}
