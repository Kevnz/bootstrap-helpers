using Nancy;

namespace Bootstrap.Helpers.Samples.Modules
{
	public class HomeModule : NancyModule 
	{
		public HomeModule()
		{
			Get["/"] = _ => View["index"];
		}
	}
}