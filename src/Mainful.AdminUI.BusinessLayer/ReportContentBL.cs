
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;

namespace Mainful.AdminUI.BusinessLayer
{
    public class ReportContentBL :BaseBL
    {
        public ResultEntity<ReportContentEntity> Create(ReportContentEntity reportcontentEntity)
        {
            var validationResult = new ResultEntity<ReportContentEntity>();

			using (var reportcontentDA = new ReportContentDA())
			{
				validationResult.Value = reportcontentDA.Create(reportcontentEntity);
			}
		
			return validationResult;
		}

		public ResultEntity<IEnumerable<ReportContentEntity>> GetAll(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<IEnumerable<ReportContentEntity>>();
		
			using (var reportcontentDA = new ReportContentDA())
			{
				validationResult.Value = reportcontentDA.GetAll(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var reportcontentDA = new ReportContentDA())
			{
				validationResult.Value = reportcontentDA.GetTotalRows(dbParamEntity);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ReportContentEntity> GetById(int id)
		{
			var validationResult = new ResultEntity<ReportContentEntity>();
		
			using (var reportcontentDA = new ReportContentDA())
			{
				validationResult.Value = reportcontentDA.GetById(id);
			}
		
			return validationResult;
		}
		
		public ResultEntity<ReportContentEntity> Update(ReportContentEntity reportcontentEntity)
		{
			var validationResult = new ResultEntity<ReportContentEntity>();
		
			using (var reportcontentDA = new ReportContentDA())
			{
				var resultUpdate = reportcontentDA.Update(reportcontentEntity);
		
				if (resultUpdate <= 0)
				{
					validationResult.Warning.Add("Failed Updating ReportContent!");
					return validationResult;
				}
		
				validationResult.Value = reportcontentEntity;
			}
		
			return validationResult;
		}
		
		public ResultEntity<int> DeleteById(int id)
		{
			var validationResult = new ResultEntity<int>();
		
			using (var reportcontentDA = new ReportContentDA())
			{
				//var ids = new int[] { id };
				validationResult.Value = reportcontentDA.Delete(id);
		
				if (validationResult.Value != 1)
				{
					validationResult.Warning.Add("Failed delete record ReportContent with ID: " + id);
					return validationResult;
				}
			}
		
			return validationResult;
		}
    }
}