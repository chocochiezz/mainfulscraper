
Ext.define('MyApp.model.BrandModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'BrandName', type: 'string' },
		{ name: 'Description', type: 'string' },
		{ name: 'Weblink', type: 'string' },
		{ name: 'Facebook', type: 'string' },
		{ name: 'Twitter', type: 'string' },
		{ name: 'GooglePlus', type: 'string' },
		{ name: 'Email', type: 'string' },
		{ name: 'Phone', type: 'string' },
		{ name: 'Instagram', type: 'string' },
		{ name: 'Logo', type: 'auto' },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'LogoChecksum', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Brand/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Brand/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Brand/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Brand/Delete',
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