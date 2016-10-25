Ext.define('MyApp.view.mainfulScraperSetting.MainfulScraperSettingController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.mainfulScraperSetting',
    //requires: [
    //    'MyApp.model.EventOrganizerModel',
    //],

    init: function () {

        var formPanel = this.lookupReference('myForm');
        if (formPanel) {
            //formPanel.removeAll();
            //formPanel.add(MyApp.GlobalFunc.generateFormFields('brandStore'));
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Brand/Create';

            //var grid = this.lookupReference('myGrid');
            //grid.getStore().load();
        }
    },
    onSelectionChange: function (selectionModel, selected, eOpts) {

        var grid = this.lookupReference('myGrid');

        var resultBtnEdit = grid.query('#BtnChoose');
        if (resultBtnEdit.length != 0) {
            resultBtnEdit[0].setDisabled(!selected.length);
        }

        var resultBtnRemove = grid.query('#BtnRemove');
        if (resultBtnRemove.length != 0) {
            resultBtnRemove[0].setDisabled(!selected.length);
        }

        if (selected.length == 0)
            return;

        var formPanel = this.lookupReference('myForm');
        if (formPanel !== null) {
            formPanel.loadRecord(selected[0]);
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Brand/Update';
            formPanel.expand(false); // Auto show form
        }
    },

    onNewClick: function () {
        var NewForm = Ext.create('Ext.window.Window',
            {
                width: 400,
                height: 230,
                title: 'Add new source',
                draggable: false,
                resizeable: false,
                modal: true,
                resizable: false,
                buttonAlign: 'center',
                items: [
                    {
                        xtype: 'form',
                        reference: 'newForm',
                        id: 'newForm',
                        layout: 'form',
                        border: false,
                        split: true,
                        bodyPadding: 10,
                        //scrollable: true,
                        jsonSubmit: true,
                        closeAction: 'hide',
                        defaultType: 'textfield',
                        animCollapse: false,
                        items: [
                            {
                                xtype: 'combobox',
                                name: 'SocialMediaSelector',
                                itemId: 'itemId_SocialMediaSelectorID',
                                fieldLabel: 'Social Media',
                                //labelAlign: 'left',
                                editable: false,
                                //margin: '0 0 0 10',
                                store: new Ext.data.SimpleStore({
                                    fields: ['value', 'text'],
                                    data: [
                                        ['FB', 'Facebook'],
                                        ['Twitter', 'Twitter'],
                                        ['IG', 'Instagram'],
                                    ]
                                }),
                                valueField: 'value',
                                displayField: 'text',
                                allowBlank: false,
                                //reference: 'refLookupPromoCategory',
                                labelWidth: 200,
                                //msgTarget: 'side',
                                listeners: {
                                    //select: 'onSelect'
                                }
                            },
                            {
                                xtype: "textfield",
                                name: "Username",
                                fieldLabel: "Username",
                                emptyText: 'You can copy paste the link here',
                                readOnly: false,
                                allowBlank: false,
                                //msgTarget: "side"
                            },
                            {
                                xtype: 'sliderfield',
                                fieldLabel: 'Quantity',
                                name: 'Qty',
                                value: 25,

                                //xtype: 'fieldcontainer',
                                //name: 'QtySelector',
                                //itemId: 'itemId_QtySelectorID',
                                //label: 'Qty'
                                //items:[
                                //    {
                                //        boxLabel: '10',
                                //        name: 'qty',
                                //        inputValue : '10',
                                //        id: 'radio1',
                                //        margin : '0 5 0 0'
                                //    },
                                //    {
                                //        boxLabel: '25',
                                //        name: 'qty',
                                //        inputValue: '25',
                                //        id: 'radio2',
                                //        margin: '0 5 0 5'
                                //    },
                                //    {
                                //        boxLabel: '50',
                                //        name: 'qty',
                                //        inputValue: '50',
                                //        id: 'radio3',
                                //        margin: '0 5 0 5'
                                //    },
                                //    {
                                //        boxLabel: '100',
                                //        name: 'qty',
                                //        inputValue: '100',
                                //        id: 'radio4',
                                //        margin: '0 5 0 5'
                                //    },
                                //],
                                //bbar: [
                                //    {
                                //        handler: function() {
                                //            var radio1 = Ext.getCmp('radio1'),
                                //                radio2 = Ext.getCmp('radio2'),
                                //                radio3 = Ext.getCmp('radio3'),
                                //                radio4 = Ext.getCmp('radio4');
                                //        }
                                //    }
                                //]
                            },
                        ]
                    }
                ],
                buttons: [
                    {
                        text: 'Reset',
                        handler: function () {
                            Ext.getCmp('newForm').reset();
                        }
                    },
                    {
                        text: 'Next',
                        handler: function () {
                            //var form = this.getView().getForm();
                            var form = Ext.getCmp('newForm').getForm();

                            if (form.isValid()) {

                                Ext.Msg.confirm('Confirmation', 'Are you sure want to continue?', function (result) {
                                    var data = Ext.getCmp('newForm').getValues();
                                    console.log(data);
                                });

                                form.reset();
                                Ext.Msg.show({
                                    title: 'Info',
                                    message: 'Data has been saved.',
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO,
                                    //fn: function (btn) {
                                    //    if (btn === 'ok') {
                                    //        grid.getSelectionModel().select(index);
                                    //    }
                                    //}
                                });
                            }
                            else {
                                Ext.Msg.alert("Warning", "Make sure all mandatory field is filled.");
                            }
                        }
                    }
                ]
            });
        NewForm.show();
    },


    onSelect: function (combo, text, value) {
        //alert("Are sure you want to select " + combo.getValue() +" as your choice?");

        //Generate brand view
        if (combo.getValue() == 'EO') {
            var formPanel = this.lookupReference('myForm');
            var win = this.lookupReference('myGrid');
            var pagingbar = this.lookupReference('mybbar');
            formPanel.reset();
            formPanel.collapse(true);
            win.setStore(MyApp.GlobalFunc.storeFactory('eventOrganizerStore'));
            win.setColumns(MyApp.GlobalFunc.getGridColumns('eventOrganizerStore', true));
            win.getStore().load();
            pagingbar.setStore(Ext.data.StoreManager.lookup('eventOrganizerStore'));
        }
            //Generate places view
        else if (combo.getValue() == 'Brand') {
            var formPanel = this.lookupReference('myForm');
            var win = this.lookupReference('myGrid');
            var pagingbar = this.lookupReference('mybbar');
            formPanel.reset();
            formPanel.collapse(true);
            win.setStore(MyApp.GlobalFunc.storeFactory('brandStore'));
            win.setColumns(MyApp.GlobalFunc.getGridColumns('brandStore', true));
            win.getStore().load();
            pagingbar.setStore(Ext.data.StoreManager.lookup('brandStore'));
            //console.log(win);
        }
            //Generate Places view
        else {
            var formPanel = this.lookupReference('myForm');
            var win = this.lookupReference('myGrid');
            var pagingbar = this.lookupReference('mybbar');
            formPanel.reset();
            formPanel.collapse(true);
            win.setStore(MyApp.GlobalFunc.storeFactory('placeLocationStore'));
            win.setColumns(MyApp.GlobalFunc.getGridColumns('placeLocationStore', true));
            win.getStore().load();
            pagingbar.setStore(Ext.data.StoreManager.lookup('placeLocationStore'));
        }

    },

    //createEOStore: function () {
    //    return new Ext.data.Store({
    //        //model : 'MyApp.model.EventOrganizerModel',
    //        //data: 'MyApp.model.EventOrganizerModel',
    //        store: MyApp.GlobalFunc.storeFactory('eventOrganizerStore'),
    //        columns: MyApp.GlobalFunc.getGridColumns('eventOrganizerStore', true),
    //        selType: 'rowmodel',
    //        plugins: 'gridfilters',
    //        stateful: true,
    //        border: false,
    //        columnLines: true
    //    });
    //},

    onEditClick: function () {
        var formPanel = this.lookupReference('myForm');
        if (formPanel) {
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Brand/Update';
            formPanel.expand(false);
        }
    },

    onRemoveClick: function () {

        var formPanel = this.lookupReference('myForm');
        var grid = this.lookupReference('myGrid');

        Ext.Msg.confirm('Confirmation', 'Are you sure want to delete the data?', function (result) {

            if (result === 'no')
                return;

            var store = grid.getStore();
            var sm = grid.getSelectionModel();
            //var rowEditing = grid.getPlugin('rowEditing');

            //rowEditing.cancelEdit();
            store.remove(sm.getSelection());
            //store.erase();

            if (store.getCount() > 0) {
                sm.select(0);
            }
            else {
                formPanel.reset();
            }

            Ext.Msg.show({
                title: 'Info',
                message: 'Data has been deleted.',
                buttons: Ext.Msg.YES,
                icon: Ext.Msg.INFO
            });

        });
    },

    onBtnSaveClick: function () {
        var formPanel = this.lookupReference('myForm');
        var grid = this.lookupReference('myGrid');
        var form = formPanel.getForm();

        if (form.isValid()) {

            var activeUrl = formPanel.url; // the url is set on the fly, so need to bind manually to form object

            var index = 0;
            if (grid.getSelectionModel().getSelection().length != 0) {
                var selectedRecord = grid.getSelectionModel().getSelection()[0];
                index = grid.getStore().indexOf(selectedRecord);
            }

            formPanel.mask('Saving...');

            form.submit({
                url: activeUrl,
                success: function (conn, response) {
                    grid.getStore().load();
                    form.reset();
                    grid.getSelectionModel().deselectAll(true);
                    formPanel.unmask();
                    Ext.Msg.show({
                        title: 'Info',
                        message: 'Data has been saved.',
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.INFO,
                        fn: function (btn) {
                            if (btn === 'ok') {
                                grid.getSelectionModel().select(index);
                            }
                        }
                    });
                },
                failure: function (conn, response) {
                    var jsonObj = Ext.decode(response.response.responseText);
                    formPanel.unmask();
                    MyApp.GlobalFunc.showWarning("Save failed because " + jsonObj.msg);
                    form.markInvalid(jsonObj.Data);
                }
            });
        }
        else {
            Ext.Msg.alert("Warning", "Make sure all mandatory field is filled.");
        }
    },

    onRowContextMenu: function (thisObj, record, item, index, e, eOpts) {
        e.stopEvent();
        var gridMenu = this.lookupReference('myGridPanelMenu');
        gridMenu.showAt(e.getXY());
    },

    onContextBtnEditClick: function () {
        this.onEditClick();
    },

    onContextBtnRemoveClick: function () {
        this.onRemoveClick();
    },

    onExportClick: function () {
        var grid = this.lookupReference('myGrid');

        if (grid.getStore().getData().length <= 0) {
            Ext.Msg.show({
                title: 'Warning',
                message: 'No data available!',
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.WARNING
            });
            return;
        }

        var dbParamEntity = {
            Filter: [],
            Sort: []
        };

        var gridFilter = grid.getStore().getFilters();
        gridFilter.items.forEach(function (item) {
            var filter = {
                Property: item.getProperty(),
                Operator: item.getOperator(),
                Value: item.getValue()
            };
            dbParamEntity.Filter.push(filter);
        });

        var gridSort = grid.getStore().getSorters();
        gridSort.items.forEach(function (item) {
            var sort = {
                Property: item.getProperty(),
                Direction: item.getDirection()
            };
            dbParamEntity.Sort.push(sort);
        });

        Ext.Ajax.request({
            method: 'POST',
            url: MyApp.GlobalVar.BASE_API_URL + 'Brand/ExportToExcel',
            jsonData: dbParamEntity,
            success: function (response) {
                var jsonObj = Ext.decode(response.responseText);
                var data = jsonObj.Data.Data;

                if (data == null) {
                    Ext.Msg.show({
                        title: 'Warning',
                        message: jsonObj.Data.Message,
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.WARNING
                    });
                    return;
                }

                Ext.Msg.show({
                    title: 'Download Link',
                    message: '<center>Your file is ready...<br/><a href="' + MyApp.GlobalVar.BASE_API_URL + 'FileService/DownloadDoc/' + data.ID + '" target="_blank">Click here to download the file</a></center>',
                    buttons: Ext.Msg.OK,
                    buttonText: { ok: 'Close' }
                });
            },
            failure: function (response) {
                Ext.Msg.show({
                    title: 'Error',
                    message: response.statusText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        });
    }
});