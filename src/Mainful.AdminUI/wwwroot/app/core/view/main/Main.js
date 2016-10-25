Ext.define('MyApp.view.main.Main', {
    extend: 'Ext.panel.Panel',
    itemId: 'MyMainApp',
    model: 'BaseModel',
    xtype: 'app-main',
    plugins: 'viewport',
    requires: [
		'MyApp.view.main.MainController',
    ],
    controller: 'main',
    layout: {
        type: 'border'
    },
    autoShow: true,
    flex: 1,
    items: [
        {
            xtype: 'panel',
            reference: 'ref_PanelHeader',
            itemId: 'itemId_PanelHeader',
            region: 'north',
            border: true,
            bodyPadding: 5,
            height: 60,
            layout: {
                type: 'anchor',
                width: '100%'
            },
            bodyStyle: {
                //background: '#ed7c13',
                //background: '#29C7FF',
                //background: '#000000',
                //backgroundImage: "url('http://mainful.com/wp-content/uploads/2016/06/img/Mainful_BGTile.png')"
            },
            //html: '<table border="0" cellpadding="0" width="100%"><tr>' + 
            //	  '<td width="50"><img src="http://mapi.ejjv.co/Content/Img/ic_launcher.png"/></td>'+
            //	  '<td><h2 style="color:#000066;">&nbsp;&nbsp;Mainful Administration</h2></td>'+
            //	  '<td align="right"><a href ="https://www.facebook.com/mainfulapp"><img class="" src="http://mainful.com/wp-content/uploads/2016/06/img/facebook.jpg" alt="facebook" width="120" height="45"></a>'+
            //		'<a href ="https://www.twitter.com/mainfulapp"><img class="" src="http://mainful.com/wp-content/uploads/2016/06/img/twitter.jpg" alt="twitter" width="120" height="45"></a>'+
            //		'<a href ="https://www.instagram.com/mainfulapp/"><img class="" src="http://mainful.com/wp-content/uploads/2016/06/img/instagram.jpg" alt="instagram" width="120" height="45"></a></td>'+
            //	  '</tr></table>'
            tbar: {

                items: [
                    {
                        xtype: 'tbfill',
                    },
                    {
                        xtype: 'button',
                        text: 'Logout',
                        listeners: {
                            click: function (e) {
                                Ext.Ajax.request({
                                    method: 'POST',
                                    url: MyApp.GlobalVar.BASE_API_URL + 'Login/Logout',
                                    success: function (response) {
                                        window.location.reload();
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
                        }
                    }
                ]
            }
        },
		{
		    xtype: 'panel',
		    reference: 'ref_PanelNavigation',
		    itemId: 'itemId_PanelNavigation',
		    title: 'Navigation',
		    region: 'west',
		    border: false,
		    split: true,
		    width: 250,
		    collapsible: true,
		    layout: 'fit',
		    items: [
				{
				    xtype: 'treepanel',
				    rootVisible: false,
				    //store: Ext.create('MyApp.store.NavigationStore', { storeId: 'navigationStore' }),
				    width: 270,
				    autoScroll: true,
				    border: false,
				    listeners: {
				        cellclick: function (thisObj, td, cellIndex, record, tr, rowIndex, e, eOpts) {
				            MyApp.GlobalFunc.openTab(record);
				        },
				        afterrender: function () {
				        }
				    }, store: {
				        root: {
				            children: [
                                {
                                    text: 'User Administrator',
                                    leaf: true,
                                    handler: 'MyApp.view.userAdministrator.UserAdministrator'
                                },
                                {
                                    text: 'Group Administrator',
                                    leaf: true,
                                    handler: 'MyApp.view.groupAdministrator.GroupAdministrator'
                                },
                                {
                                    text: 'User Management',
                                    leaf: true,
                                    handler: 'MyApp.view.userProfile.UserProfile'
                                },
                                {
                                    text: 'User Content Blocked',
                                    leaf: true,
                                    handler: 'MyApp.view.userContentBlocked.UserContentBlocked'
                                },
                                {
                                    text: 'Places Management',
                                    leaf: true,
                                    handler: 'MyApp.view.placeLocation.PlaceLocation'
                                },
                                {
                                    text: 'Brand Management',
                                    leaf: true,
                                    handler: 'MyApp.view.brand.Brand'
                                },
                                {
                                    text: 'Event Organizer Management',
                                    leaf: true,
                                    handler: 'MyApp.view.eventOrganizer.EventOrganizer'
                                },
                                {
                                    text: 'Events &amp; Seminar Management',
                                    leaf: true,
                                    handler: 'MyApp.view.event.Event'
                                },
                                {
                                    text: 'Promo Management',
                                    leaf: true,
                                    handler: 'MyApp.view.promo.Promo'
                                },
                                {
                                    text: 'Ads Management',
                                    leaf: true,
                                    handler: 'MyApp.view.ad.Ad'
                                },
                                {
                                    text: 'Feeds Management',
                                    leaf: true,
                                    handler: 'MyApp.view.feeds.Feeds'
                                },
                                {
                                    text: 'Parking Management',
                                    leaf: true,
                                    handler: 'MyApp.view.userAdminParking.UserAdminParking'
                                },
				                {
				                    text: 'Parking Space Pricing',
				                    leaf: true,
				                    handler: 'MyApp.view.parkingSpacePrice.ParkingSpacePrice'
				                },
                                {
                                    text: 'Mainful Scraper',
                                    //leaf: true,
                                    //handler: 'MyApp.view.mainfulScraper.MainfulScraper',
                                    children: [{
                                        text: 'Home',
                                        leaf: true,
                                        handler: 'MyApp.view.mainfulScraper.MainfulScraper',
                                    },{
                                        text: 'Result',
                                        leaf: true,
                                        handler: 'MyApp.view.mainfulScraperResult.MainfulScraperResult'
                                    },
                                    {
                                        text: 'Trend',
                                        leaf: true
                                    },
                                    {
                                        text: 'Settings',
                                        leaf: true,
                                        handler:'MyApp.view.mainfulScraperSetting.MainfulScraperSetting'
                                    }]
                                }
				            ]
				        }
				    },
				}
		    ],
		},
		{
		    id: 'mainTabPanel',
		    //reference: 'myTabPanel',
		    //plugins: ['tabreorderer', 'tabclosemenu'],
		    region: 'center',
		    closeAction: 'hide',
		    xtype: 'tabpanel', // TabPanel itself has no title
		    bodyStyle: {
		        //background: '#ed7c13',
		        //background: '#29C7FF',
		        background: '#F8F8FF',
		        //backgroundImage: "url('http://mainful.com/wp-content/uploads/2016/06/img/Mainful_Point.png')",
		        backgroundRepeat: 'no-repeat',
		        backgroundPosition: 'center center',
		        backgroundSize: '30%'
		    },
		    //activeTab: 0,      // First tab active by default
		    //items: {
		    //    title: 'Default Tab',
		    //    html: 'The first tab\'s content. Others may be added dynamically',
		    //}
		    //items: [
		    //    {
		    //        title: 'Simpsons',
		    //        xtype: 'gridpanel',
		    //        columns: [
		    //            { text: 'Name', dataIndex: 'name' },
		    //            { text: 'Email', dataIndex: 'email', flex: 1 },
		    //            { text: 'Phone', dataIndex: 'phone' }
		    //        ],
		    //        height: 200,
		    //        width: 400,
		    //    }
		    //]
		},
		{
		    xtype: 'panel',
		    reference: 'ref_PanelFooter',
		    itemId: 'itemId_PanelFooter',
		    region: 'south',
		    border: true,
		    bodyPadding: 5,
		    bodyStyle: {
		        //background: '#999999',
		        color: '#000066',
		        backgroundImage: "url('http://mainful.com/wp-content/uploads/2016/06/img/Mainful_BGTile.png')"
		    },
		    items: {
		        xtype: 'label',
		        html: '&copy; 2016 PT. Mitra Konsultansi Indonesia. Build Number.1606180835'
		    }
		},
    ]
});