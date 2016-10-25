
Ext.define('MyApp.model.PromoImageModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'IsMain', type: 'boolean' },
		{ name: 'PromoID', type: 'int' },
		{ name: 'Content', type: 'auto' },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'Checksum', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'PromoImage/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'PromoImage/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'PromoImage/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'PromoImage/Delete',
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