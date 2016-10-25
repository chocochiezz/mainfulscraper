Ext.define('MyApp.view.mainfulScraper.MainfulScraper', {
    extend: 'Ext.panel.Panel',
    xtype: 'MainfulScraper',
    requires: [
        'MyApp.view.mainfulScraper.MainfulScraperController',
        'MyApp.store.UserProfileStore',
        'MyApp.view.brand.Brand'
    ],
    controller: 'mainfulScraper',
    border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        //store: MyApp.GlobalFunc.storeFactory('brandStore'),
        //columns: MyApp.GlobalFunc.getGridColumns('brandStore', true),
        //columns: [],
        viewConfig: {
            emptyText: 'Select your choose from combo box above!',
            deferEmptyText: false
        },
        selType: 'checkboxmodel',
        plugins: 'gridfilters',
        stateful: true,
        border: false,
        columnLines: true,

    
        tbar: [
			{
			    xtype: 'toolbar',
			    bodyPadding: 5,
			    defaultType: 'button',
			    flex: 1,
			    border: false,
			    items: [
                    //    listConfig: {
                    //        loadingText: 'Searching...',
                    //        emptyText: 'No matching posts found.',

                    //        // Custom rendering template for each item
                    //        getInnerTpl: function() {
                    //            return 'true' 
                    //            '</a>';
                    //        }
                    //    },
                    //    pageSize: 10
                    //}, {
                    //    xtype: 'component',
                    //    style: 'margin-top:10px',
                    //    html: 'Live search requires a minimum of 4 characters.'

                    //},
                    //{
                    //    xtype: 'label',
                    //    text: 'Search',
                    //    style: 'padding: 5px; text-align: left',
                    //    width: 50
                    //},
                    //{
                    //    xtype: 'trigger',
                    //    triggerCls: Ext.baseCSSPrefix + 'form-search-trigger',
                    //    flex: 1,
                    //    name: 'BrandName',
                    //    allowBlank: false,
                    //    msgTarget: 'side',
                    //    style: 'margin-left: 5px'
                    //},
                    {
                        xtype: 'combobox',
                        name: 'ContentSelector',
                        itemId: 'itemId_ContentSelectorID',
                        fieldLabel: 'Select a content to scraped',
                        //labelAlign: 'left',
                        editable: false,
                        //margin: '0 0 0 10',
                        store: new Ext.data.SimpleStore({
                            fields: ['value', 'text'],
                            data: [
                                ['EO', 'Event Organizer'],
                                ['Brand', 'Brand'],
                                ['Places', 'Places'],
                            ]
                        }),
                        valueField: 'value',
                        displayField: 'text',
                        allowBlank: false,
                        //reference: 'refLookupPromoCategory',
                        labelWidth: 200,
                        msgTarget: 'side',
                        listeners: {
                            select: 'onSelect'
                        }
                    },
					{ xtype: 'tbfill' },
					{
					    xtype: 'button',
					    reference: 'ref_BtnChoose',
					    itemId: 'BtnChoose',
					    text: 'Choose',
					    iconCls: 'icon-page-edit',
					    disabled: true,
					    listeners: {
					        click: 'onChooseClick'
					    }
					},
                    {
                        xtype: 'button',
                        reference: 'ref_BtnNew',
                        itemId: 'BtnNew',
                        text: 'New',
                        iconCls: 'icon-page-add',
                        listeners: {
                            click: 'onNewClick'
                        }
                    }
			    ]
			}
        ],

        bbar: [
            {
                xtype: 'pagingtoolbar',
                reference: 'mybbar',
                displayInfo: true,
                flex: 1,
                border: false,
                //store: Ext.data.StoreManager.lookup('brandStore')
            },
        ],

        listeners: {
            selectionchange: 'onSelectionChange',
            itemcontextmenu: 'onRowContextMenu',
        }
    },

    //}, {
    //    xtype: 'form',
    //    reference: 'myForm',
    //    layout: 'form',
    //    border: false,
    //    //itemId: 'mainFormPanel',
    //    region: 'east',
    //    title: 'Form',
    //    collapsible: true,
    //    split: true,
    //    width: 500,
    //    collapsed: true,
    //    bodyPadding: 10,
    //    scrollable: true,
    //    jsonSubmit: true,
    //    closeAction: 'hide',
    //    defaultType: 'textfield',
    //    animCollapse: false,
    //    tbar: [
    //        { xtype: 'tbfill' },
    //        {
    //            xtype: 'button',
    //            itemId: 'BtnSave',
    //            text: 'Save',
    //            iconCls: 'icon-page-edit',
    //            handler: 'onBtnSaveClick'
    //        },
    //    ],
    //    items: [
    //       {
    //           xtype: "numberfield",
    //           name: "ID",
    //           fieldLabel: "I D",
    //           hidden: true,
    //           readOnly: false,
    //           value: 0,
    //           msgTarget: "side"
    //       },
    //       {
    //           xtype: "textfield",
    //           name: "BrandName",
    //           fieldLabel: "Brand Name",
    //           hidden: false,
    //           allowBlank: false,
    //           msgTarget: "side"
    //       },
    //       {
    //           xtype: "textfield",
    //           name: "Description",
    //           fieldLabel: "Description",
    //           hidden: false,
    //           allowBlank: false,
    //           msgTarget: "side"
    //       },
    //       {
    //           xtype: "textfield",
    //           name: "Facebook",
    //           fieldLabel: "Facebook",
    //           hidden: false,
    //           msgTarget: "side"
    //       },
    //       {
    //           xtype: "textfield",
    //           name: "Twitter",
    //           fieldLabel: "Twitter",
    //           hidden: false,
    //           msgTarget: "side"
    //       },
    //       {
    //           xtype: "textfield",
    //           name: "Instagram",
    //           fieldLabel: "Instagram",
    //           hidden: false,
    //           msgTarget: "side"
    //       },
    //    ]
    //},
        {
            xtype: 'menu',
            reference: 'myGridPanelMenu',
            closeAction: 'hide',
            margin: '0 0 10 0',
            renderTo: Ext.getBody(),
            items: [{
                text: 'Edit',
                handler: 'onContextBtnEditClick',
            }, {
                text: 'Remove',
                handler: 'onContextBtnRemoveClick',
            }]
        },

    ]
});