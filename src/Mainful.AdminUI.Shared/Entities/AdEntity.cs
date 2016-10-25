	
using System;

namespace Mainful.AdminUI.Shared.Entities
{
	public class AdEntity
	{
		public int ID { get; set;}
		public long ContentID { get; set;}
		public string ContentSource { get; set;}
		public string AdType { get; set;}
		public bool IsActive { get; set;}
		public DateTime StartDate { get; set;}
		public TimeSpan StartTime { get; set;}
		public DateTime EndDate { get; set;}
		public TimeSpan EndTime { get; set;}
		public DateTime? CreatedDate { get; set;}
		public DateTime? ModifiedDate { get; set;}
		public string AdArea { get; set;}
		public double Weight { get; set;}
		public double StartDateTime { get; set;}
		public double EndDateTime { get; set;}
	}

    public class ContentEntity
    {
        public int ID { get; set; }
        public string ContentName{ get; set; }
    }
}