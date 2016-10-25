
Ext.define('MyApp.model.PlaceCategoryModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'CategoryName', type: 'string' },
		{ name: 'Description', type: 'string' },
		{ name: 'Logo', type: 'auto' },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'Tag', type: 'string' },
		{ name: 'IsPremium', type: 'boolean' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'PlaceCategory/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'PlaceCategory/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'PlaceCategory/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'PlaceCategory/Delete',
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