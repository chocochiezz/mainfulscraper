using System;
using System.Collections.Generic;

namespace Mainful.AdminUI.Shared.Entities
{
    public class ResultEntity<T>
    {
		public T Value { get; set; }
        public List<string> Error { get; set; }
        public List<string> Warning { get; set; }
        public List<string> Info { get; set; }
        public List<string> RawValue { get; set; }
		public string ExtensionName { get; set; } // additional info for Extension

        public ResultEntity()
        {
            this.Error = new List<string>();
            this.Warning = new List<string>();
            this.Info = new List<string>();
            this.RawValue = new List<string>();
        }

        public bool HasError()
        {
            return (this.Error.Count != 0);
        }

        public bool HasWarning()
        {
            return (this.Warning.Count != 0);
        }

        public bool HasInfo()
        {
            return (this.Info.Count != 0);
        }

        public bool HasRawValue()
        {
            return (this.RawValue.Count != 0);
        }

		public object CompileMessage()
		{
			return new
			{
				Error = String.Join(",", Error),
				Warning = String.Join(",", Warning),
				Info = String.Join(",", Info)
			};
		}

		public override string ToString()
		{
			var msg = "Error: " + String.Join(",", Error);
			msg += Environment.NewLine + "Warning: " + String.Join(",", Warning);
			msg += Environment.NewLine + "nInfo: " + String.Join(",", Info);
			return msg;
		}

		public string GetWarningText()
		{
			if (Warning != null) return String.Join(",", Warning);
			return String.Empty;
		}

		public string GetErrorText()
		{
			if (Error != null) return String.Join(",", Error);
			return String.Empty;
		}

		public string GetInfoText()
		{
			if (Info != null) return String.Join(",", Info);
			return String.Empty;
		}
	}
}
