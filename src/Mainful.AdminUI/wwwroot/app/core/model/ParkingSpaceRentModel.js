
Ext.define('MyApp.model.ParkingSpaceRentModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'ParkingSpaceID', type: 'int' },
		{ name: 'UserID', type: 'int' },
		{ name: 'RentDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'StartTime', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'EndTime', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'Price', type: 'float' },
		{ name: 'IsVoid', type: 'boolean' },
		{ name: 'VoidNote', type: 'string' },
		{ name: 'CarNumber', type: 'string' },
		{ name: 'CarPhoto', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'BookingCode', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpaceRent/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpaceRent/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpaceRent/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpaceRent/Delete',
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