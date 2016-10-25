
Ext.define('MyApp.model.CountryModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'CountryName', type: 'string' },
		{ name: 'IsoCode', type: 'string' },
		{ name: 'ContinentName', type: 'string' },
		{ name: 'ContinentCode', type: 'string' },
		{ name: 'GeonameID', type: 'int' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Country/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Country/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Country/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Country/Delete',
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