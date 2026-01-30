using Assets.Project.CodeBase.Extentions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldCell : BaseObject
    {
        private const string DESTROY_TRIGGER_NAME = "ToDestroy";
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Animator animator;

        private IField _field;
        private CubeStatus cubeStatus;
        private Vector2Int matrixPosition;
        private int layer,
                    id;


        public int Layer => layer;

        public int Id => id;
        public CubeStatus CubeStatus => cubeStatus;
        public Vector2Int MatrixPosition => matrixPosition;


        public void Construct(int id) =>
            this.id = id;





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
            _field?.RemoveView(this);
            base.Remove();
        }

        public async UniTask MoveToPointAsync(int layer, Vector2Int matrixPosition, CubeStatus status)
        {
            cubeStatus = status;
            SetLayer(layer);
            await MoveCellAsync(matrixPosition);
        }

        public async UniTask MoveToFallAsync(int layer, Vector2Int matrixPosition)
        {
            SetLayer(layer);
            cubeStatus = CubeStatus.Falling;
            this.matrixPosition = matrixPosition;
            await MoveCellAsync(matrixPosition);
        }
        private async UniTask MoveCellAsync(Vector2Int matrixPosition)
        {
            this.matrixPosition = matrixPosition;
            await transform.DOMove(_field.GridToPosition(matrixPosition), 0.5f)
                          .AsyncWaitForCompletion();
            cubeStatus = CubeStatus.Idle;
        }


        public async UniTask StartDestroing()
        {
            cubeStatus = CubeStatus.Destroying;
            animator.SetTrigger(DESTROY_TRIGGER_NAME);
            await animator.AwaitAnimationComplete();
            Remove();
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
