using Nancy;
using Nancy.Diagnostics;

namespace Bootstrap.Helpers.Samples
{



	public class Bootstrapper : DefaultNancyBootstrapper
	{
		public Bootstrapper()
		{
			Cassette.Nancy.CassetteNancyStartup.OptimizeOutput = true;

		}
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("/", "public"));

			this.Conventions.AcceptHeaderCoercionConventions.Add((acceptHeaders, ctx) =>
			{

				// Modify the acceptHeaders by adding, removing or updating the current
				// values.
				var fb = "*/*";

				return acceptHeaders;
			});
		}
		protected override Nancy.Diagnostics.DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new DiagnosticsConfiguration { Password = @"nancy" }; }
		}
	}

}