namespace pigeon_lib.Utils
{
	public interface IAppOptions
	{
		public int ListLimit { get; set; }
	}

	public class AppOptions : IAppOptions
	{
		public int ListLimit { get; set; }
	}
}
