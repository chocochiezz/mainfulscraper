using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Specialized;

namespace Mainful.AdminUI.Shared.Entities
{
    public class DBParamEntity
	{
		public int Page { get; set; } // current page position
		public int Limit { get; set; }
		public List<SortDBParamEntity> Sort { get; set; }
		public List<FilterDBParamEntity> Filter { get; set; }
		public string CustomFilter { get; set; }
		public bool UsePaging { get; set; }
		//public UrlQueryString QueryStringValue { get; set; }
		public string Fields { get; set; } // set return fields, later for optimization response data length
		public string TrackingID { get; set; }

		private double latitude;
		public double Latitude
		{
			get { return latitude; }
			set
			{
				latitude = (value != 0) ? value : -6.208763;
			}
		}
		private double longitude;
		public double Longitude
		{
			get { return longitude; }
			set 
			{
				longitude = (value != 0) ? value :106.845599;
			}
		}
		public bool IsNearby { get; set; }
		public Nullable<DateTime> LastAccess { get; set; }

		public DBParamEntity()
		{
			Sort = new List<SortDBParamEntity>();
			Filter = new List<FilterDBParamEntity>();
			CustomFilter = String.Empty;
			Page = 1;
            Limit = 30;// UtilityHelper.LIMIT_PER_PAGE;
			UsePaging = true;
			//QueryStringValue = new UrlQueryString();

			// Default is Jakarta
			Latitude = -6.208763;
			Longitude = 106.845599;

			IsNearby = false;
		}
	}

	public class SortDBParamEntity
	{
		public string FieldSource { get; set; }
		public string Property { get; set; }
		public string Direction { get; set; }
		public string RawExpression { get; set; }
	}

	public class FilterDBParamEntity
	{
		public string FieldSource { get; set; }
		public string Property { get; set; }
		public object Value { get; set; }
		public string Operator { get; set; }
		public string Type { get; set; }
		public string RawExpression { get; set; }
	}

	public static class ExtensionMethod
	{
		public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this NameValueCollection col)
		{
			var dict = new Dictionary<TKey, TValue>();
			var keyConverter = TypeDescriptor.GetConverter(typeof(TKey));
			var valueConverter = TypeDescriptor.GetConverter(typeof(TValue));

			foreach (string name in col)
			{
				TKey key = (TKey)keyConverter.ConvertFromString(name);
				TValue value = (TValue)valueConverter.ConvertFromString(col[name]);
				dict.Add(key, value);
			}

			return dict;
		}
	}
}
