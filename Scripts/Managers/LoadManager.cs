using System.IO;

namespace Arcono.Editor.Managers
{
	public class LoadManager : FileConverterManager
	{
        public delegate void LoadEvent(object saveData);
        public event LoadEvent OnLoad;

        public bool Load<T>(string fileName)
		{
            string fileExtension = Path.GetExtension(fileName);

            if (!File.Exists(fileName))
                return false;

            // Load saveData using deserialization
            string saveDataString = File.ReadAllText(fileName);
            T saveData = FileConverters[fileExtension].DeserializeObject<T>(saveDataString);

            OnLoad?.Invoke(saveData);

            return true;
        }
    }
}
