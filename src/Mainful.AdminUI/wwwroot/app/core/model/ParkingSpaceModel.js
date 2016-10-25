
Ext.define('MyApp.model.ParkingSpaceModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'PlaceID', type: 'int'},
        { name: 'PlaceName', type: 'string' },
		{ name: 'Floor', type: 'int' },
		{ name: 'Spot', type: 'string' },
		{ name: 'IsActive', type: 'boolean' },
		{ name: 'IsVIP', type: 'boolean' },
		{ name: 'Price', type: 'float' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ContributorID', type: 'int' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpace/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpace/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpace/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpace/Delete',
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