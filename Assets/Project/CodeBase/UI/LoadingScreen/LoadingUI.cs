
using Assets.Project.CodeBase.Infostructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.CodeBase.UI.LoadingScreen
{
    public class LoadingUI : MonoBehaviour, IObject
    {
        public Slider slider;

        private void Awake()
        {
            AllServices.Container.RegisterObject(this);
        }

        private void OnDestroy()
        {
            AllServices.Container.DeleteObject(this);
        }

    }
}
