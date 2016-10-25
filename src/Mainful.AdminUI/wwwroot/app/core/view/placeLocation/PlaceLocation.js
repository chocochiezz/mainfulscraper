
Ext.define('MyApp.view.placeLocation.PlaceLocation', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyPlaceLocation',
    requires: [
        'MyApp.view.placeLocation.PlaceLocationController',
        'MyApp.store.PlaceLocationStore'
    ],
    controller: 'placeLocation',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('placeLocationStore'),
        columns: MyApp.GlobalFunc.getGridColumns('placeLocationStore', true),
        selType: 'rowmodel',
        plugins: 'gridfilters',
        stateful: true,
		border: false,
		columnLines: true,
        tbar: [
			{
				xtype: 'toolbar',
				bodyPadding: 5,
				flex: 1,
				border: false,
				items: [
					{
						xtype: 'button',
						reference: 'ref_BtnExport',
						itemId: 'btnExportId',
						iconCls: 'icon-page-excel',
						text: 'Export To Excel',
						hidden:true,
						listeners: {
							click: 'onExportClick'
						}
					},
					{xtype: 'tbfill'},
					{
						xtype: 'button',
						reference: 'ref_BtnAdd',
						itemId: 'BtnAdd',
						text: 'Add',
						iconCls: 'icon-page-add',
						listeners: {
							click: 'onAddClick'
						}
					},
					{
						xtype: 'button',
						reference: 'ref_BtnEdit',
						itemId: 'BtnEdit',
						disabled: true,
						text: 'Edit',
						iconCls: 'icon-page-edit',
						listeners:{
							click: 'onEditClick'
						}
					},
					{
						xtype: 'button',
						reference: 'ref_BtnRemove',
						itemId: 'BtnRemove',
						disabled: true,
						text: 'Remove',
						iconCls: 'icon-page-delete',
						listeners: {
							click: 'onRemoveClick'
						}
					},
				]
			}
        ],

        bbar: [
            {
             	xtype: 'pagingtoolbar',   
				displayInfo: true,
				flex: 1,
				border: false,
                store: Ext.data.StoreManager.lookup('placeLocationStore')
            },
        ],

        listeners: {
            selectionchange: 'onSelectionChange',
            itemcontextmenu: 'onRowContextMenu',
        }

    }, { 
            xtype: 'form',
            reference: 'myForm',
            layout: 'form',
			border: false,
            //itemId: 'mainFormPanel',
            region: 'east',
            title: 'Form',
            collapsible: true,
            split: true,
            width: 500,
            collapsed: true,
            bodyPadding: 10,
            scrollable: true,
            jsonSubmit: true,
            closeAction: 'hide',
            defaultType: 'textfield',
			animCollapse: false,
            tbar: [
                {xtype: 'tbfill'},
                {
                    xtype: 'button',
					itemId: 'BtnSave',
                    text: 'Save',
                    iconCls: 'icon-page-edit',
                    handler: 'onBtnSaveClick'
                },
            ],
            items: [
                       {
                           xtype: 'numberfield',
                           name: 'ID',
                           fieldLabel: 'I D',
                           hidden: true,
                           readOnly: false,
                           value: 0,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'PlaceName',
                           fieldLabel: 'Place Name',
                           hidden: false,
                           allowBlank: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Description',
                           fieldLabel: 'Description',
                           hidden: false,                           
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'PlaceNote',
                           fieldLabel: 'Place Note',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'checkbox',
                           name: 'IsVenue',
                           fieldLabel: 'Is Venue',
                           readOnly: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Address1',
                           fieldLabel: 'Address1',
                           hidden: false,
                           allowBlank: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Address2',
                           fieldLabel: 'Address2',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'combobox',
                           name: 'Country',
                           itemId: 'itemId_Country',
                           fieldLabel: 'Country',
                           labelAlign: 'left',
                           editable: false,
                           margin: '0 0 0 10',
                           store: Ext.create('MyApp.store.CountryStore',
                           {
                               storeId: 'CountryStoreID',
                               autoLoad: true,
                           }),
                           valueField: 'CountryName',
                           displayField: 'CountryName',
                           allowBlank: true,
                           reference: 'refCountry',
                           labelWidth: 75,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'combobox',
                           name: 'State',
                           itemId: 'itemId_State',
                           fieldLabel: 'State',
                           labelAlign: 'left',
                           editable: false,
                           margin: '0 0 0 10',
                           store: Ext.create('MyApp.store.CityStore',
                           {
                               storeId: 'CityStoreID',
                               autoLoad: true,
                           }),
                           valueField: 'Subdivision_1_Name',
                           displayField: 'Subdivision_1_Name',
                           allowBlank: true,
                           reference: 'refState',
                           labelWidth: 75,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'combobox',
                           name: 'City',
                           itemId: 'itemId_City',
                           fieldLabel: 'City',
                           labelAlign: 'left',
                           editable: false,
                           margin: '0 0 0 10',
                           store: Ext.create('MyApp.store.CityStore',
                           {
                               storeId: 'CityStoreID',
                               autoLoad: true,
                           }),
                           valueField: 'CityName',
                           displayField: 'CityName',
                           allowBlank: true,
                           reference: 'refCity',
                           labelWidth: 75,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'ZipPostal',
                           fieldLabel: 'Zip Postal',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Latitude',
                           fieldLabel: 'Latitude',
                           hidden: false,
                           allowBlank: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Longitude',
                           fieldLabel: 'Longitude',
                           hidden: false,
                           allowBlank: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'numberfield',
                           name: 'Priority',
                           fieldLabel: 'Priority',
                           hidden: false,
                           value:0,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'combobox',
                           name: 'PlaceCategoryID',
                           itemId: 'itemId_PlaceCategoryID',
                           fieldLabel: 'PlaceCategory',
                           labelAlign: 'left',
                           editable: false,
                           margin: '0 0 0 10',
                           store: Ext.create('MyApp.store.PlaceCategoryStore',
                           {
                               storeId: 'PlaceCategoryStoreID',
                               autoLoad: true,
                           }),
                           valueField: 'ID',
                           displayField: 'CategoryName',
                           allowBlank: true,
                           reference: 'refPlaceCategory',
                           labelWidth: 75,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Phone',
                           fieldLabel: 'Phone',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Weblink',
                           fieldLabel: 'Weblink',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Email',
                           fieldLabel: 'Email',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'textfield',
                           name: 'Slug',
                           fieldLabel: 'Slug',
                           hidden: false,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'numberfield',
                           name: 'SubCategoryID',
                           fieldLabel: 'Sub Category I D',
                           hidden: false,
                           readOnly: false,
                           value: 0,
                           msgTarget: 'side'
                       },
                       {
                           xtype: 'checkbox',
                           name: 'HasParking',
                           fieldLabel: 'Has Parking',
                           readOnly: false,
                           msgTarget: 'side'
                       }
            ]
        },
        {
            xtype: 'menu',
            reference: 'myGridPanelMenu',
            closeAction: 'hide',
            margin: '0 0 10 0',
            renderTo: Ext.getBody(),
            items: [{
                text: 'Edit',
                handler: 'onContextBtnEditClick',
            },{
                text: 'Remove',
                handler: 'onContextBtnRemoveClick',
            }]
        },
    ],
});	
