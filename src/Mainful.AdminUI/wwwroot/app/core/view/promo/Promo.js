
Ext.define('MyApp.view.promo.Promo', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyPromo',
    requires: [
        'MyApp.view.promo.PromoController',
        'MyApp.store.PromoStore'
    ],
    controller: 'promo',
	border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('promoStore'),
        columns: MyApp.GlobalFunc.getGridColumns('promoStore', true),
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
                store: Ext.data.StoreManager.lookup('promoStore')
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
                   fieldLabel: 'ID',
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
                   allowBlank: false,
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
                   msgTarget: 'side',
                   format: 'H:i:s'
               },
               {
                   xtype: 'textfield',
                   name: 'Tag',
                   fieldLabel: 'Tag',
                   hidden: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'Issuer',
                   fieldLabel: 'Issuer',
                   hidden: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'Days',
                   fieldLabel: 'Days',
                   hidden: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'Times',
                   fieldLabel: 'Times',
                   hidden: false,
                   allowBlank: false,
                   msgTarget: 'side'
               },
               {
                   xtype: 'textfield',
                   name: 'Terms',
                   fieldLabel: 'Terms',
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
                   name: 'PromoCategoryID',
                   itemId: 'itemId_PromoCategoryID',
                   fieldLabel: 'Promo Category',
                   labelAlign: 'left',
                   editable: false,
                   margin: '0 0 0 10',
                   store: Ext.create('MyApp.store.PromoCategoryStore',
                   {
                       storeId: 'PromoCategoryStoreID',
                       autoLoad: true,
                   }),
                   valueField: 'ID',
                   displayField: 'CategoryName',
                   allowBlank: false,
                   //reference: 'refLookupPromoCategory',
                   labelWidth: 75,
                   msgTarget: 'side'
               },
                {
                    xtype: 'combobox',
                    name: 'BrandID',
                    itemId: 'itemId_BrandID',
                    fieldLabel: 'Brand',
                    labelAlign: 'left',
                    editable: false,
                    margin: '0 0 0 10',
                    store: Ext.create('MyApp.store.BrandStore',
                    {
                        storeId: 'BrandStoreID',
                        autoLoad: true,
                    }),
                    valueField: 'ID',
                    displayField: 'BrandName',
                    allowBlank: false,
                    //reference: 'refLookupBrand',
                    labelWidth: 75,
                    msgTarget: 'side'
                },
               {
                   xtype: 'numberfield',
                   name: 'Priority',
                   fieldLabel: 'Priority',
                   hidden: false,
                   allowBlank: false,
                   value:0,
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
