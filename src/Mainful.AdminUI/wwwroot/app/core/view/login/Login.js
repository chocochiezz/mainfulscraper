Ext.define('MyApp.view.login.Login', {
    extend: 'Ext.window.Window',
    xtype: 'login',
    requires: [
        'MyApp.view.login.LoginController',
        'Ext.form.Panel'
    ],    
    draggable: false,
    resizable:false,
    controller: 'login',
    bodyPadding: 10,
    title: 'Mainful Administrator',
    closable: false,
    autoShow: true,
    items: {
        xtype: 'form',
        bodyPadding:20,
        reference: 'myForm',        
        items: [{
            xtype: 'textfield',
            name: 'UserName',
            fieldLabel: 'Username',
            allowBlank: false,
            enableKeyEvents: true,
            listeners: {
                keypress: 'onKeypress'
            }
        }, {
            xtype: 'textfield',
            name: 'Password',
            inputType: 'password',
            fieldLabel: 'Password',            
            allowBlank: false,
            enableKeyEvents: true,
            listeners: {
                keypress: 'onKeypress'
            }
        }, {
            xtype: 'button',
            itemId: 'BtnLogin',
            margin:'20 0 20 105',
            text: 'Login',
            formBind: true,
            listeners: {
                click: 'onLoginClick'
            }
        }]
    }
});