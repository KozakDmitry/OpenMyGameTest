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

        public CubeFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
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
