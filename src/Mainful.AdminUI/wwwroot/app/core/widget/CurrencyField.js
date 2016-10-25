// Currency Component
Ext.define('CurrencyField', {
	extend: 'Ext.form.field.Number',
	alias: ['widget.currencyfield'],
	currency: '', //change to the symbol you would like to display.
	listeners: {
		render: function (cmp) {
			cmp.showCurrency(cmp);
		},
		blur: function (cmp) {
			cmp.showCurrency(cmp);
		},
		focus: function (cmp) {
			cmp.setRawValue(cmp.valueToRaw(cmp.getValue()));
		}
	},
	showCurrency: function (cmp) {
		//cmp.setRawValue(Ext.util.Format.currency(cmp.valueToRaw(cmp.getValue()), cmp.currency, 2, false));
		cmp.setRawValue(Ext.util.Format.number(cmp.valueToRaw(cmp.getValue()), '0,0.00'));
	},
	valueToRaw: function (value) {
		//console.log(value.toString().replace(/[^0-9.]/g, ''));
		if (value == undefined) { return 0; }
		return value.toString().replace(/[^0-9.]/g, '');
	},
	rawToValue: function (value) {
		//console.log(Ext.util.Format.round(this.valueToRaw(value), 2));
		return Ext.util.Format.round(this.valueToRaw(value), 2);
	},
	getSubmitValue: function () {
		var me = this,
            value = me.callParent();

		if (!me.submitLocaleSeparator) {
			value = value.replace(me.decimalSeparator, '.');
		}

		return me.rawToValue(value);
	},
	getErrors: function (value) {
		value = arguments.length > 0 ? this.rawToValue(value) : this.processRawValue(this.getRawValue());
		//value = arguments.length > 0 ? value : this.rawToValue(value);

		var me = this,
            errors = me.callParent([value]),
            format = Ext.String.format,
            num;

		if (value.length < 1) { // if it's blank and textfield didn't flag it then it's valid
			return errors;
		}

		value = String(value).replace(me.decimalSeparator, '.');

		if (isNaN(value)) {
			errors.push(format(me.nanText, value));
		}

		num = me.parseValue(value);

		if (me.minValue === 0 && num < 0) {
			errors.push(this.negativeText);
		}
		else if (num < me.minValue) {
			errors.push(format(me.minText, me.minValue));
		}

		if (num > me.maxValue) {
			errors.push(format(me.maxText, me.maxValue));
		}

		return errors;
	},
});
// END Currency Component