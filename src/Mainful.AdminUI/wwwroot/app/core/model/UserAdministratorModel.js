
Ext.define('MyApp.model.UserAdministratorModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'UserName', type: 'string' },
		{ name: 'Password', type: 'string'},
		{ name: 'Name', type: 'string' },        
		{ name: 'Email', type: 'string' },
		{ name: 'Phone', type: 'string' },
        { name: 'CreatedBy', type: 'int' },
		{ name: 'ModifiedBy', type: 'int' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'Picture', type: 'string' },
		{ name: 'IsLocked', type: 'boolean' },		
		{ name: 'PasscodeExpired', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'GroupID', type: 'int' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'UserAdministrator/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'UserAdministrator/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'UserAdministrator/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'UserAdministrator/Delete',
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
