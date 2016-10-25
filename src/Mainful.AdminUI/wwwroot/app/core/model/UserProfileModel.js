
Ext.define('MyApp.model.UserProfileModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },		
		{ name: 'Name', type: 'string' },
		{ name: 'Gender', type: 'string' },
		{ name: 'Birthdate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'Phone', type: 'string' },
        { name: 'Email', type: 'string' },
		{ name: 'EmailConfirmed', type: 'boolean' },
		{ name: 'PasswordHash', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ReminderSetting', type: 'string' },
		{ name: 'PushNotification', type: 'boolean' },
		{ name: 'AvatarUrl', type: 'string' },		
		{ name: 'Status', type: 'string' },
		{ name: 'Passcode', type: 'string' },
		{ name: 'PasscodeExpired', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat }
    ],
    proxy: {
        type: 'rest',
		actionMethods:{
			create: 'POST', 
			read: 'GET', 
			update: 'POST', 
			destroy: 'GET'
		},
        api: {
            create: MyApp.GlobalVar.BASE_API_URL + 'UserProfile/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'UserProfile/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'UserProfile/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'UserProfile/Delete',
        },
        reader: {
            type: 'json',
            rootProperty: 'Data',
            totalProperty: 'MetaInfo.DataFound',
            successProperty: 'success',
            messageProperty: 'Msg'
        },
        listeners: {
			exception: function (proxy, response, operation, eOpts) {

        		var responseText = Ext.decode(response.responseText);
        		var message = (responseText.hasOwnProperty('msg'))
					? responseText.msg
					: '';

        		if (response.status !== 200) {
        			MyApp.GlobalFunc.showError(response.status + ' ' + response.statusText + '<br/>' + message);
        			return;
                }
                
                MyApp.GlobalFunc.showWarning(message);
            }
        }
    }
});