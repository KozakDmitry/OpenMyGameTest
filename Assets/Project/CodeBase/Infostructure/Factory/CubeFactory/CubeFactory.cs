using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.StaticData;

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


    }
    
}
