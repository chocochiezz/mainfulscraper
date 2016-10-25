
Ext.define('MyApp.model.UserContentBlockedModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'SourceName', type: 'string' },
		{ name: 'ReferenceID', type: 'int' },
		{ name: 'UserProfileID', type: 'int' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'UserContentBlocked/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'UserContentBlocked/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'UserContentBlocked/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'UserContentBlocked/Delete',
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