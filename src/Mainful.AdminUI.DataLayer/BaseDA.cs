using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Mainful.AdminUI.Shared.Entities;
using Mainful.AdminUI.Shared.Helpers;

namespace Mainful.AdminUI.DataLayer
{
    public class BaseDA : IDisposable
	{
		protected NpgsqlConnection DbConnection = new NpgsqlConnection(ConfigHelper.GetDefaultConnection());
		//private Stopwatch stopwatch = new Stopwatch();

		public string conDbName { get; set; }

		/// <summary>
		/// Determines whether entity [is have identifier] [the specified entity].
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public bool IsHaveId<T>(T entity)
		{
			// Only get public properties
			PropertyInfo[] propertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public);

			// check the ID property is exists
			if (propertyInfo.Count(c => c.Name.Equals("ID", System.StringComparison.OrdinalIgnoreCase)) == 0)
			{
				return false;
			}

			return true;
		}

		public void Dispose()
		{
			if (DbConnection.State == System.Data.ConnectionState.Open)
			{
				var processID = DbConnection.ProcessID;
				DbConnection.Close();
				//stopwatch.Stop();
				//LoggerHelper.Debug(String.Format("[DBCON]	PID={0}	DBState={1}	Issuer=BaseDA	ElapsedTime(ms)={2}", processID, DbConnection.State, stopwatch.ElapsedMilliseconds));
			}
		}

		protected string ExtractFilter(DBParamEntity dbParamEntity, string tableAlias = "")
		{
			if (dbParamEntity == null)
			{
				return String.Empty;
			}
			
			var filter = new List<string>();
			var customFilter = " AND ";
			// return AND field IN (asd) AND field >= 123....
			foreach (var obj in dbParamEntity.Filter)
			{
				var customValue = Convert.ToString(obj.Value);
				
				if (customValue.Equals("[ongoing]", StringComparison.OrdinalIgnoreCase) || customValue.Equals("[ongoing,expiry]", StringComparison.OrdinalIgnoreCase))
				{
					filter.Add(String.Format("\"{0}\" BETWEEN CURRENT_DATE AND (CURRENT_DATE + 7)", obj.Property)); // ongoing is content between today and next 7 days
					continue;
				}

				if (customValue.Equals("[expiry]", StringComparison.OrdinalIgnoreCase))
				{
					filter.Add(String.Format("\"{0}\" = CURRENT_DATE AND (\"{1}\"='00:00:00' OR \"{1}\">=LOCALTIME)", obj.Property, "EndTime"));
					continue;
				}
				
				if (String.IsNullOrEmpty(obj.RawExpression) == false)
				{
					var rawExpression = obj.RawExpression;
					// prevent SQL Injection, or use attribute [JsonIgnore] on the properties to make sure its not mapped from user HTTP request
					rawExpression = rawExpression.Replace(" create ", "");
					rawExpression = rawExpression.Replace(" drop ", "");
					rawExpression = rawExpression.Replace(" delete ", "");
					rawExpression = rawExpression.Replace(" update ", "");
					rawExpression = rawExpression.Replace(" insert ", "");
					rawExpression = rawExpression.Replace(" where ", "");
					rawExpression = rawExpression.Replace(" select ", "");
					rawExpression = rawExpression.Replace(" group ", "");
					rawExpression = rawExpression.Replace(" order ", "");
					filter.Add(rawExpression);
					continue;
				}
				
				var op = " = ";
				var value = obj.Value;
				var field = "\"" + obj.Property + "\"";
				var fieldSourceAlias = "\"" + obj.FieldSource + "\"";

				if (String.IsNullOrEmpty(obj.FieldSource) == false)
				{
					field = fieldSourceAlias + "." + field;
				}
				else if (String.IsNullOrEmpty(tableAlias) == false)
				{
					field = tableAlias + "." + field;
				}

				DateTime dateValue = new DateTime();
				bool isDate = DateTime.TryParse(Convert.ToString(value), out dateValue);
				bool checkDate = false;
				bool isString = Convert.ToString(value).IndexOf("'") != 1;

				switch (obj.Operator)
				{
					case "eq":
						checkDate = true;
						break;
					case "xeq":
						op = " != ";
						checkDate = true;
						break;
					case "lt":
						op = " < ";
						checkDate = true;
						break;
					case "gt":
						op = " > ";
						checkDate = true;
						break;
					case "lte":
						op = " <= ";
						checkDate = true;
						break;
					case "gte":
						op = " >= ";
						checkDate = true;
						break;
					case "in":
						op = " IN ";
						List<string> inValue = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(obj.Value));
						value = "('" + String.Join("','", inValue) + "')";
						break;
					case "notin":
						op = " NOT IN ";
						inValue = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(obj.Value));
						value = "('" + String.Join("','", inValue) + "')";
						break;
					case "like":
						op = " LIKE ";
						value = "'%" + value + "%'";
						break;
					case "ilike": // for case insensitive
						op = " ILIKE ";
						value = "'%" + value + "%'";
						break;
				}

				if (checkDate == true && isDate == true)
				{
					field = "CAST(" + field + " AS DATE)";
					value = "'" + value + "'";
				}

				//if (isString) field = "lower(" + field + ")"; // make all string compare case-insensitive
				filter.Add(field + op + value);
			}
			if (dbParamEntity.CustomFilter.Equals("OR", StringComparison.OrdinalIgnoreCase))
			{
				customFilter = " OR ";
			}
			var result = (filter.Count != 0) ? " WHERE " + String.Join(customFilter, filter) : String.Empty;

			return result;
		}

		protected string ExtractSort(DBParamEntity dbParamEntity, Boolean isNeedDefaultSort = false)
		{
			if (dbParamEntity == null)
			{
				return String.Empty;
			}

			var sort = new List<string>();

			foreach (var obj in dbParamEntity.Sort)
			{
				var rawExpression = obj.RawExpression;

				if (String.IsNullOrEmpty(rawExpression) == false)
				{
					rawExpression = rawExpression.Replace(" create ", "");
					rawExpression = rawExpression.Replace(" drop ", "");
					rawExpression = rawExpression.Replace(" delete ", "");
					rawExpression = rawExpression.Replace(" update ", "");
					rawExpression = rawExpression.Replace(" insert ", "");
					rawExpression = rawExpression.Replace(" where ", "");
					rawExpression = rawExpression.Replace(" select ", "");
					rawExpression = rawExpression.Replace(" group ", "");
					rawExpression = rawExpression.Replace(" order ", "");
					sort.Add(rawExpression);
					continue;
				}
				
				var fieldSource = obj.FieldSource;
				if (String.IsNullOrEmpty(fieldSource) == false)
				{
					fieldSource += ".";
				}
				sort.Add(fieldSource + "\"" + obj.Property + "\" " + obj.Direction);
			}

			string defaultSort = (isNeedDefaultSort == true) ? " ORDER BY \"ID\" ASC " : String.Empty;
			var result = (sort.Count != 0) ? " ORDER BY " + String.Join(", ", sort) : defaultSort;

			return result;
		}

		protected string ExtractPaging(DBParamEntity dbParamEntity)
		{
			if (dbParamEntity == null)
			{
				return String.Empty;
			}

			var query = String.Empty;

			if (dbParamEntity.UsePaging == true)
			{
				var offset = (dbParamEntity.Page - 1) * dbParamEntity.Limit;
				if (offset < 0) offset = 0;
				query = " LIMIT " + dbParamEntity.Limit + " OFFSET " + offset;
			}

			return query;
		}
	}
}
