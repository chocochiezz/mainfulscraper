
Ext.define('MyApp.view.ad.Ad', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyAd',
    requires: [
        'MyApp.view.ad.AdController',
        'MyApp.store.AdStore'
    ],
    controller: 'ad',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('adStore'),
        columns: MyApp.GlobalFunc.getGridColumns('adStore', true),
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
                store: Ext.data.StoreManager.lookup('adStore')
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
                    xtype: 'combobox',
                    name: 'ContentSource',                    
                    itemId: 'itemId_ContentSource',
                    labelAlign: 'left',
                    fieldLabel: 'Content Source',
                    editable: false,
                    allowBlank: false,
                    store: Ext.create('Ext.data.Store', {
                        fields: ['Val'],
                        data: [
                            { Val: 'place', Desc: 'Places' },
                            { Val: 'event', Desc: 'Event' },
                            { Val: 'seminar', Desc: 'Seminar' },
                            { Val: 'promo', Desc: 'Promo' }
                        ],
                    }),
                    listeners: {
                        change: 'onContentSourceChange'
                    },
                    displayField: 'Desc',
                    valueField: 'Val',
                    msgTarget: 'side'
                },
                //{
                //    xtype: 'MyLookupComponent',
                //    createNewOnEnter: false,
                //    fieldLabel: 'ContentID',
                //    multiSelect: false,
                //    store: Ext.create('MyApp.store.ContentIDStore',
                //    {
                //        storeId: 'ContentIDStoreID',
                //        autoLoad: true,
                //    }),
                //    valueField: 'ID',
                //    allowBlank: false,
                //    msgTarget: 'side',
                //    displayField: 'ContentName',
                //    name: 'ContentID',
                //    //reference: 'ref_LookupProject',
                //    itemId: 'itemId_LookupContentID',
                //},

                {
                    xtype: 'combobox',
                    name: 'ContentID',
                    itemId: 'itemId_ContentID',
                    fieldLabel: 'Content',
                    labelAlign: 'left',
                    editable: false,
                    margin: '0 0 0 10',
                    store: Ext.create('MyApp.store.ContentIDStore',
                    {
                        storeId: 'ContentIDStoreID',
                        autoLoad: true,
                    }),
                    valueField: 'ID',
                    displayField: 'ContentName',
                    allowBlank: false,                    
                    labelWidth: 75,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'AdType',
                    fieldLabel: 'Ad Type',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'checkbox',
                    name: 'IsActive',
                    fieldLabel: 'Is Active',
                    readOnly: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'datefield',
                    name: 'StartDate',
                    fieldLabel: 'Start Date',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    format: 'd-M-Y'
                },
                {
                    xtype: 'textfield',
                    name: 'StartTime',
                    fieldLabel: 'Start Time',
                    hidden: false,
                    readOnly: false,
                    allowBlank: false,
                    msgTarget: 'side',
                    format: 'H:i:s'
                },
                {
                    xtype: 'datefield',
                    name: 'EndDate',
                    fieldLabel: 'End Date',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    format: 'd-M-Y'
                },
                {
                    xtype: 'textfield',
                    name: 'EndTime',
                    fieldLabel: 'End Time',
                    hidden: false,
                    readOnly: false,
                    allowBlank: false,
                    msgTarget: 'side',
                    format: 'H:i:s'
                },
                {
                    xtype: 'textfield',
                    name: 'AdArea',
                    fieldLabel: 'Ad Area',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'numberfield',
                    name: 'Weight',
                    fieldLabel: 'Weight',
                    hidden: false,
                    msgTarget: 'side',
                    value:0
                },
                {
                    xtype: 'numberfield',
                    name: 'StartDateTime',
                    fieldLabel: 'Start Date Time',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'numberfield',
                    name: 'EndDateTime',
                    fieldLabel: 'End Date Time',
                    hidden: false,
                    allowBlank: false,
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
