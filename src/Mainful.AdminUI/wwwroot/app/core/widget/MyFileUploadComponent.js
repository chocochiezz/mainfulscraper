/// http://ext4all.com/post/extjs-4-2-2-html-5-multi-file-upload.html

//Ext.define('Ext.ux.form.MultiFile', {
Ext.define('My.FileUpload.Component', {
	extend: 'Ext.form.field.File',
	//alias: 'widget.multifilefield',
	xtype: 'MyFileUploadComponent',
	alias: 'widget.MyFileUploadComponent',

	initComponent: function () {
		var me = this;

		me.on('render', function () {
			me.fileInputEl.set({ multiple: true });
		});

		me.callParent(arguments);
	},

	onFileChange: function (button, e, value) {
		this.duringFileSelect = true;

		var me = this,
            upload = me.fileInputEl.dom,
            files = upload.files,
            names = [];

		if (files) {
			for (var i = 0; i < files.length; i++)
				names.push(files[i].name);
			value = names.join(', ');
		}

		Ext.form.field.File.superclass.setValue.call(this, value);

		delete this.duringFileSelect;
	}
});