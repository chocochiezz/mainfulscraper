Ext.define('MyApp.store.BaseStore',{
    extend: 'Ext.data.Store',
    remoteFilter: true,
    remotesort: true,
    remoteGroup: true,
    pageSize: 30,
    filters: [],
    sorters: [],
    autoSync: true,
    autoLoad: false

});