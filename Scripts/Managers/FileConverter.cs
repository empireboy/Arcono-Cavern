using System.Collections.Generic;

namespace Arcono.Editor.Managers
{
	public class FileConverterManager
	{
        public static Dictionary<string, IFileConverter> FileConverters { get; set; }

        public FileConverterManager()
		{
            if (FileConverters == null)
                InitializeFileConverters();
		}

        private void InitializeFileConverters()
		{
			FileConverters = new Dictionary<string, IFileConverter>
			{
				{ ".json", new JsonConverter() }
			};
		}
    }
}
