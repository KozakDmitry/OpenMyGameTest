using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Assets.Project.CodeBase.Logic.Gameplay
{
    public class CameraSizeProvider : InitializableWindow, IObject
    {
        [SerializeField]
        private Camera _cam;
        public override UniTask Initialize()
        {
            AllServices.Container.RegisterObject(this);
            return base.Initialize();
        }

        private void OnDestroy()
        {
            AllServices.Container.DeleteObject(this);
        }

        public Rect GetFieldSize()
        {
            Vector2 pos = _cam.transform.position;
            var cameraOrthographicSize = _cam.orthographicSize;
            var size = new Vector2(_cam.aspect * cameraOrthographicSize, cameraOrthographicSize);
            return new Rect(pos - size, size * 2);
        }
    }
}
