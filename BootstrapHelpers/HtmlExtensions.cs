using BootstrapHelpers.Models;

namespace BootstrapHelpers
{
	using System.Collections.Generic; 
	using Nancy.ViewEngines.Razor;
	using System.Dynamic;
	using System.Reflection;

 
		public static class HtmlExtensions
		{
			public static IHtmlString SelectFromList<T, M>(this HtmlHelpers<T> helper, List<M> list, int? value)
			{
				var result = "<select id=\"\" name=\"\">";
				result += "<option value=\"\">Please Select</option>";
				if (list != null)
				{
					foreach (var item in list)
					{
						var idProp = item.GetType().GetProperty("Id", (BindingFlags.Instance | BindingFlags.Public));
						var id = idProp.GetValue(item);
						var nameProp = item.GetType().GetProperty("Name", (BindingFlags.Instance | BindingFlags.Public));
						var name = nameProp.GetValue(item);
						if (value.HasValue && (int)id == value)
						{
							result += "<option value=\"" + id + "\" selected=\"selected\">" +name + "</option>";
						}
						else
						{
							result += "<option value=\"" + id + "\">" + name + "</option>";
						}
					}
				}
				result += "</select>";
				return new NonEncodedHtmlString(result);
			}

			public static IHtmlString GetTable<T, M>(this HtmlHelpers<T> helper, List<M> list, string root, params string[] props)
			{
				var result = "<table class=\"table table-bordered\">";
				result += "<thead>";
				foreach (var propertyName in props)
				{
					result += "<th>" + propertyName + "</th>";
				}
				result += "<th> </th></thead>";
				if (list != null)
				{
					result += "<tbody>";
					foreach (var item in list)
					{
						result += "<tr>";
						foreach (var propertyName in props)
						{
							var prop = item.GetType().GetProperty(propertyName, (BindingFlags.Instance | BindingFlags.Public));
							if (propertyName == prop.Name)
							{
								result += "<td>" + prop.GetValue(item, null) + "</td>";
							}
						}
						var id = item.GetType().GetProperty("Id", (BindingFlags.Instance | BindingFlags.Public));
						result += "<td><a href=\"/" + root + "/" + id + "/edit\">Edit</a></td></tr>";
					}
					result += "</tbody>";
				}
				else
				{
					result += "<tbody><tr><td>No Results</td></tr></tbody>";
				}
				return new NonEncodedHtmlString(result);
			}

			public static IHtmlString GetInputRow<T>(this HtmlHelpers<T> helper, object val, string label)
			{
				var result = "<div class=\"control-group\">";
				result += "<label>" + label + "<label>";
				result += "<input type=\"text\" placeholder=\"\" value=\"@Model.User.Email\" id=\"Email\" name=\"Email\" class=\"required\"/>";
				result += "<span class=\"help-block\">The users email address, used for logging into the system.</span>";
				result += "</div>";
				return new NonEncodedHtmlString(result);
			}

			public static IHtmlString Breadcrumb<T>(this HtmlHelpers<T> helper, List<Link> links)
			{
				
 
				var result = "<ul class=\"breadcrumb\">";
				for (int i = 0; i < links.Count; i++){
					result += "<li><a href=\"" + links[i].Href + "\">" + links[i].Text + "</a>";
					result += i < (links.Count - 1) ? "<li><a href=\"" + links[i].Href + "\">" + links[i].Text + "</a>" + "<span class=\"divider\">/</span></li>" : "<li class=\"active\">" + links[i].Text + "</li>";
				}
				result += "</ul>";
				return new NonEncodedHtmlString(result);
			}
		}
 

}
