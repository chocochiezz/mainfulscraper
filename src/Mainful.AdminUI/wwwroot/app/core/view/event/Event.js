
Ext.define('MyApp.view.event.Event', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyEvent',
    requires: [
        'MyApp.view.event.EventController',
        'MyApp.store.EventStore'
    ],
    controller: 'event',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('eventStore'),
        columns: MyApp.GlobalFunc.getGridColumns('eventStore', true),
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
                store: Ext.data.StoreManager.lookup('eventStore')
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
                    name: 'Title',
                    fieldLabel: 'Title',
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
                    xtype: 'datefield',
                    name: 'StartDate',
                    fieldLabel: 'Start Date',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    allowBlank: false,
                    format: 'd-M-Y'
                },
                {
                    xtype: 'datefield',
                    name: 'EndDate',
                    fieldLabel: 'End Date',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    allowBlank: false,
                    format: 'd-M-Y'
                },
                {
                    xtype: 'datefield',
                    name: 'StartTime',
                    fieldLabel: 'Start Time',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    format: 'H:i:s'
                },                
                {
                    xtype: 'datefield',
                    name: 'EndTime',
                    fieldLabel: 'End Time',
                    hidden: false,
                    readOnly: false,
                    msgTarget: 'side',
                    format: 'H:i:s'
                },
                {
                    xtype: 'textfield',
                    name: 'Timezone',
                    fieldLabel: 'Timezone',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Weblink',
                    fieldLabel: 'Weblink',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Facebook',
                    fieldLabel: 'Facebook',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Twitter',
                    fieldLabel: 'Twitter',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'GooglePlus',
                    fieldLabel: 'Google Plus',
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
                    name: 'Online',
                    fieldLabel: 'Online',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'combobox',
                    name: 'EventCategoryID',
                    itemId: 'itemId_EventCategoryID',
                    fieldLabel: 'Event Category',
                    labelAlign: 'left',
                    editable: false,
                    margin: '0 0 0 10',                    
                    store: Ext.create('MyApp.store.EventCategoryStore',
                    {
                        storeId: 'EventCategoryStoreID',
                        autoLoad: true,
                    }),
                    valueField: 'ID',
                    displayField: 'CategoryName',
                    allowBlank: false,
                    reference: 'refLookupCategory',
                    labelWidth: 75,
                    msgTarget: 'side'
                },
                {
                     xtype: 'combobox',
                     name: 'EventOrganizerID',
                     itemId: 'itemId_EventOrganizerID',
                     fieldLabel: 'Event Organizer',
                     labelAlign: 'left',
                     editable: false,
                     margin: '0 0 0 10',                     
                     store: Ext.create('MyApp.store.EventOrganizerStore',
                     {
                         storeId: 'EventOrganizerStoreID',
                         autoLoad: true,
                     }),
                     valueField: 'ID',
                     displayField: 'Name',
                     allowBlank: false,
                     reference: 'refLookupEventOrganizer',
                     labelWidth: 75,
                     msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Tag',
                    fieldLabel: 'Tag',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'checkbox',
                    name: 'IsFree',
                    fieldLabel: 'Is Free',
                    readOnly: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Currency',
                    fieldLabel: 'Currency',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'PriceRange',
                    fieldLabel: 'Price Range',
                    hidden: false,
                    msgTarget: 'side'
                },
                {
                    xtype: 'numberfield',
                    name: 'Priority',
                    fieldLabel: 'Priority',
                    hidden: false,
                    allowBlank: false,
                    msgTarget: 'side',
                    value:0
                },
                {
                    xtype: 'combobox',
                    name: 'EventType',                    
                    itemId: 'itemId_EventType',
                    labelAlign: 'left',
                    fieldLabel: 'Event Type',
                    editable: false,
                    allowBlank: false,
                    store: Ext.create('Ext.data.Store', {
                        fields: ['Val'],
                        data: [
                            { Val: '', Desc: 'Event' },
                            { Val: 'Seminar', Desc: 'Seminar' }
                        ],
                    }),
                    displayField: 'Desc',
                    valueField: 'Val',
                    msgTarget: 'side'
                },
                {
                    xtype: 'textfield',
                    name: 'Slug',
                    fieldLabel: 'Slug',
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
