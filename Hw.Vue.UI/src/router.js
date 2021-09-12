import System from "./components/System.vue";
import Management from "./components/Management.vue";
import Pmenu from "./components/permission/Pmenu.vue";
import Report from "./components/permission/Report.vue";
import Home from "./components/Home.vue";
import Track from "./components/Track.vue";
import Login from './components/Login.vue'
import Index from './components/Index.vue'
import Default from './components/Default.vue'
import Model from "./components/modelHelper/Model.vue";
import Dashboard from "./components/dashboard/Dashboard.vue";

const routes = [
    {
        name: 'login',
        path: '/',
        component: Login,
    },
    {
        name: 'default',
        path: '/default',
        component: Default,
        meta: {
            keepAlive: true
        },
        children: [
            {
                path: '/default',
                component: Dashboard,
                meta: {
                    keepAlive: true
                },
                props: true
            },
            {
                name: 'Pmenu',
                path: '/:function/:model',
                component: Model,
                meta: {
                    keepAlive: true
                },
                props: true
            }, {
                name: 'Report',
                path: '/Report/:function/:model',
                component: Report,
                meta: {
                    keepAlive: true
                },
                props: true
            },
        ],
    },
    {
        name: 'index',
        path: '/index',
        component: Index,
        meta: {
            keepAlive: true
        },
        children: [
            {
                name: 'management',
                path: '/management',
                component: Management,
                children: [{
                    name: 'Pmenu',
                    path: '/management/:function/:model',
                    component: Pmenu,
                    meta: {
                        keepAlive: true
                    },
                    props: true
                }, {
                    name: 'Report',
                    path: '/Report/:function/:model',
                    component: Report,
                    meta: {
                        keepAlive: true
                    },
                    props: true
                },],
                meta: {
                    keepAlive: true
                }
            },
            {
                name: 'system',
                path: '/system',
                component: System,
                meta: {
                    keepAlive: true
                }
            },
            {
                name: 'home',
                path: '/home',
                component: Home,
                meta: {
                    keepAlive: true
                }
            },
            {
                name: 'track',
                path: '/track',
                component: Track,
                meta: {
                    keepAlive: true
                }
            }
        ]
    },

]

export default routes;
