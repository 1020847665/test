// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import VueRouter from 'vue-router';
import VueResource from 'vue-resource';
import './js/weui/dist/style/weui.min.css';
import './js/weui/dist/weui.js';

import './css/index.css';
import App from './App';

// 引入组件

//使用
Vue.use(VueRouter);
Vue.use(VueResource);


// =========================路由配置==================
var router = new VueRouter({
    // 当hashbang的值为true时，所有的路径都会被格式化已#!开头，
    hashbang: true,
    history: false,
    saveScrollPosition: true,
    transitionOnLoad: true,
    routes: [{
        path: '/teacher-list',
        note: '教师列表',
        component: require('./view/teacher-list.vue')
    }, {
        path: '/teacher-detail/:teacherId',
        name: 'teacherDetail',
        note: '教师详情',
        component: require('./view/teacher-detail.vue')
    }, {
        path: '/zl-list',
        note: '资料列表',
        component: require('./view/zl-list.vue')
    }, {
        path: '/zl-detail/:zlId',
        name: 'zlDetail',
        note: '资料详情',
        component: require('./view/zl-detail.vue')
    }, {
        path: '/ban-list',
        note: '培训班列表',
        component: require('./view/ban-list.vue')
    }, {
        path: '/ban-detail/:banId',
        name: 'banDetail',
        note: '培训班详情',
        component: require('./view/ban-detail.vue')
    }]
});


//加载路由规则
// new Vue({
//     el: '#app',
//     template: '<App/>',
//     components: {
//         App
//     },
//     router:router,
//     render: h => h(App)
// })
var app = new Vue({
    el: '#app',
    template: '<App/>',
    components: {
        App
    },
    router
});

// router.beforeEach((to, from, next) => {

// });
