
Ext.define('MyApp.view.feeds.Feeds', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyFeeds',
    requires: [
        'MyApp.view.feeds.FeedsController',
        'MyApp.store.FeedsStore'
    ],
    controller: 'feeds',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('feedsStore'),
        columns: MyApp.GlobalFunc.getGridColumns('feedsStore', true),
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
                store: Ext.data.StoreManager.lookup('feedsStore')
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
                   name: 'FeedChannel',
                   fieldLabel: 'Feed Channel',
                   hidden: false,
                   allowBlank: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'Content',
                   fieldLabel: 'Content',
                   hidden: false,
                   allowBlank: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'ImgUrl',
                   fieldLabel: 'Img Url',
                   hidden: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'TargetTags',
                   fieldLabel: 'Target Tags',
                   hidden: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'datefield',
                   name: 'PushDate',
                   fieldLabel: 'Push Date',
                   hidden: false,
                   readOnly: false,
                   msgTarget: 'side',
                   format: 'd-M-Y H:i:s'
               },
               {
                   xtype: 'numberfield',
                   name: 'PriorityWeight',
                   fieldLabel: 'Priority Weight',
                   hidden: false,
                   msgTarget: 'side',
                   minValue: 0,
                   value:0
               },
               {
                   xtype: 'datefield',
                   name: 'PlanPushDate',
                   fieldLabel: 'Plan Push Date',
                   hidden: false,
                   readOnly: false,
                   msgTarget: 'side',
                   allowBlank: false,
                   format: 'd-M-Y H:i:s'
               },
               {
                   xtype: 'textfield',
                   name: 'TrackingID',
                   fieldLabel: 'Tracking I D',
                   hidden: false,
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
