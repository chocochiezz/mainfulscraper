
Ext.define('MyApp.view.tracking.Tracking', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyTracking',
    requires: [
        'MyApp.view.tracking.TrackingController',
        'MyApp.store.TrackingStore'
    ],
    controller: 'tracking',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('trackingStore'),
        columns: MyApp.GlobalFunc.getGridColumns('trackingStore', true),
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
                store: Ext.data.StoreManager.lookup('trackingStore')
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
