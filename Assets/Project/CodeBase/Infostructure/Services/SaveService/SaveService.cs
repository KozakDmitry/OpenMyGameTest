
using Assets.Project.CodeBase.Data;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Project.CodeBase.Infostructure.Services.SaveService
{
    public class SaveService : ISaveService
    {
        private const string SAVE_SLOT_NAME = "Save";
        private IProgressService _progressService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();


        public SaveService(IProgressService progressService)
        {
            _progressService = progressService;

        }
        public void RegisterWriter(ISavedProgress writer) =>
            ProgressWriters.Add(writer);
        public void RemoveWriter(ISavedProgress writer) =>
            ProgressWriters.Remove(writer);
        public void Save()
        {
            foreach (ISavedProgress reader in ProgressReaders)
            {
                reader.UpdateProgress(_progressService.Progress);
            }
            string item = JsonUtility.ToJson(_progressService.Progress);
            PlayerPrefs.SetString(SAVE_SLOT_NAME, item);
        }
        public void Load()
        {
            string item = PlayerPrefs.GetString(SAVE_SLOT_NAME);
            if (item != null && item != default)
            {
                PlayerProgress progress = JsonUtility.FromJson<PlayerProgress>(item);
                _progressService.Progress.SetFromSave(progress);
            }
        }


    }


}
