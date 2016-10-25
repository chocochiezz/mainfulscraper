// http://docs.sencha.com/extjs/5.1/core_concepts/components.html
Ext.define('My.Lookup.Component', {
	extend: 'Ext.form.field.Tag',
	xtype: 'MyLookupComponent',
	alias: 'widget.MyLookupComponent',
	//forceSelection: false, // if this set to false, somehow the default value from dataindex gridpanel will shown zero
	createNewOnEnter: false,
	//multiSelect: false,
	triggerOnClick: false,
	triggerCls: 'x-form-search-trigger',
	//triggerWrapCls: 'icon-book-open',
	triggers: {
		picker: {
			//weight: -10,
			//handler: 'onTriggerClick',
			handler: function () {
				this.openWindow();
			},
			scope: 'this'
		}
	},
	config: {
		popupWindowWidth: 500,
		popupWindowHeight: 350,
		showFields: [],
	},
	//initComponent: function () {
	//	this.callParent(arguments); // call the superclass 
	//	//console.log(this.items);
	//},
	/* CUSTOM METHOD 
	---------------------------------------------------------------------------*/
	openWindow: function () {
		var fieldLabel = this.getFieldLabel();
		var popupWindoWidth = this.getPopupWindowWidth();
		var popupWindowHeight = this.getPopupWindowHeight();
		var currentStore = this.getStore();
		var showFields = this.getShowFields();
		var gridColumns = MyApp.GlobalFunc.getGridColumns(currentStore.getStoreId(), true, showFields);
		var isMultiselect = this.customMultiSelect;
		var currentSelModel = 'rowmodel';

		if (isMultiselect) {
			currentSelModel = {
				selType: 'checkboxmodel'
			};
		}

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
				columnLines: true,
				plugins: 'gridfilters',
				store: currentStore,
				columns: gridColumns,
				selModel: currentSelModel,
				listeners: {
					rowdblclick: function (grid, record, tr, rowIndex, e, eOpts) {
						var buttonSelect = grid.up().down('#itemId_BtnSelect');
						buttonSelect.fireEvent('click', buttonSelect);
					},
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
								var selected = [];

								for (var i = 0; i < selection.length; i++) {
									selected.push(selection[i].get(me.valueField));
								}

								me.setValue(selected);

								if (selected.length == 1) {
									try {
										me.fireEvent('select', me, selection[0]);
									}
									catch (err) {
										// sent error client to server if possible
										Ext.Msg.alert('Warning', err.message);
									}
								}

								grid = null;
								window.close();
							}
						},
					},
					//{
					//	xtype: 'textfield',
					//	emptyText: 'Keywords...',
					//	flex:1,
					//}
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

					//currentStore.clearFilter(); // don't clear all store, remove filter that has an ID xxxx
					var filters = currentStore.getFilters();
					//console.log(filters);
					if (Ext.isEmpty(filters)) {
						return;
					}

					for (var i = 0; i < filters.length; i++) {

						var filter = filters.getAt(i);

						if (filter.getId().indexOf('x-gridfilter') != -1) {
							currentStore.removeFilter(filter);
						}
					}

					//currentStore.removeFilter(null); // remove anonymous filter (doesn't have id)
					//console.log(currentStore.getFilters());
				}
			}
		});

		if (typeof me.value !== 'undefined' && me.value != null && me.value != '') {
			//currentStore.on('load', 'onLoad_StoreLookup', me, { single: false });
			var val = [];
			if (Ext.isArray(me.value)) {
				val = Ext.Array.merge(me.value);
			}
			else {
				val.push(me.value);
			}

			var grid = windowPopup.down('gridpanel');

			var valueField = me.valueField;

			var selectedData = [];
			val.forEach(function (item) {
				var exist = grid.getStore().queryBy(function (dt) {
					if (dt.get(valueField) == item) {
						return true;
					}
				});

				if (exist.length != 0) {
					selectedData.push(exist.items[0]);
				}
			});

			grid.getSelectionModel().select(selectedData);
		}

		windowPopup.show();
	},

	onLoad_StoreLookup: function (obj, records, successful, operation, eOpts) {
		var me = this;
		alert('loaded');
	},
});