
Ext.define('MyApp.model.TransactionModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'CreatedTime', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'Debt', type: 'float' },
		{ name: 'Credit', type: 'float' },
		{ name: 'Note', type: 'string' },
		{ name: 'RefID', type: 'int' },
		{ name: 'IsVoid', type: 'boolean' },
		{ name: 'VoidNote', type: 'string' },
		{ name: 'UserID', type: 'int' },
		{ name: 'Category', type: 'string' },
		{ name: 'TxDetail', type: 'string' },
		{ name: 'IsPending', type: 'boolean' },
		{ name: 'PendingNote', type: 'string' },
		{ name: 'OrderNumber', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Transaction/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Transaction/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Transaction/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Transaction/Delete',
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