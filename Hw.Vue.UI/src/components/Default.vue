<style scoped>
.layout {
  background: #f5f7f9;
  overflow: hidden;
  height: 100%;
}
.ivu-layout {
  height: 100%;
  box-sizing: border-box;
  overflow: hidden;
}
.layout-header-bar {
  background: #fff;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: space-between;
}
.layout-logo-left {
  width: 90%;
  height: 30px;
  background: #5b6270;
  border-radius: 3px;
  margin: 15px auto;
}
.menu-icon {
  transition: all 0.2s;
  cursor: pointer;
}
.rotate-icon {
  transform: rotate(-90deg);
}
.menu-item span {
  display: inline-block;
  overflow: hidden;
  width: 69px;
  text-overflow: ellipsis;
  white-space: nowrap;
  vertical-align: bottom;
  transition: width 0.1s ease 0.2s;
}
.menu-item i {
  transform: translateX(0px);
  transition: font-size 0.2s ease, transform 0.2s ease;
  vertical-align: middle;
  font-size: 16px;
}
.collapsed-menu span {
  width: 0px;
  transition: width 0.2s ease;
}

.collapsed-menu i {
  transform: translateX(5px);
  transition: font-size 0.2s ease 0.2s, transform 0.2s ease 0.2s;
  vertical-align: middle;
  font-size: 22px;
}
.layout-header-bar-collapsedSider {
  display: inline-block;
}
.layout-header-bar-collapsedSider-btn {
  margin-left: 20px;
  padding: 0 5px;
}
</style>
<template>
  <div class="layout">
    <Layout>
      <Sider
        ref="side1"
        hide-trigger
        collapsible
        :collapsed-width="60"
        v-model="isCollapsed"
      >
        <hw-menu :collapsed="isCollapsed"></hw-menu>
      </Sider>
      <Layout>
        <Header :style="{ padding: 0 }" class="layout-header-bar">
          <div class="layout-header-bar-collapsedSider">
          
            <Button
              class="layout-header-bar-collapsedSider-btn"
              @click.native="collapsedSider"
              type="text"
            >
              <Icon :class="rotateIcon" custom="" size="24"></Icon
            ></Button>
            
          </div>

          <Dropdown
            trigger="click"
            @on-click="person"
            style="margin-left: 20px; margin-right: 20px"
          >
            <a href="javascript:void(0)" style="color: #000">
              <img
                style="
                  width: 28px;
                  background-color: #eeeeee;
                  vertical-align: middle;
                  margin-right: 5px;
                  border-radius: 14px;
                "
                src="../assets/logo1.png"
              />
              <span>伯伯而泵</span>
            </a>
            <DropdownMenu slot="list">
              <DropdownItem name="person">个人中心</DropdownItem>
              <DropdownItem name="logout" divided>安全退出</DropdownItem>
            </DropdownMenu>
          </Dropdown>
        </Header>
        <Content>
          <layout-tab></layout-tab>
          <keep-alive>
            <router-view :key="$route.path"></router-view>
          </keep-alive>
        </Content>
      </Layout>
    </Layout>
  </div>
</template>
<script>
import LayoutTab from "./layoutTab/LayoutTab.vue";
import HwMenu from "./menu/HwMenu.vue";
import { mapActions } from "vuex";
export default {
  components: { HwMenu, LayoutTab },
  data() {
    return {
      isCollapsed: false,
      openNames: [],
      activeName: "",
    };
  },

  computed: {
    rotateIcon() {
      return ["iconfont", !this.isCollapsed ? "icon-shouqi" : "icon-zhankai"];
    },
    menuitemClasses() {
      return ["menu-item", this.isCollapsed ? "collapsed-menu" : ""];
    },
  },
  methods: {
    ...mapActions([
      "clear", //also supports payload `this.nameOfAction(amount)`
    ]),

    collapsedSider() {
      this.$refs.side1.toggleCollapse();
    },
    person(item) {
      if (item === "logout") {
        this.clear();
        this.$router.push("/");
      }
    },
  },
};
</script>