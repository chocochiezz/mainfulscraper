using Serilog;
using System;

namespace Mainful.AdminUI.Shared.Helpers
{
    public static class LoggerHelper
	{
		//public static string LogentriesToken
		//{
		//	get
		//	{
		//		var configToken = ConfigurationManager.AppSettings["Logentries.Token"];
		//		if (String.IsNullOrEmpty(configToken))
		//		{
		//			configToken = "2546acb8-4af4-4054-9e0f-212b26fbe991";
		//		}
		//		return configToken;
		//	}
		//}

		//private static NLog.Logger TheLogger = NLog.LogManager.GetCurrentClassLogger();
		private static ILogger errorLogger = new LoggerConfiguration()
			//.WriteTo.Logentries(LogentriesToken)
			//.WriteTo.Logger(lc => lc
			//	.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
			//	.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\error-log.txt", retainedFileCountLimit: 7)
			//)
			//.WriteTo.Logger(lc => lc
			//	.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
			//	.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\warning-log.txt", retainedFileCountLimit: 7)
			//)
			//.WriteTo.Logger(lc => lc
			//	.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
			//	.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\info-log.txt", retainedFileCountLimit: 7)
			//)
			//.WriteTo.Logger(lc => lc
			//	.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
			//	.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\debug-log.txt")
			//)
						//.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\error-log.txt", restrictedToMinimumLevel: LogEventLevel.Error, retainedFileCountLimit: 7)
						//.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\warning-log.txt", restrictedToMinimumLevel: LogEventLevel.Warning, retainedFileCountLimit: 7)
						//.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\info-log.txt", restrictedToMinimumLevel: LogEventLevel.Information, retainedFileCountLimit: 7)
						//.WriteTo.RollingFile(UtilityHelper.GetBasePath() + @"Logs\debug-log.txt", restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 7)
						//.MinimumLevel.Error()
						//.ReadFrom.AppSettings()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:Error"))
	//					.ReadFrom.AppSettings()
						.CreateLogger();

		private static ILogger warnLogger = new LoggerConfiguration()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:Warning")) //, restrictedToMinimumLevel: LogEventLevel.Warning, retainedFileCountLimit: 7)
//						.ReadFrom.Settings()
						.CreateLogger();

		private static ILogger infoLogger = new LoggerConfiguration()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:Info"))
		//				.ReadFrom.AppSettings()
						.CreateLogger();

		private static ILogger debugLogger = new LoggerConfiguration()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:Debug"))
			//			.ReadFrom.AppSettings()
						.CreateLogger();

		private static ILogger verboseLogger = new LoggerConfiguration()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:Verbose"))
				//		.ReadFrom.AppSettings()
						.CreateLogger();

		private static ILogger pushnotifLogger = new LoggerConfiguration()
						.WriteTo.RollingFile(ConfigHelper.Get("Logging:Path:PushNotif"))
				//		.ReadFrom.AppSettings()
						.CreateLogger();

		public static void Verbose(string message, params object[] propertyValue)
		{
			verboseLogger.Verbose(message, propertyValue);
		}

		public static void Info(string message, params object[] propertyValue)
		{
			infoLogger.Information(message, propertyValue);
		}

		public static void Warn(string message, params object[] propertyValue)
		{
			warnLogger.Warning(message, propertyValue);
		}

		public static void Debug(string message, params object[] propertyValue)
		{
			debugLogger.Debug(message, propertyValue);
		}

		public static void Error(string message, params object[] propertyValue)
		{
			errorLogger.Error(message, propertyValue);
		}

		public static void Error(Exception x)
		{
			errorLogger.Error(x, "", null);
		}

		public static void PushNotif(string message, params object[] propertyValue)
		{
			pushnotifLogger.Information(message, propertyValue);
		}

		//public static void Fatal(string message, params object[] propertyValue)
		//{
		//	errorLogger.Fatal(message, propertyValue);
		//}

		//public static void Fatal(Exception x)
		//{
		//	errorLogger.Fatal(x, "", null);
		//}

		//public static void DataLog(DataLogEntity dataLogEntity)
		//{
		//	//dId=34345634563456, --Device ID, to track the anonymous user
		//	//dBr=SS, --Device brand
		//	//dMo=ST-0323, --Device model

		//	//uId=0 --User ID, zero mean anonymous
		//	//uName=Tester --User full name or display name
		//	//uGen=male, --User Gender
		//	//uAge=32, --User age

		//	//cId=213 --content ID, record id on table Event
		//	//cType=Event | Promo --content type, is table name
		//	//cTitle=Home production --category title
		//												//cCat=category --content category
		//												//cEt=Seminar --event type

		//																							//pId=323 --Place ID, record id on table PlaceLocation
		//																							//pCity=city --Location
		//																							//pName=2345, --Place Name
		//																							//pCat=Hospitals, --Place Category			

		//																							//uA=user agent (mobile app use trick Hit HTTP to API to get user agent feedback)
		//																							//iP=ip address
		//																							//t=impression | click --Type of log
		//																							//p=list | similar | detail | share | fave --This is page type
		//																							//pN=global.listEvents --page name related to ads location, nearby.listPromo, etc. (refer to StatusHelper.cs)
		//																							//c=android | ios | windows | web --channel

		//	var adType = dataLogEntity.AdType.ToUpper();

		//	Info("[" + adType + "] dID={@DeviceID} dBR={@DeviceBrand} dMO={@DeviceModel} uID={@UserID} uNAME={@UserFullName} uGEN={@UserGender} uAGE={@UserAge} cID={@ContentID} cTYPE={@ContentType} cTITLE={@ContentTitle} cCAT={@ContentCategory} cET={@ContentEventType} pID={@PlaceID} pCITY={@PlaceCity} pNAME={@PlaceName} pCAT={@PlaceCategory} UA={@UserAgent} IP={@IPAddress} T={@AdType} P={@ViewType} PN={@PageName} C={@Channel}", 
		//			dataLogEntity.DeviceID,
		//			dataLogEntity.DeviceBrand,
		//			dataLogEntity.DeviceModel,
		//			dataLogEntity.UserID,
		//			dataLogEntity.UserFullName,
		//			dataLogEntity.UserGender,
		//			dataLogEntity.UserAge,
		//			dataLogEntity.ContentID,
		//			dataLogEntity.ContentType,
		//			dataLogEntity.ContentTitle,
		//			dataLogEntity.ContentCategory,
		//			dataLogEntity.ContentEventType,
		//			dataLogEntity.PlaceID,
		//			dataLogEntity.PlaceCity,
		//			dataLogEntity.PlaceName,
		//			dataLogEntity.PlaceCategory,
		//			dataLogEntity.UserAgent,
		//			dataLogEntity.IPAddress,
		//			adType,
		//			dataLogEntity.ViewType,
		//			dataLogEntity.PageName,
		//			dataLogEntity.Channel
		//		);
		//}

	}
}
