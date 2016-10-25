
Ext.define('MyApp.model.EventOrganizerModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'Name', type: 'string' },
		{ name: 'Description', type: 'string' },
		{ name: 'Phone1', type: 'string' },
		{ name: 'Phone2', type: 'string' },
		{ name: 'Logo', type: 'auto' },
		{ name: 'ShortName', type: 'string' },
		{ name: 'LongDescription', type: 'string' },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'LogoChecksum', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'EventOrganizer/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'EventOrganizer/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'EventOrganizer/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'EventOrganizer/Delete',
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