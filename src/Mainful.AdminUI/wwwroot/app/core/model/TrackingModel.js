
Ext.define('MyApp.model.TrackingModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'TrackingID', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'UserID', type: 'int' },
		{ name: 'DeviceModel', type: 'string' },
		{ name: 'DeviceBrand', type: 'string' },
		{ name: 'DeviceID', type: 'string' },
		{ name: 'UserFullname', type: 'string' },
		{ name: 'UserEmail', type: 'string' },
		{ name: 'Channel', type: 'string' },
		{ name: 'ClientID', type: 'string' },
		{ name: 'TrackingChannel', type: 'string' },
		{ name: 'Params', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Tracking/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Tracking/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Tracking/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Tracking/Delete',
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