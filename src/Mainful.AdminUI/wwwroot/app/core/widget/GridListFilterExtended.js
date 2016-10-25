Ext.define('Ext.grid.filters.filter.ListExtended', {
	extend: 'Ext.grid.filters.filter.List',
	alias: 'grid.filter.listExtended',
	type: 'listExtended',
	operator: 'in',

	//plain: false,
	//showSeparator: false,

	dataStore: {},
	modelList: {},
	storeList: {},
	gridList: {},

	alwaysLoad: true, // always reload the store when menu activated

	//menuDefaults: {
	//	xtype: 'menu',
	//	width: 500,
	//	layout: {
	//		type: 'vbox',
	//		align: 'begin'
	//	}
	//},

	/**
        * @cfg {Number} updateBuffer
        * Number of milliseconds to wait after user interaction to fire an update. Only supported
        * by filters: 'list', 'numeric', and 'string'.
        */
//	updateBuffer: 500,

	//showMenu: function (menuItem) {
	//	var me = this;

	//	if (!me.menu) {
	//		me.createMenu();
	//	}

	//	menuItem.activeFilter = me;

	//	menuItem.setMenu(me.menu, false);
	//	menuItem.setChecked(me.active);
	//	// Disable the menu if filter.disabled explicitly set to true.
	//	menuItem.setDisabled(me.disabled === true);

	//	me.activate(/*showingMenu*/ true);
	//},
	show: function () {
		var store = this.store;

		if (this.loadOnShow && this.alwaysLoad === true && store.hasPendingLoad() === false) {
			var me = this;
			store.load({
				callback: function (records, operation, success) {
					me.createMenuItems(store);
				}
			});
			
			return;
		}

		if (this.loadOnShow && !this.loaded && !store.hasPendingLoad()) {
			store.load();
		}
	},
	/** @private */
	createMenuItems: function (store, showMenuContain) {
		//console.log('createMenuItem');
		var me = this,
            menu = me.menu,
            len = store.getCount(),
            contains = Ext.Array.contains,
            listeners, itemDefaults, record, gid, idValue, idField, labelValue, labelField, i, item, processed;
		
		// B/c we're listening to datachanged event, we need to make sure there's a menu.
		if (len && menu) {
			listeners = {
				checkchange: me.onCheckChange,
				scope: me
			};

			itemDefaults = me.getItemDefaults();

			menu.suspendLayouts();
			menu.removeAll(true);

			gid = me.single ? Ext.id() : null;
			idField = me.idField;
			labelField = me.labelField;
			uniqueKey = idField + '_' + labelField;
			
			//if (me.dataStore.hasOwnProperty(uniqueKey) === false) {
			//	me.dataStore[uniqueKey] = [];
			//	me.modelList[uniqueKey] = Ext.create('Ext.data.Model', {
			//		fields: [
			//			{ name: idField, type: 'string' },
			//			{ name: labelField, type: 'string' },
			//		]
			//	});
			//	me.storeList[uniqueKey] = Ext.create('Ext.data.Store', {
			//		remoteFilter: false,
			//		remoteSort: false,
			//		remoteGroup: false,
			//		autoDestroy: true,
			//		pageSize: 0, // disable paging
			//		model: me.modelList[uniqueKey],
			//		proxy: {
			//			type: 'memory',
			//			reader: {
			//				type: 'json',
			//				rootProperty: ''
			//			}
			//		},
			//	});
			//	//me.gridList[uniqueKey] = Ext.create('Ext.grid.Panel', {
			//	//	//xtype: 'grid',
			//	//	border: false,
			//	//	width: 300,
			//	//	height: 200,
			//	//	group: gid,
			//	//	enableColumnHide: false,
			//	//	store: me.storeList[uniqueKey],
			//	//	columns: [
			//	//		{
			//	//			dataIndex: idField,
			//	//			text: idField,
			//	//			hidden: true,
			//	//		},
			//	//		{
			//	//			dataIndex: labelField,
			//	//			//text: labelField,
			//	//			text: 'Item',
			//	//			flex: 1
			//	//		}
			//	//	],
			//	//	selType: 'checkboxmodel',
			//	//});
		    //}

            //TRY convert : filter to always create new store
			var dataStorefd = []; //me.dataStore[uniqueKey] = [];

			var modelfd = //modelList[uniqueKey] =
                Ext.create('Ext.data.Model', {
			    fields: [
                    { name: idField, type: 'string' },
                    { name: labelField, type: 'string' },
			    ]
                });
            var storefd = 
			//me.storeList[uniqueKey] =
                Ext.create('Ext.data.Store', {
			    remoteFilter: false,
			    remoteSort: false,
			    remoteGroup: false,
			    autoDestroy: true,
			    pageSize: 0, // disable paging
			    model: modelfd, //me.modelList[uniqueKey],
			    proxy: {
			        type: 'memory',
			        reader: {
			            type: 'json',
			            rootProperty: ''
			        }
			    },
			});

            var dynamicStore = dataStorefd;
		    //var dynamicStore = me.dataStore[uniqueKey];

			if (dynamicStore.length == 0) {
				
				processed = [];
				store.each(function (item) {
					idValue = item.get(idField);
					labelValue = item.get(labelField);

					// Only allow unique values.
					if (labelValue == null || contains(processed, idValue)) {
						return;
					}

					processed.push(labelValue);
					
					var obj = {};
					obj[idField] = idValue;
					obj[labelField] = labelValue;
					dynamicStore.push(obj);
				});
			}

			var newStore = storefd;
			//var newStore = me.storeList[uniqueKey];
			newStore.loadData(dynamicStore);

			//var myGrid = me.gridList[uniqueKey];

			// add freetext filter
			menu.add(Ext.apply({
				xtype: 'textfield',
				emptyText: 'Filter keyword...',
				triggers: {
					search: {
						cls: 'x-form-search-trigger',
					}
				},
				group: gid,
				listeners: {
					change: function (textfield, newValue, oldValue, e) {
						newStore.clearFilter();
						newStore.setFilters(
							function (item) {
								var itemName = item.get(labelField).toLowerCase();
								var isFound = itemName.indexOf(newValue.toLowerCase());
								return (isFound == -1 ? false : true);
							}
						);
					},
					scope: me
				}
			}, itemDefaults));

			//menu.add(Ext.apply(myGrid, itemDefaults));
			//console.log(menu);
			menu.add(Ext.apply({
				xtype: 'grid',
				itemId: 'myGridFilter_' + uniqueKey,
				border: false,
				//reserveScrollbar: true,
				width: 300,
				height: 200,
				group: gid,
				autoDestroy: true,
				enableColumnHide: false,
				//scrollable: false,
				store: storefd, //me.storeList[uniqueKey],
                autodestroy: true,
				columns: [
					{
						dataIndex: idField,
						text: idField,
						hidden: true,
					},
					{
						dataIndex: labelField,
						text: '(Select all)',
						flex: 1
					}
				],
				selType: 'checkboxmodel',
				viewConfig: {
					//preserveScrollOnReload: true,
					//preserveScrollOnRefresh: true
				},
				listeners: {
					boxready: function (grid) {
						//grid.setScrollable(false);
						//grid.setScrollable('vertical'); // have empty space issue and layout error, but still can be justify
						//grid.setHeight(200);
						//grid.setScrollable({y: 'auto'});
					},
					viewReady: function (grid) {
						//grid.setScrollable('vertical'); // first look good, when open another list filter scrollbar going weird, error setElement
					},
					selectionchange: function (grid, selected, eOpts) {
						var _selected = [];
						for (var i = 0; i < selected.length; i++) {
							_selected.push(selected[i].get(idField));
						}
						me.menu._selected = _selected; // custom properties
						me.setValue();
					},
					afterrender: function (grid, eOpts) {
						//alert('after render');
						//grid.setScrollable('vertical');
					},
				}
			}, itemDefaults));

			menu.resumeLayouts(true);
		}
	},
	/**
	* @private
	* Template method that is to set the value of the filter.
	*/
	setValue: function () {

		var me = this,
            items = me.menu.items,
            value = [],
            i, len, checkItem;

		// The store filter will be updated, but we don't want to recreate the list store or the menu items in the
		// onDataChanged listener so we need to set this flag.
		me.preventDefault = true;

		//for (i = 0, len = items.length; i < len; i++) {
		//	checkItem = items.getAt(i);

		//	if (checkItem.xtype === 'textfield') {
		//		//value.push(checkItem.getValue());
		//		continue;
		//	}

		//	if (checkItem.checked) {
		//		value.push(checkItem.value);
		//	}
		//}
		var selectedItems = me.menu._selected;
		for (i = 0, len = selectedItems.length; i < len; i++) {
			checkItem = selectedItems[i];
			value.push(checkItem);
		}

		me.filter.setValue(value);
		len = value.length;

		if (len && me.active) {
			me.updateStoreFilter();
		} else {
			me.setActive(!!len);
		}

		me.preventDefault = false;
	},

});
