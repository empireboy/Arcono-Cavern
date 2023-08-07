namespace Arcono.Editor
{
	public interface IFileConverter
	{
		string SerializeObject<T>(T data);
		T DeserializeObject<T>(string data);
	}
}
