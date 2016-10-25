
Ext.define('MyApp.view.parkingSpacePrice.ParkingSpacePrice', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyParkingSpacePrice',
    requires: [
        'MyApp.view.parkingSpacePrice.ParkingSpacePriceController',
        'MyApp.store.ParkingSpacePriceStore'
    ],
    controller: 'parkingSpacePrice',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('parkingSpacePriceStore'),
        columns: MyApp.GlobalFunc.getGridColumns('parkingSpacePriceStore', true),
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
                store: Ext.data.StoreManager.lookup('parkingSpacePriceStore')
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
            items:
            [
               {
                   xtype: "numberfield",
                   name: "ID",
                   fieldLabel: "I D",
                   hidden: true,
                   readOnly: false,
                   value: 0,
                   msgTarget: "side"
               },
               {
                   xtype: 'MyLookupComponent',
                   createNewOnEnter: false,
                   fieldLabel: 'ParkingSpaceID',
                   customMultiSelect: false,
                   store: Ext.create('MyApp.store.ParkingSpaceStore',
                   {
                       storeId: 'ParkingSpaceStoreID',
                       autoLoad: true,
                   }),
                   displayField: 'Spot',
                   valueField: 'ID',
                   allowBlank: false,
                   msgTarget: 'side',                   
                   name: 'ParkingSpaceID',                   
                   itemId: 'itemId_LookupParkingSpaceID',
               },
               {
                   xtype: "textfield",
                   name: "ParkingSpaceIDShadow",
                   fieldLabel: "Price",
                   hidden: true,
                   msgTarget: "side"
               },
               //{
               //    xtype: "numberfield",
               //    name: "ParkingSpaceID",
               //    fieldLabel: "Parking Space ID",
               //    hidden: false,
               //    readOnly: false,
               //    value: 0,
               //    msgTarget: "side"
               //},
               {
                   xtype: "datefield",
                   name: "StartDate",
                   fieldLabel: "Start Date",
                   hidden: false,
                   readOnly: false,
                   msgTarget: "side",
                   "format": "d-M-Y"
               },
               {
                   xtype: "datefield",
                   name: "EndDate",
                   fieldLabel: "End Date",
                   hidden: false,
                   readOnly: false,
                   msgTarget: "side",
                   "format": "d-M-Y"
               },
               {
                   xtype: "numberfield",
                   name: "Price",
                   fieldLabel: "Price",
                   hidden: false,
                   msgTarget: "side"
               },
                {
                    xtype: 'combobox',
                    name: 'Category',
                    itemId: 'itemId_Category',
                    labelAlign: 'left',
                    fieldLabel: 'Category',
                    editable: false,
                    allowBlank: false,
                    store: Ext.create('Ext.data.Store', {
                        fields: ['Val'],
                        data: [
				            { Val: 'promo', Desc: 'promo' },
				            { Val: 'holiday', Desc: 'holiday' },
				            { Val: 'weekend', Desc: 'weekend' },
				            { Val: 'weekday', Desc: 'weekday' }
                        ],
                    }),
                    displayField: 'Desc',
                    valueField: 'Val',
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
