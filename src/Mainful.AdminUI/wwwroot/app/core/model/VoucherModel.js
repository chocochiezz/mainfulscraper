
Ext.define('MyApp.model.VoucherModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'Title', type: 'string' },
		{ name: 'Value', type: 'float' },
		{ name: 'IsActive', type: 'boolean' },
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Voucher/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Voucher/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Voucher/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Voucher/Delete',
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