import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router';
import Routers from './router.js';
import ViewUI from 'view-design';
import VueI18n from 'vue-i18n';
import en from 'view-design/dist/locale/en-US';
import zh from 'view-design/dist/locale/zh-CN';
import l_zh from './locales/zh-CN'
import l_en from './locales/en'
import 'view-design/dist/styles/iview.css';
import service from './components/http';
import store from './store'
import './assets/font/iconfont.css'

import BaiduMap from "wkm-vue-baidu-map";
Vue.use(BaiduMap, {
    ak: "pVBne1bmUyut9A2yU9z8igM1G9aXnyFS",
});
// 事件总线
Vue.prototype.$EventBus = new Vue()

import global from './components/global'

Vue.prototype.global = global
Vue.prototype.http = service

Vue.use(ViewUI);

Vue.use(VueRouter);

Vue.use(VueI18n);

Vue.locale = () => { };

const messages = {
    en: Object.assign(l_en, en),
    zh: Object.assign(l_zh, zh)
};

// Create VueI18n instance with options
const i18n = new VueI18n({
    locale: 'en', // set locale
    messages // set locale messages
});
// The routing configuration
const RouterConfig = {
    routes: Routers
};
const router = new VueRouter(RouterConfig);

new Vue({
    store,
    router: router,
    i18n: i18n,
    render: h => h(App),
}).$mount('#app')
