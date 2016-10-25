Ext.define('MyApp.view.mainfulScraperSetting.MainfulScraperSetting', {
    extend: 'Ext.panel.Panel',
    xtype: 'MainfulScraperSetting',
    requires: [
        'MyApp.view.mainfulScraper.MainfulScraperController',
        'MyApp.store.UserProfileStore',
        'MyApp.view.brand.Brand'
    ],
    controller: 'mainfulScraper',
    border: false,
    items: [{
        region: 'center',
        xtype: 'form',
        reference: 'myForm',
        layout: 'form',
        border: false,
        //itemId: 'mainFormPanel',
        //title: 'Form',
        //collapsible: true,
        split: true,
        width: 500,
        bodyPadding: 10,
        scrollable: true,
        jsonSubmit: true,
        closeAction: 'hide',
        defaultType: 'textfield',
        animCollapse: false,
        //tbar: [
        //    { xtype: 'tbfill' },
        //    {
        //        xtype: 'button',
        //        itemId: 'BtnSave',
        //        text: 'Save',
        //        iconCls: 'icon-page-edit',
        //        handler: 'onBtnSaveClick'
        //    },
        //],
        items: [
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
               xtype: "textarea",
               name: "BrandName",
               fieldLabel: "Brand Name",
               hidden: false,
               allowBlank: false,
               msgTarget: "side"
           },
           {
               xtype: "textfield",
               name: "Description",
               fieldLabel: "Description",
               hidden: false,
               allowBlank: false,
               msgTarget: "side"
           },
           {
               xtype: "textfield",
               name: "Facebook",
               fieldLabel: "Facebook",
               hidden: false,
               msgTarget: "side"
           },
           {
               xtype: "textfield",
               name: "Twitter",
               fieldLabel: "Twitter",
               hidden: false,
               msgTarget: "side"
           },
           {
               xtype: "textfield",
               name: "Instagram",
               fieldLabel: "Instagram",
               hidden: false,
               msgTarget: "side"
           },
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
            }, {
                text: 'Remove',
                handler: 'onContextBtnRemoveClick',
            }]
        },

    ]
});