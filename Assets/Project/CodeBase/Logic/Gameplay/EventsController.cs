using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Gameplay.Field;
using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.CodeBase.Logic.Gameplay
{
    public class EventsController : InitializableWindow
    {
        private IField _field;
        private IFieldNormalizer _fieldNormalizer;
        public override async UniTask Initialize()
        {
            _field = await AllServices.Container.SingleAwait<IField>();
            _fieldNormalizer = await AllServices.Container.SingleAwait<IFieldNormalizer>();
            _field.OnCellChanged += CheckField;
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
            SaveCells();
            _fieldNormalizer.TryToNormalize();
        }

        private void SaveCells()
        {
            throw new NotImplementedException();
        }

        private void ChangeLevel()
        {
            throw new NotImplementedException();
        }
    }
}
