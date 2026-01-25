using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.Logic.Gameplay.Field;
using Assets.Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.Factory.CubeFactory
{
    public class CubeFactory : ICubeFactory
    {



        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;



        public CubeFactory(IStaticDataService staticData, IAssets assets)
        {
            _staticData = staticData;
            _assets = assets;
        }

        public FieldCell CreateFieldCell(int id)
        {
            var item = _staticData.ForCubeData(id);
            Transform viewObstacle = Object.Instantiate(item.basePrefab);
            var it = viewObstacle.GetComponent<FieldCell>();
            it.Construct(item);
            return it;
        }
    }

}
