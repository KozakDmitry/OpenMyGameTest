using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.StaticData;

namespace Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory
{
    public class BackgroundFactory :IBackgroundFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;



        public BackgroundFactory(IStaticDataService staticData, IAssets assets)
        {
            _staticData = staticData;
            _assets = assets;
        }


    }
}
