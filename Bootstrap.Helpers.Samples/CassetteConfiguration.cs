using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Bootstrap.Helpers.Samples
{
	/// <summary>
	/// Configures the Cassette asset bundles for the web application.
	/// </summary>
	public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
	{
		public void Configure(BundleCollection bundles)
		{
			bundles.AddPerSubDirectory<StylesheetBundle>("Public");
			bundles.AddPerIndividualFile<ScriptBundle>("public/js/");
		}
	}
}