
Ext.define('MyApp.model.FeedsModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'FeedChannel', type: 'string' },
		{ name: 'Content', type: 'string' },
		{ name: 'ImgUrl', type: 'string' },
		{ name: 'TargetTags', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'PushDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'PriorityWeight', type: 'float' },
		{ name: 'PlanPushDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'TrackingID', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Feeds/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Feeds/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Feeds/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Feeds/Delete',
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