
Ext.define('MyApp.model.AdModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'ContentID', type: 'int' },
		{ name: 'ContentSource', type: 'string' },
		{ name: 'AdType', type: 'string' },
		{ name: 'IsActive', type: 'boolean' },
		{ name: 'StartDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'StartTime', type: 'string', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'EndDate', type: 'date', displayFormat: MyApp.GlobalVar.dateFormat },
		{ name: 'EndTime', type: 'string', displayFormat: MyApp.GlobalVar.timeFormat },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'AdArea', type: 'string' },
		{ name: 'Weight', type: 'float' },
		{ name: 'StartDateTime', type: 'float' },
		{ name: 'EndDateTime', type: 'float' }
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Ad/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Ad/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Ad/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Ad/Delete',
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


Ext.define('MyApp.model.ContentIDModel', {
    extend: 'MyApp.model.BaseModel',
    idProperty: 'ID',
    fields: [
		{ name: 'ID', convert: null, type: 'int' },
		{ name: 'ContentName', type: 'string' },		
    ],
    proxy: {
        type: 'rest',
        actionMethods: {            
            read: 'GET',            
        },
        api: {            
            read: MyApp.GlobalVar.BASE_API_URL + 'Ad/GetContentID',            
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