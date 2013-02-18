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

			public static IHtmlString GetInputRow<T>(this HtmlHelpers<T> helper, object val, string label, string help = "" ) 
			{
				var result = "<div class=\"control-group\">";
				result += string.Format("<label>{0}<label>", label);
				result += string.Format("<input type=\"text\" placeholder=\"\" value=\"{0}\" id=\"{1}\" name=\"{1}\" class=\"required\"/>", val, label.Replace(" ", ""));
				if(!string.IsNullOrEmpty(help))
					result += string.Format("<span class=\"help-block\">{0}</span>", help);
				result += "</div>";
				return new NonEncodedHtmlString(result);
			}

			public static IHtmlString Breadcrumb<T>(this HtmlHelpers<T> helper, List<Link> links)
			{
				var result = "<ul class=\"breadcrumb\">";
				for (int i = 0; i < links.Count; i++){
					result += i < (links.Count - 1) ? string.Format("<li><a href=\"{0}\">{1}</a><span class=\"divider\">/</span></li>", links[i].Href, links[i].Text) : string.Format("<li class=\"active\">{0}</li>", links[i].Text);
				}
				result += "</ul>";
				return new NonEncodedHtmlString(result);
			}
			public static IHtmlString Button<T>(this HtmlHelpers<T> helper, string label, string btnClass ="primary", string btnType = "submit")
			{
				var result = string.Format("<button type=\"{1}\" class=\"btn btn-{2}\">{0}</button>", label, btnType, btnClass.ToLower());
 
				return new NonEncodedHtmlString(result);
			}

		}
 

}
