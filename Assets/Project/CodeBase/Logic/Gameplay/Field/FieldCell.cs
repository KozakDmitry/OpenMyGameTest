using System;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldCell : BaseObject
    {
        private IField _field;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Construct()
        {
            
        }
        public void SetSize(float size)
        {
            spriteRenderer.size = new Vector2(size, size);
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void OnFieldInitialize(IField field)
        {
            _field = field;
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
