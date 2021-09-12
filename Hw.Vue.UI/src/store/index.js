import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from "vuex-persistedstate"
Vue.use(Vuex)

const store = new Vuex.Store({
    plugins: [createPersistedState({
        storage: window.sessionStorage
    })],
    state: {
        list: [],
        fleet: [], //车队
        device: [], //车辆
        fleetDevice: [], //车队车辆
        menuList: [],
        activeMenu: { url: '/default' },
        openNames: [],
        openMenu: [{
            children: [],
            url: "/default",
            actionType: "菜单",
            icon: "string",
            name: "首页",
            canclose: true,
        },]
    },
    actions: {

        updateMenuLis({ commit }, menuList) {
            commit('setMenuList', menuList);
        },

        updateActiveMenu({ commit }, menu) {
            commit('setActiveMenu', menu);
        },

        removeOpenMenu({ commit }, name) {
            commit('removeOpenMenu', name);
        },

        clear({ commit }) {
            commit('clear');
        }
    },
    getters: {
        menuList: state => {
            return state.menuList.filter((d) => { return d });
        },
        activeMenu: state => {
            return state.activeMenu;
        },
        openMenuList: state => {
            return state.openMenu.filter((d) => { return d });
        },
        openNames: state => {
            return state.openNames;
        }
    },
    mutations: {

        removeOpenMenu(state, item) {
            let index = state.openMenu.findIndex((d) => d.url == item.url)
            if (index >= 0) {
                state.openMenu.splice(index, 1);
            }
        },
        clear(state) {

            state.menuList = [];
            state.activeMenu = { url: '/default' };
            state.openMenu = [{
                children: [],
                url: "/default",
                actionType: "菜单",
                icon: "string",
                name: "首页",
                canclose: true,
            }]

        },
        setActiveMenu(state, menu) {
            if (!menu) return;
            state.activeMenu = menu;
            state.openNames = menu.openNames;
            if (!state.openMenu.some((d) => d.url == menu.url))
                state.openMenu.push(menu);
        },


        setMenuList(state, menuList) {
            state.menuList = menuList;
        },

        increment(state, parm) {
            state.list.push(parm);
        },
        update(state, data) {
            Vue.set(state.list, data.index, data.data);
        },
        setFleet(state, parm) {
            state.fleet = parm;
        },
        setDevice(state, parm) {
            state.device = parm;
        },
        setfleetDevice(state) {
            let temp = [];
            let guid = 1;

            function fleets(fleet, left) {
                let i = left;
                for (const f of fleet) {
                    let t = Object.assign(f, { type: 'fleet', left: i, guid: guid++ });
                    temp.push(t);
                    if (t.children && t.children.length > 0) {
                        fleets(t.children, i + 1);
                    }
                    state.device.filter((d) => d.fleetId == f.id).forEach((d) => {
                        let t1 = Object.assign(d, { type: 'device', left: i + 1, guid: guid++ });
                        temp.push(t1);
                    });
                }
            }
            fleets(state.fleet, 0);
            state.fleetDevice = temp;
        }

    }
})
export default store