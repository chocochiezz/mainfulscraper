Ext.define('MyApp.view.login.LoginController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.login',
    onLoginClick: function () {
        var formPanel = this.lookupReference('myForm');
        var window = this.getView();
        
        var form = formPanel.getForm();
        if (form.isValid()) {            
            formPanel.mask('Login...');
            form.submit({
                url: MyApp.GlobalVar.BASE_API_URL + 'Login/Login',
                success: function (conn, response) {                                      
                    window.destroy();
                    Ext.widget('app-main');
                },
                failure: function (conn, response) {
                    var jsonObj = Ext.decode(response.response.responseText);                    
                    formPanel.unmask();
                    MyApp.GlobalFunc.showWarning("Login failed because " + jsonObj.msg);
                    form.markInvalid(jsonObj.Data);
                }
            });
        }
        else {
            Ext.Msg.alert("Warning", "Make sure all mandatory field is filled.");
        }
    },
    onKeypress: function (field, event) {

        if (event.charCode == 13)
        {
            var formPanel = this.lookupReference('myForm');
            var resultBtnLogin = formPanel.query('#BtnLogin');
            
            if (resultBtnLogin.length != 0) {                
                resultBtnLogin[0].fireEvent('click');
            }
        }
    }
});