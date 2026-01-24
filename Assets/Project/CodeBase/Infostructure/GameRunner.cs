using System.Collections;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure
{
    public class GameRunner : MonoBehaviour
    {
        public Bootstrapper bootstrapperPrefab;
        private void Awake()
        {
            Bootstrapper bootstrapper = FindFirstObjectByType<Bootstrapper>();
            if (bootstrapper == null)
            {
                Instantiate(bootstrapperPrefab); 
            }
        }
    }
}