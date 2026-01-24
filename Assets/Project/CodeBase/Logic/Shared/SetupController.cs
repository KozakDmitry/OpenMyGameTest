using Assets.Project.CodeBase.Infostructure.Services;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Shared
{
    public class SetupController : MonoBehaviour, IObject
    {
        [SerializeField]
        private List<InitializableWindow> InitializableWindowList;


        private void Awake()
        {
            AllServices.Container.RegisterObject(this);
        }
        private void OnDestroy()
        {
            AllServices.Container.DeleteObject(this);
        }

        public async UniTask Initialize()
        {
            foreach (var item in InitializableWindowList)
            {
                await item.Initialize();
            }

            foreach (var item in InitializableWindowList)
            {
                await item.AfterInitialize();
            }
        }
    }
}
