
Ext.define('MyApp.model.ParkingSpacePriceModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'ParkingSpaceID', type: 'int' },
		{ name: 'StartDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
        { name: 'EndDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'Price', type: 'float' },
		{ name: 'Category', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpacePrice/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpacePrice/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpacePrice/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'ParkingSpacePrice/Delete',
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