// http://docs.sencha.com/extjs/5.1/core_concepts/components.html
Ext.define('Single.Lookup.Component', {
	extend: 'Ext.form.field.Text',
	xtype: 'SingleLookupComponent',
	alias: 'widget.SingleLookupComponent',
	triggers: {
		clear: {
			cls: 'x-form-clear-trigger',
			handler: function () {
				this.reset();
				this._submitValue = null;
			}
		},
		picker: {
			cls: 'x-form-search-trigger',
			handler: function () {
				this.openWindow();
			},
			scope: 'this'
		},
	},
	config: {
		popupWindowWidth: 850,
		popupWindowHeight: 500,
		showFields: [],
		store: null, // mandatory
		valueField: null, // mandatory
		displayField: null, // mandatory
		queryMode: 'remote', // || 'local' 
	},

	_submitValue: null, // submitted data
	_localCache: [],
	_defaultFilters: null,

	setLocalCache: function (key, value) {
		this._localCache[key] = value;
	},

	getLocalCache: function (key) {
		var localCache = this._localCache;
		if (localCache.hasOwnProperty(key)) {
			return localCache[key];
		}
		return null;
	},

	getSubmitValue: function () {
		//console.log('getSubmitValue() > this._submitValue: ' + this._submitValue);
		if (this._submitValue !== null) {
			return this._submitValue;
		}

		return this.processRawValue(this.getRawValue());
	},

	getValue: function () {
		//console.log('getValue() > this._submitValue: ' + this._submitValue);
		if (this._submitValue !== null) {
			return this._submitValue;
		}

		var me = this,
		val = me.rawToValue(me.processRawValue(me.getRawValue()));
		me.value = val;
		return val;
	},

	getDisplayValue: function () {
		var me = this,
            val = me.rawToValue(me.processRawValue(me.getRawValue()));
		me.value = val;
		return val;
	},

	setValue: function (value) {

		// ------------------------------------------------------------------------------------------------------------
		// CUSTOM BEHAVIOR
		// ------------------------------------------------------------------------------------------------------------
		var displayField = this.getDisplayField();
		var valueField = this.getValueField();
		var store = this.getStore();

		//var record = store.findRecord(valueField, value, 0, false, false, true);
		var records = store.query(valueField, value, false, false, true);
		var record = (records.length == 0) ? null : records.getAt(0);

		var cachedRecord = this.getLocalCache(value);

		// too expensive, so commented
		//if (cacheRecord != null && record != null && Ext.Object.equals(cachedRecord.getData(), record.getData()) == false) {
		//	cachedRecord = record;
		//}

		if (cachedRecord != null && record == null) {
			record = cachedRecord;
		}

		//if (record == null) {
		//	Ext.Msg.alert('Warning', 'SingleLookupComponent.listeners.change: Cannot get value from ' + displayField);
		//	return;
		//}

		if (record != null) {
			// cache the value temporary, prevent invalid behavior when using queryMode 'remote'
			this.setLocalCache(value, record); // sequence is matter, because variable value will be change next
			//console.log('setValue() > set submit value');
			this._submitValue = record.get(valueField);

			// enhancement 2016 Jan 25
			displayFieldList = displayField.split(',');
			var concatedValue = '';
			for (var i = 0; i < displayFieldList.length; i++) {
				concatedValue += record.get(displayFieldList[i].trim()) + ' ';
			}
			value = concatedValue;
			//value = record.get(displayField);
		}

		//console.log(value);

		// ------------------------------------------------------------------------------------------------------------

		var me = this;
		me.setRawValue(me.valueToRaw(value));
		return me.mixins.field.setValue.call(me, value);
	},

	listeners: {
		change: function (textfield, newValue, oldValue, eOpts) {
			if (newValue == 0) {
				textfield.suspendEvents();
				textfield.reset();
				textfield.resumeEvents();
				return;
			}
		},
		destroy: function (textfield, eOpts) {

			var defaultFilters = this._defaultFilters;
			var store = this.getStore();

			if (defaultFilters !== null && defaultFilters.length > 0) {
				var filters = [];
				for (var i = 0; i < defaultFilters.length; i++) {
					filters.push(defaultFilters[i]);
				}
				store.clearFilter(true);// true the filter is cleared silently.
				store.setFilters(filters);
				//store.load();
				return;
			}

			store.clearFilter(); // if remote query this will get all with paging based on pageSize, but how if the pageSize is 10 (not get all data)
			//Ext.destroy(this.getStore());
		},
	},

	initComponent: function () {
		this.callParent(arguments); // call the superclass 
		var store = this.getStore();
		var storeFilters = store.getFilters();
		var filters = [];
		storeFilters.each(function (filter, idx) {
			filters.push(filter);
		});
		this._defaultFilters = filters;

		if (store.getAutoLoad() === false) {
			store.load(); // load manually
		}

		var queryMode = this.getQueryMode();
		var isRemote = (queryMode === 'local') ? false : true;
		store.setRemoteFilter(isRemote);
		store.setRemoteSort(isRemote);
	},

	/* CUSTOM METHOD 
	---------------------------------------------------------------------------*/
	openWindow: function () {

		if (this.store === null) {
			Ext.Msg.alert('Warning', 'You must define the data store.');
			return;
		}

		//this.store.clearFilter(); 

		var fieldLabel = this.getFieldLabel();
		var popupWindoWidth = this.getPopupWindowWidth();
		var popupWindowHeight = this.getPopupWindowHeight();
		var currentStore = this.getStore();
		var showFields = this.getShowFields();
		var gridColumns = MyApp.GlobalFunc.getGridColumns(currentStore.getStoreId(), true, showFields);
		var currentSelModel = 'rowmodel';
		var me = this;

		var windowPopup = Ext.create('Ext.window.Window', {
			title: fieldLabel,
			constraint: true,
			constrainHeader: true,
			modal: true,
			height: popupWindowHeight,
			width: popupWindoWidth,
			layout: 'fit',
			items: {
				xtype: 'grid',
				border: false,
				plugins: 'gridfilters',
				store: currentStore,
				columns: gridColumns,
				selModel: currentSelModel,
				listeners: {
					rowdblclick: function (grid, record, tr, rowIndex, e, eOpts) {
						var buttonSelect = grid.up().down('#itemId_BtnSelect');
						buttonSelect.fireEvent('click', buttonSelect);
					},
					//destroy: function (obj) {
					//	currentStore.clearFilter(true); // Uncaught TypeError: Cannot read property 'getHeaderByDataIndex' of null
					//}
				},
				tbar: [
					{
						xtype: 'button',
						itemId: 'itemId_BtnSelect',
						iconCls: 'icon-accept',
						text: 'Choose Selected(s)',
						listeners: {
							click: function (button, e) {

								var window = button.up('window');
								var grid = button.up('gridpanel');
								var selection = grid.getSelectionModel().getSelection();
								
								if (selection.length == 0) {
									Ext.Msg.alert('Warning', 'Please select the row.');
									return;
								}

								var selected = selection[0];
								
								me._submitValue = selected.get(me.valueField);

								try {
									me.fireEvent('select', me, selected);
								}
								catch (err) {
									// sent error client to server if possible
									Ext.Msg.alert('Warning', err.message);
								}

								me.suspendEvents();

								// enhancement 2016 Jan 25
								displayFieldList = me.displayField.split(',');
								var concatedValue = '';
								for (var i = 0; i < displayFieldList.length; i++) {
									concatedValue += selected.get(displayFieldList[i].trim()) + ' ';
								}

								//me.setValue(selected.get(me.displayField));
								me.setValue(concatedValue);
								me.resumeEvents();

								grid = null;
								window.close();
							}
						},
					},
				],
				bbar: [
					{
						xtype: 'pagingtoolbar',
						displayInfo: true,
						store: currentStore,
						flex: 1,
						border: false,
					},
				],
			},
			listeners: {
				close: function (panel, eOpts) {
					currentStore.clearFilter();
					me._submitValue = null;
				}
			}
		});

		windowPopup.show();
	},
});