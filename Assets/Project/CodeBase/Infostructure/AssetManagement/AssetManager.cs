using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Assets.Project.CodeBase.Infostructure.AssetManagement
{
    public class AssetManager : IAssets
    {
        public async UniTask<GameObject> InstantiatePrefabAsync(string path)
        {
            GameObject prefab = (GameObject)await Resources.LoadAsync<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        public async UniTask<GameObject> InstantiatePrefabAsync(string path, Transform parent)
        {
            GameObject prefab = (GameObject)await Resources.LoadAsync<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject InstantiatePrefab(string path, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);

        }
        public GameObject InstantiatePrefab(string path, Vector3 position, Quaternion quaternion, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, quaternion, parent);

        }

    }
}