
Ext.define('MyApp.model.MainfulScraperResultModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'Title', type: 'string' },
		{ name: 'Description', type: 'string' },
		{ name: 'StartDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'StartTime', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'EndDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'EndTime', type: 'date', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'Tag', type: 'string' },
		{ name: 'Issuer', type: 'string' },
		{ name: 'Days', type: 'string' },
		{ name: 'Times', type: 'string' },
		{ name: 'Terms', type: 'string' },
		{ name: 'Online', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'PromoCategoryID', type: 'int' },
		{ name: 'BrandID', type: 'int' },
		{ name: 'Priority', type: 'float' },
		{ name: 'Slug', type: 'string' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Promo/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Promo/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Promo/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Promo/Delete',
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