
Ext.define('MyApp.view.userProfile.UserProfile', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyUserProfile',
    requires: [
        'MyApp.view.userProfile.UserProfileController',
        'MyApp.store.UserProfileStore'
    ],
    controller: 'userProfile',
    border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('userProfileStore'),
        columns: MyApp.GlobalFunc.getGridColumns('userProfileStore', true),
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
					    hidden: true,
					    listeners: {
					        click: 'onExportClick'
					    }
					},
					{ xtype: 'tbfill' },
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
					    listeners: {
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
                store: Ext.data.StoreManager.lookup('userProfileStore')
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
            { xtype: 'tbfill' },
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
                    xtype: "numberfield",
                    name: "ID",
                    fieldLabel: "ID",
                    hidden: true,
                    readOnly: false,
                    value: 0,
                    msgTarget: "side"
                },
                {
                    xtype: "textfield",
                    name: "Name",
                    fieldLabel: "Name",
                    hidden: false,
                    allowBlank: false,
                    msgTarget: "side"
                },
                {
                    xtype: "textfield",
                    name: "PasswordHash",
                    fieldLabel: "Password Hash",
                    inputType: "password",
                    hidden: false,
                    allowBlank: false,
                    msgTarget: "side"
                },
                {
                    xtype: 'fieldcontainer',
                    fieldLabel: 'Gender',
                    defaultType: 'radiofield',
                    layout: 'hbox',
                    items: [
                        {
                            boxLabel: 'Male',
                            name: 'Gender',
                            inputValue: 'Male',
                            itemId: 'itemId_RadioGenderMale'
                        },
                        {
                            boxLabel: 'Female',
                            name: 'Gender',
                            inputValue: 'Female',
                            itemId: 'itemId_RadioGenderFemale',
                            margin: '0 0 0 10'
                        }
                    ]
                },
                {
                    xtype: "datefield",
                    name: "Birthdate",
                    fieldLabel: "Birthdate",
                    hidden: false,
                    allowBlank: false,
                    readOnly: false,
                    msgTarget: "side",
                    format: "d-M-Y"
                },
                {
                    xtype: "textfield",
                    name: "Phone",
                    fieldLabel: "Phone",
                    hidden: false,
                    msgTarget: "side"
                },
                {
                    xtype: "textfield",
                    name: "Email",
                    fieldLabel: "Email",
                    hidden: false,
                    vtype: 'email',
                    allowBlank: false,
                    msgTarget: "side"
                },
                {
                    xtype: "checkbox",
                    name: "EmailConfirmed",
                    fieldLabel: "Email Confirmed",
                    readOnly: false,
                    msgTarget: "side"
                },
                {
                    xtype: "textfield",
                    name: "ReminderSetting",
                    fieldLabel: "Reminder Setting",
                    hidden: false,
                    msgTarget: "side"
                },
                {
                    xtype: "checkbox",
                    name: "PushNotification",
                    fieldLabel: "Push Notification",
                    readOnly: false,
                    msgTarget: "side"
                },
                {
                    xtype: "textfield",
                    name: "AvatarUrl",
                    fieldLabel: "Avatar Url",
                    hidden: false,
                    msgTarget: "side"
                },
                {
                   xtype: 'fieldcontainer',
                   fieldLabel: 'Status',
                   defaultType: 'radiofield',
                   layout: 'hbox',
                   items: [
                       {
                           boxLabel: 'Active',
                           name: 'Status',
                           inputValue: 'active',
                           itemId: 'itemId_RadioActive'
                       },
                       {
                           boxLabel: 'Inactive',
                           name: 'Status',
                           inputValue: 'inactive',
                           itemId: 'itemId_RadioInactive',
                           margin: '0 0 0 10'
                       }
                   ]
                },
                {
                    xtype: "textfield",
                    name: "Passcode",
                    fieldLabel: "Passcode",
                    hidden: false,
                    msgTarget: "side"
                },
                {
                    xtype: "datefield",
                    name: "PasscodeExpired",
                    fieldLabel: "Passcode Expired",
                    hidden: false,
                    readOnly: false,
                    msgTarget: "side",
                    format: "d-M-Y"
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
            }, {
                text: 'Remove',
                handler: 'onContextBtnRemoveClick',
            }]
        },
    ],
});
