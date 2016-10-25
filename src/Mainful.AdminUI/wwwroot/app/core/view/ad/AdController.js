
Ext.define('MyApp.view.ad.AdController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ad',

    init: function () {
                
        var formPanel = this.lookupReference('myForm');
        if (formPanel) {
            //formPanel.removeAll();
            //formPanel.add(MyApp.GlobalFunc.generateFormFields('adStore'));
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Ad/Create';

            //var grid = this.lookupReference('myGrid');
            //grid.getStore().load();
        }
    },

    onSelectionChange: function (selectionModel, selected, eOpts) {

        var grid = this.lookupReference('myGrid');

        var resultBtnEdit = grid.query('#BtnEdit');
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
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Ad/Update';
            formPanel.expand(false); // Auto show form
        }
    },

    onAddClick: function () {
        var formPanel = this.lookupReference('myForm');
        var grid = this.lookupReference('myGrid');
        if (formPanel) {
            grid.getSelectionModel().deselectAll();
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Ad/Create';
            formPanel.reset();
            formPanel.expand(false);
        }
    },

    onEditClick: function () {
        var formPanel = this.lookupReference('myForm');
        if (formPanel) {
            formPanel.url = MyApp.GlobalVar.BASE_API_URL + 'Ad/Update';
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
		else
		{
			Ext.Msg.alert("Warning", "Make sure all mandatory field is filled.");
		}
    },

    onRowContextMenu: function(thisObj, record, item, index, e, eOpts) {
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
			url: MyApp.GlobalVar.BASE_API_URL + 'Ad/ExportToExcel',
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
	},
	onContentSourceChange: function () {
	    var formPanel = this.lookupReference('myForm');
	    var contentSourceObj = formPanel.getForm().findField('ContentSource');
	    
	    var contentSource = contentSourceObj.value;
	    //if (contentSource != null)
	    //{
	    //    var contentIDObj = formPanel.getForm().findField('ContentID');
	    //    var storeobj = Ext.data.StoreManager.lookup('ContentIDStore');

	    //    Ext.data.StoreManager.lookup('ContentIDStore').load({
	    //        params: {
	    //            contentSource: contentSource
	    //        },
	    //        callback: function (records, operation, success) {
	    //            // do something after the load finishes
	    //        },
	    //        scope: this
	    //    });
	    //}
	},
});
