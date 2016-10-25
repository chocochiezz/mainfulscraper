namespace Mainful.Shared.Helpers
{
    public static class StatusHelper
	{
		public const int INCOMPLETE = 0;
		public const int COMPLETE = 1;
		public const int VERIFIED = 2;

		public const string USER_ACTIVE = "active";
		public const string USER_INACTIVE = "inactive";
		public const string USER_BLOCKED = "blocked";

		public const int MSG_QUEUE = 6;
		public const int MSG_SEND = 7;
		public const int MSG_RESEND = 8;
		public const int MSG_SUCCESS_SEND = 9;
		public const int MSG_FAILED_SEND = 10;
        public const int HIDDEN = 11;

		public const string CLIENT_WEB = "web";
		public const string CLIENT_ANDROID = "android";
		public const string CLIENT_IOS = "ios";
		public const string CLIENT_WINPHONE = "winphone";

		public const int BUCKET_SYSTEM = 0;
		public const int BUCKET_USER = 1;

		public const string CONTENT_EVENTS = "event";
		public const string CONTENT_SEMINARS = "seminar";
		public const string CONTENT_PROMO = "promo";
		public const string CONTENT_PLACES = "place";

		public const int EVENT_DRAFT = 0;
		public const int EVENT_ACTIVE = 1;
		public const int EVENT_BLOCK = 2;

		public const string VIEW_TYPE_LIST = "listing";
		public const string VIEW_TYPE_SIMILAR = "similar";
		public const string VIEW_TYPE_DETAIL = "detail";
		public const string VIEW_TYPE_FAVE = "fave";
		public const string VIEW_TYPE_SHARE = "share";
		
		public const string AD_IMPRESSION = "impression";
		public const string AD_CLICK = "click";

		public const string AD_TYPE_DIAMOND = "diamond";
		public const string AD_TYPE_PLATINUM = "platinum";

		public const string AD_AREA_GLOBAL_LIST_EVENTS = "global.listEvents"; // global when user not activete the nearby toggle
		public const string AD_AREA_GLOBAL_LIST_PROMO = "global.listPromo";
		public const string AD_AREA_GLOBAL_LIST_SEMINAR = "global.listSeminar";

		public const string AD_AREA_NEARBY_LIST_EVENTS = "nearby.listEvents"; // nearby is City based
		public const string AD_AREA_NEARBY_LIST_PROMO = "nearby.listPromo";
		public const string AD_AREA_NEARBY_LIST_SEMINAR = "nearby.listSeminar";
		public const string AD_AREA_NEARBY_LIST_PLACES = "nearby.listPlaces";

		public const string AD_AREA_DETAIL_EVENTS = "detail.events"; // show on similars list, its global list  not nearby
		public const string AD_AREA_DETAIL_PROMO = "detail.promo"; // show on similars list, its global list  not nearby
		public const string AD_AREA_DETAIL_SEMINAR = "detail.seminar"; // show on similars list, its global list not nearby
		public const string AD_AREA_DETAIL_PLACES = "detail.places"; // show on top 3 events or top 3 promo, its global list not nearby

		public const string NOTIFY_LOCATION = "Location";
		public const string NOTIFY_PAGE_EVENT_LIST = "EventList";
		public const string NOTIFY_PAGE_EVENT_DETAIL = "EventDetail";
		public const string NOTIFY_PAGE_SEMINAR_LIST = "SeminarList";
		public const string NOTIFY_PAGE_SEMINAR_DETAIL = "SeminarDetail";
		public const string NOTIFY_PAGE_PROMO_LIST = "PromoList";
		public const string NOTIFY_PAGE_PROMO_DETAIL = "PromoDetail";
		public const string NOTIFY_PAGE_PLACES_LIST = "PlacesList";
		public const string NOTIFY_PAGE_PLACES_DETAIL = "PlacesDetail";
		public const string NOTIFY_PAGE_FAVORITES_LIST = "FavoritesList";
		public const string NOTIFY_PAGE_PLACES_PROMO = "PlacesPromo";
		public const string NOTIFY_PAGE_PLACES_EVENTS = "PlacesEvents";
	}
}
