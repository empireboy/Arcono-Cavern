using Newtonsoft.Json;

namespace Arcono.Editor
{
	public class JsonConverter : IFileConverter
	{
		public string SerializeObject<T>(T data)
		{
			return JsonConvert.SerializeObject(data);
		}

		public T DeserializeObject<T>(string data)
		{
			return JsonConvert.DeserializeObject<T>(data);
		}
	}
}
