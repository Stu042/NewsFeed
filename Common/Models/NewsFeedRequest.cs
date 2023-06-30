using Microsoft.AspNetCore.Http;

namespace Common.Models;

public class NewsFeedRequest {
	public string? Name { get; set; }
	public int MaxItems { get; set; }


	/// <summary>Can throw; ArgumentNullException, FormatException and OverflowException </summary>
	public static NewsFeedRequest FromQuery(IQueryCollection query) {
		var request = new NewsFeedRequest {
			Name = query["name"],
			MaxItems = int.Parse(query["maxItems"])
		};
		return request;
	}

	public string ToUiString() {
		return $"Name: {Name}, MaxItems: {MaxItems}";
	}
}

/*
public static NewsFeedRequest FromQuery(IQueryCollection query) {
		var request = new NewsFeedRequest();
		foreach (var prop in typeof(NewsFeedRequest).GetProperties()) {
			var propName = char.ToLower(prop.Name[0]) + prop.Name[1..];
			if (query.TryGetValue(propName, out var value)) {
				switch (Type.GetTypeCode(prop.PropertyType)) {
					case TypeCode.Boolean:
						prop.SetValue(prop.PropertyType, value.FirstOrDefault(), null);
						break;
					case TypeCode.Char:
						prop.SetValue(prop.PropertyType, value.FirstOrDefault(), null);
						break;
					case TypeCode.Int16:
					case TypeCode.Int32:
					case TypeCode.Int64:
						var intVal = int.Parse(value.FirstOrDefault());
						prop.SetValue(prop.PropertyType, intVal, null);
						break;
					case TypeCode.DateTime:
						var dateTimeVal = DateTime.Parse(value.FirstOrDefault());
						prop.SetValue(prop.PropertyType, dateTimeVal, null);
						break;
					case TypeCode.String:
						var stringVal = new string(value.FirstOrDefault());
						prop.SetValue(prop.PropertyType, stringVal, null);
						break;
					default:
						throw new Exception("Unknown type in FromQuery");
				}
			}
		}
		return request;
	}
*/