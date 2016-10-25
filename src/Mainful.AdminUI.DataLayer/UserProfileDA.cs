
using Mainful.AdminUI.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace Mainful.AdminUI.DataLayer
{
	public class UserProfileDA : BaseDA
	{
		public UserProfileEntity Create(UserProfileEntity userprofileEntity)
		{
			var query = @"INSERT INTO ""UserProfile""(""Email"",""EmailConfirmed"",""Name"",""Gender"",""Birthdate"",""Phone"",""PasswordHash"",""CreatedDate"",""ModifiedDate"",""ReminderSetting"",""PushNotification"",""AvatarUrl"",""IsDeleted"",""Status"",""Passcode"",""PasscodeExpired"") VALUES(@Email,@EmailConfirmed,@Name,@Gender,@Birthdate,@Phone,@PasswordHash,@CreatedDate,@ModifiedDate,@ReminderSetting,@PushNotification,@AvatarUrl,@IsDeleted,@Status,@Passcode,@PasscodeExpired) RETURNING ""ID"";";

			int id = DbConnection.Query<int>(query, userprofileEntity).Single();
			userprofileEntity.ID = id;
			return userprofileEntity;
		}

		public IEnumerable<UserProfileEntity> GetAll(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT ""ID"",""Email"",""EmailConfirmed"",""Name"",""Gender"",""Birthdate"",""Phone"",""PasswordHash"",""CreatedDate"",""ModifiedDate"",""ReminderSetting"",""PushNotification"",""AvatarUrl"",""IsDeleted"",""Status"",""Passcode"",""PasscodeExpired"" FROM ""UserProfile"" {{Filter}} {{Sorting}} {{Paging}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
			query = query.Replace("{{Sorting}}", ExtractSort(dbParamEntity));
			query = query.Replace("{{Paging}}", ExtractPaging(dbParamEntity));
		
			var userprofileEntity = DbConnection.Query<UserProfileEntity>(query);
		
			return userprofileEntity;
		}
		
		public int GetTotalRows(DBParamEntity dbParamEntity)
		{
			var query = @"SELECT COUNT(""ID"") FROM ""UserProfile"" {{Filter}}";
		
			query = query.Replace("{{Filter}}", ExtractFilter(dbParamEntity));
		
			return DbConnection.Query<int>(query).Single();
		}
		
		public UserProfileEntity GetById(int id)
		{
			var query = @"SELECT * FROM ""UserProfile"" WHERE ""ID""=@ID AND ""IsDeleted""=false";
		
			var userprofileEntity = DbConnection.Query<UserProfileEntity>(query, new {ID=id}).SingleOrDefault();
		
			return userprofileEntity;
		}
		
		public int Update(UserProfileEntity userprofileEntity)
		{
			int affectedRows = 0;
			if (IsHaveId<UserProfileEntity>(userprofileEntity) == false)
			{
				var query = @"UPDATE ""UserProfile"" SET ""Email""=@Email,""EmailConfirmed""=@EmailConfirmed,""Name""=@Name,""Gender""=@Gender,""Birthdate""=@Birthdate,""Phone""=@Phone,""PasswordHash""=@PasswordHash,""CreatedDate""=@CreatedDate,""ModifiedDate""=@ModifiedDate,""ReminderSetting""=@ReminderSetting,""PushNotification""=@PushNotification,""AvatarUrl""=@AvatarUrl,""IsDeleted""=@IsDeleted,""Status""=@Status,""Passcode""=@Passcode,""PasscodeExpired""=@PasscodeExpired WHERE ""ID""=@ID";
				affectedRows = DbConnection.Execute(query, userprofileEntity);
			}
		
			return affectedRows;
		}

        public int Delete(int ids)
        {
            var query = @"UPDATE ""UserProfile"" SET ""IsDeleted""=true WHERE ""ID""=@ID";
            var affectedRows = DbConnection.Execute(query, new { ID = ids });

            return affectedRows;
        }
	}
}