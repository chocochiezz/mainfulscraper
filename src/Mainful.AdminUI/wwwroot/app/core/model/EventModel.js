
Ext.define('MyApp.model.EventModel', {
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
		{ name: 'Timezone', type: 'string' },
		{ name: 'Weblink', type: 'string' },
		{ name: 'Facebook', type: 'string' },
		{ name: 'Twitter', type: 'string' },
		{ name: 'GooglePlus', type: 'string' },
		{ name: 'Email', type: 'string' },
		{ name: 'Online', type: 'string' },
		{ name: 'CreatedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'ModifiedDate', type: 'date', displayFormat: MyApp.GlobalVar.dateTimeFormat },
		{ name: 'EventCategoryID', type: 'int' },
		{ name: 'EventOrganizerID', type: 'int' },
		{ name: 'Tag', type: 'string' },
		{ name: 'IsFree', type: 'boolean' },
		{ name: 'Currency', type: 'string' },
		{ name: 'PriceRange', type: 'string' },
		{ name: 'Priority', type: 'float' },
		{ name: 'EventType', type: 'string' },
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
            create: MyApp.GlobalVar.BASE_API_URL + 'Event/Create',
            read: MyApp.GlobalVar.BASE_API_URL + 'Event/Get',
            update: MyApp.GlobalVar.BASE_API_URL + 'Event/Update',
            destroy: MyApp.GlobalVar.BASE_API_URL + 'Event/Delete',
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