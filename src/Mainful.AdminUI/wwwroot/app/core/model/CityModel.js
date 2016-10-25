
Ext.define('MyApp.model.CityModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'ContinentCode', type: 'string' },
		{ name: 'ContinentName', type: 'string' },
		{ name: 'CountryIsoCode', type: 'string' },
		{ name: 'CountryName', type: 'string' },
		{ name: 'Subdivision_1_IsoCode', type: 'string' },
		{ name: 'Subdivision_1_Name', type: 'string' },
		{ name: 'Subdivision_2_IsoCode', type: 'string' },
		{ name: 'Subdivision_2_Name', type: 'string' },
		{ name: 'CityName', type: 'string' },
		{ name: 'MetroCode', type: 'string' },
		{ name: 'TimeZone', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'City/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'City/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'City/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'City/Delete',
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