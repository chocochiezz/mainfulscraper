using Newtonsoft.Json;

namespace Mainful.AdminUI.Shared.Entities
{
    public class JsonResultEntity
	{
		// http://docs.sencha.com/extjs/6.2.0-classic/Ext.form.action.Submit.html
		[JsonProperty("errors")]
		public object Errors { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
        //[JsonProperty("msg")]
        [JsonProperty("msg")]
        public string Message { get; set; }
        public object Data { get; set; }
        public MetaInfoEntity MetaInfo { get; set; }

        public JsonResultEntity()
        {
            Success = false;
            Message = string.Empty;
        }
	}
}
