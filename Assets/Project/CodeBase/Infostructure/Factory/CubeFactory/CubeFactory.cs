using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.Logic.Gameplay.Field;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Cubes;
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
            CubeData data = _staticData.ForCubeData(id);
            Transform viewObstacle = Object.Instantiate(data.basePrefab);
            FieldCell cell = viewObstacle.GetComponent<FieldCell>();
            cell.Construct(data.id);
            return cell;
        }
    }

}
