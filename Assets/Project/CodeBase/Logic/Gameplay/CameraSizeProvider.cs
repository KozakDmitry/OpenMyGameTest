using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Assets.Project.CodeBase.Logic.Gameplay
{
    public class CameraSizeProvider : InitializableWindow, ICameraSizeProvider
    {
        [SerializeField]
        private Camera _cam;
        public override UniTask Initialize()
        {
            AllServices.Container.RegisterObject(this as ICameraSizeProvider);
            return base.Initialize();
        }

        private void OnDestroy()
        {
            AllServices.Container.DeleteObject(this as ICameraSizeProvider);
        }
        public Vector3 ScreenToWorldPoint2D(Vector2 screenPosition) => 
            _cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));

        public Bounds GetCameraBounds()
        {
            float halfHeight = _cam.orthographicSize;
            float halfWidth = halfHeight * _cam.aspect;
            Vector3 center = _cam.transform.position;
            Vector3 size = new Vector3(halfWidth * 2f, halfHeight * 2f, 0f);

            return new Bounds(center, size);
        }
    }
}
