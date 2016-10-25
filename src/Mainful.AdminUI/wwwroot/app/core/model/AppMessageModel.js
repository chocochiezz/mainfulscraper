
Ext.define('MyApp.model.AppMessageModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'CriteriaCity', type: 'string' },
		{ name: 'CriteriaGender', type: 'string' },
		{ name: 'CriteriaAgeMin', type: 'int' },
		{ name: 'CriteriaAgeMax', type: 'int' },
		{ name: 'CriteriaDeviceOS', type: 'string' },
		{ name: 'CriteriaDeviceBrand', type: 'string' },
		{ name: 'CriteriaMember', type: 'boolean' },
		{ name: 'Content', type: 'string' },
		{ name: 'Weight', type: 'float' },
		{ name: 'StartDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'EndDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'Context', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'AppMessage/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'AppMessage/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'AppMessage/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'AppMessage/Delete',
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