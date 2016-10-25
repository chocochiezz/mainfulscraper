
Ext.define('MyApp.model.PlaceUserImageModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'Content', type: 'string' },
		{ name: 'IsMain', type: 'boolean' },
		{ name: 'PlaceID', type: 'int' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'Checksum', type: 'string' },
		{ name: 'Caption', type: 'string' },
		{ name: 'UserContent', type: 'boolean' },
		{ name: 'ContentType', type: 'string' },
		{ name: 'Approved', type: 'boolean' },
		{ name: 'Rating', type: 'int' },
		{ name: 'UserID', type: 'int' },
		{ name: 'Point', type: 'int' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'PlaceUserImage/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'PlaceUserImage/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'PlaceUserImage/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'PlaceUserImage/Delete',
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