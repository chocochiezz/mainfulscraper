// @source core/ajax/Connection.js
Ext.data.Connection.override({

	setupHeaders: function (xhr, options, data, params) {
		if (Ext.isEmpty(options.headers)) {
			options.headers = {
				Accept: '*/*'
			};
		} else {
			options.headers['Accept'] = '*/*';
		}
		options.headers['PhoneSpec'] = '{"DeviceID":"4f8hf9458h","Brand":"Samohung","Model":"CJ-7","Channel":"android"}';
		options.headers['Authorization'] = "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIxMjM0NTY3OCIsInN1YiI6IiIsImF1ZCI6IiIsImNpIjoiMTIzNDU2NzgiLCJjaCI6ImFuZHJvaWQiLCJ1aWQiOjQsImRpZCI6IjRmOGhmOTQ1OGgiLCJzY29wZSI6IioifQ.tPgus0Yao-WXkhaW8Kv5HK_mm3ZGhCDtBoN-6BatwPY";
		this.callParent(arguments);
	},
	//setupUrl: function (options, url) {
	//	var form = this.getForm(options);
	//	if (form) {
	//		url = url || form.action;
	//	}

	//	// ------------------------------------------------------------------
	//	// CUSTOM BEHAVIOR
	//	// Purpose of this code is to passing project id and role id
	//	// in every ajax request, one of the functionality is 
	//	// for checking authorization per project per role on the server-side
	//	// so the authorization logic can know the CONTEXT
	//	// ------------------------------------------------------------------
	//	//var project = MyApp.UserProfile.getProjectActive();
	//	//var role = MyApp.UserProfile.getRoleActive();
	//	//var role = packages.RoleID;
	//	var user = MyApp.UserProfile;

	//	//var projectId = (project !== null) ? project.ID : 0;
	//	var userId = (user !== null) ? user.getUserId() : 0;
	//	var userName = (user !== null) ? user.getUserName() : '';
	//	var roleId = 0;

	//	if (user.getRoleActive() != null) {
	//		roleId = user.getRoleActive().RoleID;
	//	}

	//	if (typeof url === 'string') {

	//		if (url.indexOf('?') == -1) {
	//			url += '?';
	//		}

	//		if (url.indexOf('&') == -1) {
	//			url += '&';
	//		}

	//		//url += '_ProjectID=' + projectId + '&_PackageID=' + packageId + '&_RoleID=' + role + '&_UserID=' + userId + '&_CopadUserID=' + copadUserId;
	//		url += '_UserID=' + userId + '&_UserName=' + userName + '&_RoleID=' + roleId;
	//	}

	//	//console.log(MyApp.UserProfile.getConfig());
	//	//console.log(url);
	//	// ------------------------------------------------------------------

	//	return url;
	//},

});

Ext.define('MyApp.GlobalVar', {
	singleton: true,

	BASE_FOLDER: "",// "incoms/",
	BASE_URL: "http://localhost:54731/",
	BASE_API_URL: "http://localhost:54731/api/",
	BASE_UPLOAD_URL: "http://localhost:54731/",
	PUBLIC_URL: "http://localhost:54731/",

	TIMEOUT: 6000,

	isEditMode: false,
	dateTimeFormat: 'd-M-Y H:i:s',
	timeFormat: 'H:i:s',
	dateFormat: 'd-M-Y',
	isoDateTimeFormat: 'c',
	microsoftFormat: 'MS',

	getBasePath: function () {
		//var basePath = window.location.href;

		//if (basePath.charAt(basePath.length - 1) !== '/')
		//	basePath += '/';

		//return basePath;

		return location.protocol + "//" + location.hostname + (location.port && ":" + location.port) + "/";
	},

	constructor: function () {
		var basePath = this.getBasePath();//this.getBaseUrl() + this.BASE_FOLDER;
		this.BASE_URL = basePath;
		this.BASE_API_URL = basePath;// + 'api/';
		this.BASE_UPLOAD_URL = basePath;
		this.PUBLIC_URL = basePath;
	},

	listMonth: [
		{ Val: 1, Desc: 'January' },
		{ Val: 2, Desc: 'February' },
		{ Val: 3, Desc: 'March' },
		{ Val: 4, Desc: 'April' },
		{ Val: 5, Desc: 'May' },
		{ Val: 6, Desc: 'June' },
		{ Val: 7, Desc: 'July' },
		{ Val: 8, Desc: 'August' },
		{ Val: 9, Desc: 'September' },
		{ Val: 10, Desc: 'October' },
		{ Val: 11, Desc: 'November' },
		{ Val: 12, Desc: 'December' }
	],

	listWorkLocationType: [
		{ Val: 'offshore', Desc: 'Offshore' },
		{ Val: 'onshore', Desc: 'Onshore' },
		{ Val: 'home_office', Desc: 'Home Office' },
		{ Val: 'client_home_office', Desc: 'Client Home Office' },
	],

	listTimesheetType: [
		{ Val: 'Weekly', Desc: 'Weekly' },
		{ Val: 'Monthly', Desc: 'Monthly' }
	],

	listTimesheetStatus: [
		{ Val: 'DRAFT', Desc: 'Draft' },
		{ Val: 'SIGNED', Desc: 'Signed' }
	]
});
/**
 * The main application class. An instance of this class is created by `app.js` when it calls
 * Ext.application(). This is the ideal place to handle application launch and initialization
 * details.
 */
Ext.application({
	name: 'MyApp',

	//appFolder: 'Incoms/Scripts/app', // for testing in JKWbit4
	appFolder: MyApp.GlobalVar.BASE_URL + 'app/core', // for localhost
	//autoCreateViewport: 'MyApp.view.main.Main',
	stores: [
        // TODO: add global / shared stores here
	],
	views: [
        'MyApp.view.login.Login',
        'MyApp.view.main.Main'
	],
	splashscreen: {},
	init: function () {
		//splashscreen = Ext.getBody().mask('Loading Application... Please wait...', 'splashscreen');
	},
	launch: function () {        		
		Ext.Ajax.on('beforerequest', this.showSpinner);
		Ext.Ajax.on('requestcomplete', this.hideSpinner);
		Ext.Ajax.on('requestexception', this.hideSpinner);

		Ext.tip.QuickTipManager.init();

	    Ext.Ajax.request({
	        method: 'POST',
	        url: MyApp.GlobalVar.BASE_API_URL + 'Login/CheckSession',
	        success: function (response) {
	            var obj = Ext.decode(response.responseText);

	            if(obj.success)
	                Ext.widget('app-main');
	            else
	                Ext.widget('login');
	        },
	        failure: function (response) {
	            Ext.Msg.show({
	                title: 'Error',
	                message: response.statusText,
	                buttons: Ext.Msg.OK,
	                icon: Ext.Msg.ERROR
	            });
	        }
	    });
	},
	showSpinner: function (conn) {
		Ext.getBody().mask("Please be patient, the system is processing and loading related data...", 'custom-mask');
	},
	hideSpinner: function () {
		Ext.getBody().unmask();
	}
});

Ext.define('MyApp.ExtJSHub', {
	mixins: ['Ext.mixin.Observable'],
	//alternateClassName: 'ExtJSHub',
	singleton: true,
	//config:{
	//	listeners: {
	//		relay: function (storeId) {
	//			alert('this is config relay');
	//		}
	//	},
	//},

	constructor: function (config) {
		var me = this;
		me.mixins.observable.constructor.call(this, config);
		me.addListener({
			relay: function (storeId) {
				//alert('relay called');
				// define pre-logic here... if needed
			}
		})
	},
});

Ext.define('MyApp.GlobalFunc', {
	singleton: true,
	getGridColumns: function (storeId, useRowNumber, listShowField) {

		var gridColumns = new Array();
		var currentStore = this.storeFactory(storeId); //Ext.getStore(storeId);
		var fieldsInfo = currentStore.getProxy().getModel().getFields();

		var _modelName = currentStore.getProxy().getModel().getName();
		var tmpModelName = _modelName.split('.');
		var modelName = tmpModelName[2];
		var labelList = {};

		// use custom mean call the predefined columns
		//if (typeof modelName !== 'undefined' && modelName.length !== 0) {
		//    var fieldLabel = this.getFormFields(storeId, modelName);
		//    Ext.Array.each(fieldLabel, function (name, index, currentObj) {
		//        if (fieldLabel[index].hasOwnProperty('fieldLabel')) {
		//            labelList[fieldLabel[index].name] = fieldLabel[index].fieldLabel;
		//        }
		//    });

		//    //console.log(labelList);
		//}

		if (useRowNumber === true) {
			gridColumns.push({ xtype: 'rownumberer', resizeable: true });
		}

		// create gridcolumn based on the 
		// and show only 7 first fields, ID primary key not included (hidden)
		var showField = 7;
		for (var i = 0; i < fieldsInfo.length; i++) {
			var isField = fieldsInfo[i].isField;
			if (isField !== true) {
				continue;
			}

			var isFormatNumber = fieldsInfo[i].hasOwnProperty('formatNumber')
                ? fieldsInfo[i].formatNumber
                : true;
			var displayFormat = (fieldsInfo[i].hasOwnProperty('displayFormat'))
                ? fieldsInfo[i].displayFormat
                : MyApp.GlobalFunc.dateTimeFormat;
			var fieldType = fieldsInfo[i].type;
			var fieldName = fieldsInfo[i].name;
			var isHideOnGrid = fieldsInfo[i].hideOnGrid;
			var headerOnGrid = fieldsInfo[i].hasOwnProperty('headerOnGrid') ? fieldsInfo[i].headerOnGrid : null;
			var isSortable = fieldsInfo[i].hasOwnProperty('sortable') ? fieldsInfo[i].sortable : true;
			// later is hidden will be get from the model
			// so quite straight forward to setting the display data
			var isHidden = (showField <= 0) ? true : false;
			var flex = fieldsInfo[i].hasOwnProperty('flex') ? fieldsInfo[i].flex : 0;
			var columnWidth = fieldsInfo[i].hasOwnProperty('columnWidth') ? fieldsInfo[i].columnWidth : 0;

			var hasEditor = false;
			if (fieldsInfo[i].hasOwnProperty('hasEditor')) {
				hasEditor = fieldsInfo[i].hasEditor;
			}

			if (headerOnGrid === null && labelList.hasOwnProperty(fieldName)) {
				headerOnGrid = labelList[fieldName];
			}

			// override
			if (isHideOnGrid === true) {
				isHidden = isHideOnGrid;
			}
			else if (isHideOnGrid === false) {
				isHidden = false;
			}

			var isForeignKey = fieldsInfo[i].isForeignKey;

			if (fieldName === 'ID' || fieldName == 'TempPath' || fieldName === 'AvatarPath' || isForeignKey === true) {
				isHidden = true;
			}
			else if (typeof isHideOnGrid === 'undefined') {
				showField--;
			}

			var column = {};

			if (fieldType === 'int' || fieldType === 'numeric') {
				column.xtype = 'numbercolumn';
				column.align = 'right';
				column.filter = 'number';
				column.format = '0,000.00'; // default
				//column.format = '0,0.00';

				if (isFormatNumber == false) {
					column.format = '';
				}

				if (hasEditor) {
					column.editor = {
						xtype: 'numberfield'
					};
				}
			}
			else if (fieldType === 'date') {
				//var dateFormat = fieldsInfo[0].dateFormat;
				column.xtype = 'datecolumn';
				column.format = displayFormat;
				column.filter = 'date';
				if (hasEditor) {
					column.editor = {
						xtype: 'datefield'
					};
				}
			}
			else if (fieldType === 'boolean' || fieldType === 'bool') {
				column.xtype = 'booleancolumn';
				//column.xtype = 'checkcolumn';
				column.trueText = 'Yes';
				column.falseText = 'No';
				column.filter = 'boolean';

				if (hasEditor) {
					column.editor = {
						xtype: 'checkbox'
					};
				}

			}
			else // is String
			{
				column.filter = 'string';
				if (hasEditor) {
					column.editor = {
						xtype: 'textfield'
					};
				}
			}

			if (headerOnGrid !== null) {
				column.text = headerOnGrid;
			}
			else {
				column.text = this.getFriendlyName(fieldName);
			}
			column.dataIndex = fieldName;
			column.sortable = isSortable;

			if (listShowField != undefined && listShowField != null) {
				if (listShowField.length != 0) {
					var exist = listShowField.filter(function (item) {
						if (item.indexOf(fieldName) != -1) {
							return true;
						}
					});

					if (exist.length != 0) {
						isHidden = false;
						flex = 1;
					}
					else {
						isHidden = true;
					}
				}
			}

			column.hidden = isHidden;
			column.filterable = true;

			if (flex != 0) {
				column.flex = flex;
			}

			if (flex == 0 && columnWidth != 0) {
				column.width = columnWidth;
			}

			//column.filter = {type: 'string'};

			gridColumns.push(column);
		}
		//to get genrated columns
		//console.log(Ext.encode( gridColumns));
		return gridColumns;
	},
	getFilterConfig: function (storeId) {

		var currentStore = this.storeFactory(storeId); //Ext.getStore(storeId);
		var fieldsInfo = currentStore.getProxy().getModel().getFields();
		var filters = [];

		for (var i = 0; i < fieldsInfo.length; i++) {
			var fieldName = fieldsInfo[i].name;
			var fieldType = fieldsInfo[i].type.type;
			var filter = { dataIndex: fieldName };

			if (fieldType === 'int' || fieldType === 'float') {
				filter.type = 'numeric';
			}
			else if (fieldType === 'date') {
				filter.type = 'date';
			}
			else if (fieldType === 'boolean') {
				filter.type = 'boolean';
			}
			else {
				filter.type = 'string';
			}

			filters.push(filter);
		}

		return filters;
	},
	generateFormFields: function (storeId) {

		var currentStore = this.storeFactory(storeId); //Ext.getStore(storeId);
		var fieldsInfo = currentStore.getProxy().getModel().getFields();
		var generatedFields = [];

		Ext.Array.each(fieldsInfo, function (item, index, allItems) {

			var fieldType = item.type;
			var fieldName = item.name;
			var fieldLabel = MyApp.GlobalFunc.getFriendlyName(fieldName);
			var isIdentifier = item.identifier;
			var isHidden = isIdentifier ? true : false;
			var fieldLookup = null;
			if (item.hasOwnProperty('lookup')) {
				fieldLookup = item.lookup;
			}

			///http://stackoverflow.com/questions/18848261/extjs-mvc-dates-showing-in-grid-but-loadrecord-cannot-load-them-into-form-co
			var displayFormat = null;
			if (item.hasOwnProperty('displayFormat')) {
				displayFormat = item.displayFormat;
			}

			var isReadOnly = false;
			if (item.hasOwnProperty('readonly')) {
				isReadOnly = item.readonly;
			}

			var inputComponent = null;
			if (item.hasOwnProperty('inputComponent')) {
				inputComponent = item.inputComponent;
			}

			if (item.hasOwnProperty('hideOnForm')) {
				isHidden = item.hideOnForm;
			}

			switch (fieldType) {
				case 'int':
				case 'number':
					component = {
						xtype: 'numberfield',
						name: fieldName,
						fieldLabel: fieldLabel,
						hidden: isHidden,
						readOnly: isReadOnly,
						value: 0,
						msgTarget: 'side'
					};

					// set minValue, maxValue, defaultValue, step, value, etc
					// set allowDecimals, decimalPrecision, decimalSeparator

					break;
				case 'date':
					component = {
						xtype: 'datefield',
						name: fieldName,
						fieldLabel: fieldLabel,
						hidden: isHidden,
						readOnly: isReadOnly,
						msgTarget: 'side'
						//format: dateFormat,
						//altFormat: 'c',
					};

					if (displayFormat !== null) {
						component.format = displayFormat;
					}

					// set maxValue : new Date() // limited to the current date or prior
					// set format, altFormat

					break;
				case 'boolean':
					component = {
						xtype: 'checkbox',
						name: fieldName,
						fieldLabel: fieldLabel,
						readOnly: isReadOnly,
						msgTarget: 'side'
					};

					// set inputValue, uncheckedValue

					break;
				default: // string

					var xtype = isReadOnly ? 'displayfield' : 'textfield';

					component = {
						xtype: xtype,
						name: fieldName,
						fieldLabel: fieldLabel,
						hidden: isHidden,
						msgTarget: 'side'
						//readOnly: isReadOnly
					};

					// set maxLength, etc

					break;
			}

			if (inputComponent !== null) {
				// override or combine object properties by/with inputComponent
				var objectKeys = Object.keys(inputComponent);
				for (var i = 0; i < objectKeys.length; i++) {
					var idxName = objectKeys[i];
					component[idxName] = inputComponent[idxName];
				}
			}

			//console.log(component);

			generatedFields.push(component);

		});

		return generatedFields;

	},
	getFriendlyName: function (text) {

		// http://stackoverflow.com/questions/7888238/javascript-split-string-on-uppercase-characters
		splittedText = text.split(/(?=[A-Z])/); // alternate .match(/([A-Z]?[^A-Z]*)/g).slice(0,-1)
		modifiedText = splittedText.join(' ');

		return modifiedText;
	},
	openTab: function (record, editMode) {
		var tabPanelList = Ext.ComponentQuery.query('#mainTabPanel');
		var editMode = typeof editMode != 'undefined' ? editMode : false;

		if (tabPanelList.length === 0) {
			Ext.Msg.alert('Error', 'Cannot open tab because the main tab panel ID not found.');
			return;
		}

		if (record.data.hasOwnProperty('handler') === false
            || record.data.hasOwnProperty('handler') && record.data.handler.length === 0) {
			return;
		}

		var tabPanel = tabPanelList[0];
		// open multitab for each record despite the operation (either read / edit)
		var tabId = '_panel_' + record.internalId + "_" + record.data.dataID;

		var tab = tabPanel.getComponent(tabId);

		if (!tab) {
			var _customParams = null;

			if (record.data.hasOwnProperty('_parameter') === true) {
				if (record.data._parameter != null) {

					var splitParams = record.data._parameter.split('&');

					var stringJson = '{';
					var delim = '';

					splitParams.forEach(function (item) {
						if (item.length != 0) {
							var splitVal = item.split('=');

							if (splitVal.length >= 2) {
								stringJson += delim + splitVal[0] + ':';

								if (splitVal[1].toLowerCase().indexOf('true') || splitVal[1].toLowerCase().indexOf('yes')) {
									stringJson += 'true';
								}
								else if (splitVal[1].toLowerCase().indexOf('false') || splitVal[1].toLowerCase().indexOf('no')) {
									stringJson += 'false';
								}
								else {
									stringJson += '"' + splitVal[1] + '"'
								}
							}

							delim = ',';
						}
					});

					stringJson += '}';

					if (stringJson.length != 0) {
						_customParams = Ext.decode(stringJson);
					}
				}
			}

			var view = Ext.create(record.data.handler, {
				itemId: tabId,
				dataID: record.data.dataID,
				record: record.data.record,
				editMode: editMode,
				title: record.data.text,
				defaults: { autoScroll: true },
				closable: true,
				closeAction: 'hide',
				hideMode: 'offsets',
				layout: 'border',
				__customParams: _customParams
			});

			tab = tabPanel.add(view);

			//tab = tabPanel.add({
			//    itemId: tabId,
			//    title: record.data.text,
			//    defaults: {autoScroll: true},
			//    closable: true,
			//    hideMode: 'offsets',
			//    layout: 'border',
			//    /*region: 'center',*/
			//    /*autoScroll: true,*/
			//    //items: view
			//});
		}

		tabPanel.setActiveTab(tab);
		return view; // maybe can be useful on near future
	},
	openTabExtended: function (node, params, closeExistingTab) {

		if (closeExistingTab === undefined) {
			closeExistingTab = false;
		}

		if (params === undefined) {
			params = {};
		}

		var tabPanelList = Ext.ComponentQuery.query('#mainTabPanel');

		if (tabPanelList.length === 0) {
			Ext.Msg.alert('Error', 'Cannot open tab because the main tab panel ID not found.');
			return;
		}

		if (node.data.hasOwnProperty('handler') === false
            || node.data.hasOwnProperty('handler') && node.data.handler.length === 0) {
			return;
		}

		var tabPanel = tabPanelList[0];
		var tabId = '_panel_' + node.internalId;
		var tab = tabPanel.getComponent(tabId);
		var view = {};

		if (closeExistingTab === true) {
			tabPanel.remove(tab);
			tab = null;
		}

		if (!tab) {

			view = Ext.create(node.data.handler, {
				itemId: tabId,
				title: node.data.text,
				defaults: { autoScroll: true },
				closable: true,
				closeAction: 'hide',
				hideMode: 'offsets',
				layout: 'border',
				__customParams: params
			});

			tab = tabPanel.add(view);

			//tab = tabPanel.add({
			//    itemId: tabId,
			//    title: record.data.text,
			//    defaults: {autoScroll: true},
			//    closable: true,
			//    hideMode: 'offsets',
			//    layout: 'border',
			//    /*region: 'center',*/
			//    /*autoScroll: true,*/
			//    //items: view
			//});
		}

		tabPanel.setActiveTab(tab);
		return view; // maybe can be useful on near future
	},
	storeFactory: function (storeId, storeClassName, autoload) {

		if (autoload === undefined) {
			autoload = false;
		}

		if (storeClassName === undefined) {
			var firstUpper = storeId[0].toUpperCase();
			storeClassName = firstUpper + storeId.substr(1);
		}

		//if (typeof storeId === 'object') {
		//  console.log(storeId);
		//storeId = 'testStore';
		//}

		var store = Ext.data.StoreManager.lookup(storeId);
		//console.log(storeId);
		//console.log(store);
		if (store === undefined) {
			store = Ext.create('MyApp.store.' + storeClassName, {
				storeId: storeId,
				autoLoad: autoload,
			});
		}

		return store;
	},
	isInt: function (value) {
		if (isNaN(value)) {
			return false;
		}
		var x = parseFloat(value);
		return (x | 0) === x;
	},
	guid: function () {
		function s4() {
			return Math.floor((1 + Math.random()) * 0x10000)
			  .toString(16)
			  .substring(1);
		}

		return s4() + s4();
	},
	showMask: function (message) {
		var vp = Ext.ComponentQuery.query('app-main')[0];
		vp.mask(message);
	},
	unMask: function () {
		var vp = Ext.ComponentQuery.query('app-main')[0];
		vp.unmask();
	},
	showInfo: function (message, title) {
		if (title === undefined) {
			title = 'Information'
		}
		Ext.Msg.show({
			title: title,
			message: message,
			buttons: Ext.Msg.OK,
			icon: Ext.Msg.INFO
		});
	},
	showWarning: function (message, title) {
		if (title === undefined) {
			title = 'Warning'
		}
		Ext.Msg.show({
			title: title,
			message: message,
			buttons: Ext.Msg.OK,
			icon: Ext.Msg.WARNING
		});
	},
	showError: function (message, title) {
		if (title === undefined) {
			title = 'Error'
		}
		Ext.Msg.show({
			title: title,
			message: message,
			buttons: Ext.Msg.OK,
			icon: Ext.Msg.ERROR
		});
	},
	encodeText: function (rawText) {
		return encodeURI(rawText);
	},
	decodeText: function (encodedText) {
		return decodeURI(encodedText);
	},
	fnApplySpecificPermission: function (resourceCode, type, arrComponent) {
		var permission = MyApp.UserProfile.getPermissionForm();

		var listPermission = permission.filter(function (item) {
			if (item.ResourceCode == resourceCode) {
				return true;
			}
		});

		if (listPermission != null) {
			listPermission.forEach(function (item) {
				if (item.CanAccess == true && item.JsCodes != null) {
					var objJsCodes = Ext.decode(item.JsCodes);

					switch (type.toLowerCase()) {
						case 'button':
							if (arrComponent != null) {
								arrComponent.filter(function (item) {
									var tempResult = objJsCodes.button.filter(function (btn) {
										if (btn.ItemId.indexOf(item) != -1 || btn.Reference.indexOf(item) != -1) {
											return true;
										}
									});

									if (tempResult.length != 0) {
										var comp = Ext.ComponentQuery.query('#' + tempResult[0].ItemId);

										if (comp != null) {
											if (comp.length != 0) {
												comp[0].show();
											}
										}
									}
								});
							}
							break;
					}
				}
			});
		}
	},
	fnReloadAllStore: function () {
		var arrPanel = [
			{
				panel: 'MyEvaluation', //Evaluation List, execute button
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button',
					}
				]
			},
			{
				panel: 'MyEvaluationReview', //My Evaluation, execute button
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button',
					}
				]
			},
			{
				panel: 'MyEvaluationClarification', //Clarification List, refresh Grid
				items: [
					{
						compId: 'id_EvaluationClarification',
						type: 'reload-store'
					},
					{
						compId: 'id_ClarificationComment',
						type: 'reload-store'
					},
					{
						compId: 'id_formDetailClarification',
						type: 'fn',
						fnName: 'reloadRecord'
					},
				]
			},
			{
				panel: 'MyEvaluationApproval',
				items: [
					{
						compId: 'id_EvaluationApproval',
						type: 'reload-store'
					},
					{
						compId: 'id_EvaluationApproval_EvaluationReview',
						type: 'reload-store'
					}
				]
			},
			{
				panel: 'MyFinalApproval',
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button'
					}
				]
			},
			{
				panel: 'MyEvaluationForm',
				items: [
					{
						compId: 'id_GridClarification',
						type: 'fn',
						fnName: 'reloadClarification'
					},
				]
			},
			{
				panel: 'MyClarificationForm',
				items: [
					{
						compId: 'id_GridComments',
						type: 'reload-store'
					}
				],
			},
			{
				panel: 'MyPreliminaryReport',
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button'
					}
				],
			},
			{
				panel: 'MyFinalReport',
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button'
					}
				],
			},
			{
				panel: 'MyReportClarification',
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button'
					}
				],
			},
			{
				panel: 'MyReportEvaluationReview',
				items: [
					{
						compId: 'id_BtnRefresh',
						type: 'execute-button'
					}
				],
			}
		];

		var me = this;

		for (var i = 0; i < arrPanel.length; i++) {

			var panelXtype = arrPanel[i].panel;
			var panel = Ext.ComponentQuery.query(panelXtype);
			var items = arrPanel[i].items;
			var isPanelEmpty = Ext.isEmpty(panel);
			if (isPanelEmpty || (isPanelEmpty == false && panel.length == 0)) {
				continue;
			}

			var currentPanel = panel[0];

			//get tab panel component
			var tabPanel = currentPanel.up('tabpanel');
			//if (Ext.isEmpty(tabPanel)) {
			//	continue;
			//}

			//get active tab panel
			var activeTab = null;

			if (Ext.isEmpty(tabPanel) == false) {
				activeTab = tabPanel.getActiveTab();
			}

			//if there is any active tab with appropiate xtype
			if (Ext.isEmpty(activeTab) == false && activeTab.xtype == panelXtype && panelXtype != 'MyEvaluationForm') { // no need to shown popup for evaluation review form

				if (Ext.isEmpty(activeTab._IsPopupReloadShown)) {
					activeTab._IsPopupReloadShown = false; // prevent auto reload when user do filtering or else
				}

				// why i'm doing this, read this http://stackoverflow.com/questions/500431/what-is-the-scope-of-variables-in-javascript
				var _items = items;
				var _currentPanel = currentPanel;
				var _panelXtype = panelXtype;

				if (activeTab._IsPopupReloadShown == false) {

					activeTab._IsPopupReloadShown = true;

					Ext.Msg.confirm('Confirmation', 'There was an updated data, do you want to reload your current List or Form?', function (confirm) {

						activeTab._IsPopupReloadShown = false;

						if (confirm == 'no' || confirm == 'cancel') {
							return;
						}

						me._ExecReload(_items, _currentPanel, _panelXtype);
					});
				}

				continue;
			}

			me._ExecReload(items, currentPanel, panelXtype);
		}
	},
	_ExecReload: function (items, currentPanel, panelXtype) {
		for (var j = 0; j < items.length; j++) {

			var compId = items[j].compId; //console.log(compId);
			var compType = items[j].type;
			var fnName = Ext.isEmpty(items[j].fnName) ? '' : items[j].fnName;
			var childComponent = Ext.ComponentQuery.query('#' + compId, currentPanel);
			var isChildCompEmpty = Ext.isEmpty(childComponent);

			if (isChildCompEmpty || (isChildCompEmpty == false && childComponent.length != 0)) {
				var currentComp = childComponent[0];
				switch (compType) {
					case 'execute-button':
						if (currentComp.fireEvent) {
							currentComp.fireEvent('click');
						}
						break;
					case 'reload-store':
						if (currentComp.getStore) {
							currentComp.getStore().load();
						}
						break;
					case 'fn': // call specifed method or function
						var parentComp = currentComp.up(panelXtype);
						if (Ext.isEmpty(parentComp)) {
							return;
						}

						var parentCompController = parentComp.getController();
						if (Ext.isEmpty(parentCompController)) {
							return;
						}

						if (typeof parentCompController[fnName] === 'function') {
							parentCompController[fnName]();
						}
						break;
				}
			}
		}

	},
	nullToZero: function (model, attributeName, allowBlank) {
		if (Ext.isEmpty(allowBlank)) {
			allowBlank = false;
		}
		var value = model[attributeName];
		return Ext.isEmpty(value, allowBlank) ? 0 : value;
	},
	fnGenerateId: function (prefix, suffix, totalRandomText) {
		if (totalRandomText == undefined || totalRandomText == null) {
			totalRandomText = 5;
		}

		var currDate = Ext.Date.format(new Date(), 'YmdHis');
		var text = "";
		var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		for (var i = 0; i < totalRandomText; i++) {
			text += possible.charAt(Math.floor(Math.random() * possible.length));
		}

		return prefix + text + currDate + suffix;
	},
	getComp: function (xtype, itemId) {
		var compQuery = Ext.ComponentQuery.query(xtype + '#' + itemId);
		if (compQuery.length == 0) return null;
		return compQuery[0];
	},
	getOrdinalNumber: function (n) {
		var s = ["th", "st", "nd", "rd"];
		var v = n % 100;

		return n + (s[(v - 20) % 10] || s[v] || s[0]);
	}
});

Ext.define('MyApp.UserProfile', {
	singleton: true,

	config: {
		listProject: null,
		//projectActive: null,
		listRole: null,
		roleActive: null,
		userId: null,
		userName: null,
		employeeId: null,
		employeeName: null
	},

	constructor: function () {
		//this.setUserId(2);
		//this.setUserName('Aubing Asworo Rinobono');
	}
});

Ext.define('Override.form.field.VTypes', {
	override: 'Ext.form.field.VTypes',
	daterange: function (val, field) {
		var date = field.parseDate(val);

		if (!date) {
			return false;
		}

		var form = field.up('form');

		if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
			var start = form.getForm().findField(field.startDateField);
			start.setMaxValue(date);
			start.validate();
			this.dateRangeMax = date;
		}
		else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
			var end = form.getForm().findField(field.endDateField);
			end.setMinValue(date);
			end.validate();
			this.dateRangeMin = date;
		}
		/*
		 * Always return true since we're only using this vtype to set the
		 * min/max allowed values (these are tested for after the vtype test)
		 */
		return true;
	},

	daterangeText: 'Start date must be less than end date',
});