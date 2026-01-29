using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Assets.Project.CodeBase.Logic.Gameplay.Field;
using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;


namespace Assets.Project.CodeBase.Logic.Gameplay
{
    public class EventsController : InitializableWindow
    {
        private IField _field;
        private IFieldNormalizer _fieldNormalizer;
        private ISaveService _saveService;
        private IMapInfoService _mapInfoService;
        private ISceneService _sceneService;
        public override async UniTask Initialize()
        {
            _field = await AllServices.Container.SingleAwait<IField>();
            _fieldNormalizer = await AllServices.Container.SingleAwait<IFieldNormalizer>();
            _saveService = AllServices.Container.Single<ISaveService>();
            _mapInfoService = AllServices.Container.Single<IMapInfoService>();
            _sceneService = AllServices.Container.Single<ISceneService>();
            _field.OnCellChanged += CheckField;
        }

        public override UniTask AfterInitialize()
        {
            CheckField();
            return base.AfterInitialize();
        }
        private void OnDestroy()
        {
            if (_field != null)
            {
                _field.OnCellChanged -= CheckField;
            }
        }
        private void CheckField()
        {
            if (_field.GetFieldCells.Count == 0)
            {
                ChangeLevel();
                return;
            }
            if (!_fieldNormalizer.TryToNormalize())
            {
                SaveCells();
            }
        }

        private void SaveCells()
        {
            _saveService.Save();
        }

        private void ChangeLevel()
        {
            _mapInfoService.ChangeLevelToNext();
            _saveService.Save();
            _sceneService.LoadScene(SceneNames.Game);
        }
    }
}
