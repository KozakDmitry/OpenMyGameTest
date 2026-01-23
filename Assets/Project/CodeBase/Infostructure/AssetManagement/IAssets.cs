using Assets.Project.CodeBase.Infostructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.AssetManagement
{

    public interface IAssets : IService
    {
        GameObject InstantiatePrefab(string path);
        GameObject InstantiatePrefab(string path, Vector3 position);
        GameObject InstantiatePrefab(string path, Vector3 position, Quaternion quaternion, Transform parent);
        UniTask<GameObject> InstantiatePrefabAsync(string path, Transform parent);
        UniTask<GameObject> InstantiatePrefabAsync(string path);
    }
}