using System;
using Mainful.AdminUI.DataLayer;
using Mainful.AdminUI.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Mainful.AdminUI.BusinessLayer
{
    public class GroupAdministratorBL : BaseBL
    {
        public ResultEntity<GroupAdministratorEntity> Create(GroupAdministratorEntity groupadministratorEntity)
        {
            var validationResult = new ResultEntity<GroupAdministratorEntity>();

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                var groupList = groupadministratorDA.GetByGroupName(groupadministratorEntity.GroupName);

                if (groupList.Count()>0)
                {
                    validationResult.Warning.Add("Groupname "+groupadministratorEntity.GroupName+" already exist");
                    return validationResult;
                }

                groupadministratorEntity.CreatedDate = DateTime.Now;
                validationResult.Value = groupadministratorDA.Create(groupadministratorEntity);
            }

            return validationResult;
        }

        public ResultEntity<IEnumerable<GroupAdministratorEntity>> GetAll(DBParamEntity dbParamEntity)
        {
            var validationResult = new ResultEntity<IEnumerable<GroupAdministratorEntity>>();

            var filter = new DBParamEntity();

            dbParamEntity.Filter.Add(new FilterDBParamEntity
            {
                Property = "IsDeleted",
                Operator = "eq",
                Value = "false"
            });

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                validationResult.Value = groupadministratorDA.GetAll(dbParamEntity);
            }

            return validationResult;
        }

        public ResultEntity<int> GetTotalRows(DBParamEntity dbParamEntity)
        {
            var validationResult = new ResultEntity<int>();

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                validationResult.Value = groupadministratorDA.GetTotalRows(dbParamEntity);
            }

            return validationResult;
        }

        public ResultEntity<GroupAdministratorEntity> GetById(int id)
        {
            var validationResult = new ResultEntity<GroupAdministratorEntity>();

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                validationResult.Value = groupadministratorDA.GetById(id);
            }

            return validationResult;
        }

        public ResultEntity<GroupAdministratorEntity> Update(GroupAdministratorEntity groupadministratorEntity)
        {
            var validationResult = new ResultEntity<GroupAdministratorEntity>();

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                var groupList = groupadministratorDA.GetByGroupName(groupadministratorEntity.GroupName);

                var linq = (from x in groupList
                           where x.ID != groupadministratorEntity.ID
                           select x).ToList<GroupAdministratorEntity>();

                if (linq.Count() > 0)
                {
                    validationResult.Warning.Add("Groupname " + groupadministratorEntity.GroupName + " already exist");
                    return validationResult;
                }

                groupadministratorEntity.ModifiedDate = DateTime.Now;
                var resultUpdate = groupadministratorDA.Update(groupadministratorEntity);

                if (resultUpdate <= 0)
                {
                    validationResult.Warning.Add("Failed Updating GroupAdministrator!");
                    return validationResult;
                }

                validationResult.Value = groupadministratorEntity;
            }

            return validationResult;
        }

        public ResultEntity<int> DeleteById(int id)
        {
            var validationResult = new ResultEntity<int>();

            using (var groupadministratorDA = new GroupAdministratorDA())
            {
                //var ids = new int[] { id };
                validationResult.Value = groupadministratorDA.Delete(id);

                if (validationResult.Value != 1)
                {
                    validationResult.Warning.Add("Failed delete record GroupAdministrator with ID: " + id);
                    return validationResult;
                }
            }

            return validationResult;
        }
    }
}