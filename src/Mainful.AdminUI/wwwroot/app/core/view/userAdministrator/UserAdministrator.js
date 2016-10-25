
Ext.define('MyApp.view.userAdministrator.UserAdministrator', {
    extend: 'Ext.panel.Panel',
    xtype: 'MyUserAdministrator',
    requires: [
        'MyApp.view.userAdministrator.UserAdministratorController',
        'MyApp.store.UserAdministratorStore'
    ],
    controller: 'userAdministrator',
    border: false,
    items: [{
        region: 'center',
        xtype: 'gridpanel',
        reference: 'myGrid',
        store: MyApp.GlobalFunc.storeFactory('userAdministratorStore'),
        columns: MyApp.GlobalFunc.getGridColumns('userAdministratorStore', true),
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
                store: Ext.data.StoreManager.lookup('userAdministratorStore')
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
                 xtype: 'numberfield',
                 name: 'ID',
                 fieldLabel: 'ID',
                 hidden: true,
                 msgTarget: 'side',
                 value:0
            },
            {
                xtype: 'textfield',
                name: 'UserName',
                fieldLabel: 'User Name',
                hidden: false,
                msgTarget: 'side',
                allowBlank: false
            },
            {
                xtype: 'textfield',
                name: 'Password',
                inputType: 'password',
                fieldLabel: 'Password',
                hidden: false,
                msgTarget: 'side',
                allowBlank: false
            },
            {
                xtype: 'textfield',
                name: 'Name',
                fieldLabel: 'Name',
                hidden: false,
                msgTarget: 'side',
                allowBlank: false
            },
            {
                xtype: 'textfield',
                name: 'Email',
                fieldLabel: 'Email',
                hidden: false,
                msgTarget: 'side',
                vtype: 'email',
                allowBlank: false
            },
            {
                xtype: 'numberfield',
                name: 'Phone',
                fieldLabel: 'Phone',
                hidden: false,
                msgTarget: 'side'
            },
            {
                xtype: 'textfield',
                name: 'Picture',
                fieldLabel: 'Picture',
                hidden: true,
                msgTarget: 'side'
            },
            {
                xtype: 'textfield',
                name: 'IsLocked',
                fieldLabel: 'IsLocked',
                hidden: true,
                msgTarget: 'side'
            },
            {
                xtype: 'textfield',
                name: 'IsDeleted',
                fieldLabel: 'IsDeleted',
                hidden: true,
                msgTarget: 'side'
            },
            {
                xtype: 'combobox',
                name: 'GroupID',
                itemId: 'itemId_GroupID',
                fieldLabel: 'Group',
                labelAlign: 'left',
                editable: false,
                margin: '0 0 0 10',
                store: Ext.create('MyApp.store.GroupAdministratorStore',
				{
				    storeId: 'GroupAdministratorStoreID',
				    autoLoad: true,
				}),
                valueField: 'ID',
                displayField: 'GroupName',
                allowBlank: true,
                reference: 'refLookupState',
                labelWidth: 75,
                msgTarget: 'side'
            },
            {
                xtype: 'datefield',
                name: 'CreatedDate',
                fieldLabel: 'CreatedDate',
                hidden: true,
                msgTarget: 'side'
            },
            {
                xtype: 'datefield',
                name: 'ModifiedDate',
                fieldLabel: 'ModifiedDate',
                hidden: true,
                msgTarget: 'side'
            },
            {
                xtype: 'datefield',
                name: 'PasscodeExpired',
                fieldLabel: 'PasscodeExpired',
                hidden: true,
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
            }, {
                text: 'Remove',
                handler: 'onContextBtnRemoveClick',
            }]
        },
    ],
});
