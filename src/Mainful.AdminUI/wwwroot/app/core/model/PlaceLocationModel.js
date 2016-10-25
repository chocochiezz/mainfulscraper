
Ext.define('MyApp.model.PlaceLocationModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'PlaceName', type: 'string' },
		{ name: 'Description', type: 'string' },
		{ name: 'PlaceNote', type: 'string' },		
		{ name: 'IsVenue', type: 'boolean' },
		{ name: 'Address1', type: 'string' },
		{ name: 'Address2', type: 'string' },
		{ name: 'City', type: 'string' },
		{ name: 'State', type: 'string' },
		{ name: 'Country', type: 'string' },
		{ name: 'ZipPostal', type: 'string' },
		{ name: 'Latitude', type: 'float' },
		{ name: 'Longitude', type: 'float' },
		{ name: 'Priority', type: 'float' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'PlaceCategoryID', type: 'int' },
		{ name: 'Phone', type: 'string' },
		{ name: 'Weblink', type: 'string' },
		{ name: 'Email', type: 'string' },
		{ name: 'Slug', type: 'string' },
		{ name: 'SubCategoryID', type: 'int' },
		{ name: 'HasParking', type: 'boolean' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'PlaceLocation/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'PlaceLocation/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'PlaceLocation/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'PlaceLocation/Delete',
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