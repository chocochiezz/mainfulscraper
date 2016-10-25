Ext.define('Ext.ux.form.field.DateDisplayField', {
	extend: 'Ext.form.field.Display',
	alias: 'widget.datedisplayfield',
	config: {
		format: 'Y-m-d'
	},
	valueToRaw: function (value) {
		return Ext.util.Format.date(value, this.getFormat());
	}
});