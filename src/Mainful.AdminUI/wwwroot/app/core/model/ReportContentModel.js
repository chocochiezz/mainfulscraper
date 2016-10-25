
Ext.define('MyApp.model.ReportContentModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'Content', type: 'string' },
		{ name: 'Category', type: 'string' },
		{ name: 'Rating', type: 'int' },
		{ name: 'Context', type: 'string' },
		{ name: 'ReffID', type: 'int' },
		{ name: 'Approved', type: 'boolean' },
		{ name: 'Comment', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'ReportContent/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'ReportContent/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'ReportContent/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'ReportContent/Delete',
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