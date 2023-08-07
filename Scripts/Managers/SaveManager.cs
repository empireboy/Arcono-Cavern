using System;
using System.IO;

namespace Arcono.Editor.Managers
{
    public class SaveManager : FileConverterManager
    {
        public delegate void SaveEvent();
        public event SaveEvent OnSave;

        public void Save<T>(string fileName, T saveData, bool backup = true)
        {
            string fileExtension = Path.GetExtension(fileName);

            // Save saveData using serialization
            string saveDataString = FileConverters[fileExtension].SerializeObject(saveData);
            File.WriteAllText(fileName, saveDataString);

            if (backup)
                CreateBackup(fileName, saveDataString);

            OnSave?.Invoke();
        }

        private void CreateBackup(string fileName, string saveDataJson)
        {
            int backupIndex = 1;

            while (File.Exists(GetBackupFileName(fileName, backupIndex)))
            {
                backupIndex++;
            }

            File.WriteAllText(GetBackupFileName(fileName, backupIndex), saveDataJson);
        }

        private string GetBackupFileName(string fileName, int index)
        {
            string fileExtension = Path.GetExtension(fileName);
            string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - fileExtension.Length);

            return fileNameWithoutExtension + "_Backup_" + index + fileExtension;
        }
    }
}
