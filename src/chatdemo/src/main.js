import Vue from 'vue';
import App from './App.vue';
import router from './router';
import vuetify from '@/plugins/vuetify';
import axios from 'axios'
Vue.prototype.$axios = axios;
Vue.config.productionTip = false;
new Vue({
    router: router,
    vuetify: vuetify,
    render: function (h) { return h(App); }
}).$mount('#app');
//# sourceMappingURL=main.js.map