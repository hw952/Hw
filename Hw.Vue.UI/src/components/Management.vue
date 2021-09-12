<template>
  <Layout :style="{ height: '100%', background: '#fff' }">
    <Sider hide-trigger :style="{ height: '100%', background: '#fff' }">
      <Menu
        ref="menuName"
        theme="light"
        width="auto"
        :open-names="openNames"
        :active-name="activeName"
      >
        <sub-menu-item :items="data"></sub-menu-item>

        <!--         
        <Submenu name="1">
          <template slot="title">
            <Icon type="ios-navigate"></Icon>
            {{ $t("permission") }}
          </template>
          <MenuItem to="/management/permission/menu" name="1-1">{{ $t("menu") }}</MenuItem>
          <MenuItem to="/management/permission/role" name="1-2">{{ $t("role") }}</MenuItem>
          <MenuItem to="/management/permission/user" name="1-3">用户管理</MenuItem>
        </Submenu>
        <Submenu name="2">
          <template slot="title">
            <Icon type="ios-keypad"></Icon>
            Item 2
          </template>
          <MenuItem name="2-1">Option 1</MenuItem>
          <MenuItem name="2-2">Option 2</MenuItem>
        </Submenu>
        <Submenu name="3">
          <template slot="title">
            <Icon type="ios-analytics"></Icon>
            Item 3
          </template>
          <MenuItem name="3-1">Option 1</MenuItem>
          <MenuItem name="3-2">Option 2</MenuItem>
        </Submenu> -->
      </Menu>
    </Sider>

    <Layout>
      <Header
        :style="{
          padding: 0,
          background: '#eee',
          boxShadow: '0 2px 3px 2px rgba(0,0,0,.1)',
        }"
      >
        <ul class="ul-tab">
          <li v-for="tab in tabs" :key="tab" :class="[tabActive(tab)]">
            <span @click="tabClick(tab)">{{ getPathName(tab) }}</span
            ><Icon @click="tabCloseClick(tab)" type="md-close-circle" />
          </li>
        </ul>
      </Header>
      <Content :style="{ padding: '10px', height: '100%', background: '#fff' }">
        <keep-alive>
          <router-view :key="$route.path"></router-view>
        </keep-alive>
      </Content>
    </Layout>
  </Layout>
</template>

<script>
import SubMenuItem from "./common/SubMenuItem.vue";

export default {
  components: { SubMenuItem },
  methods: {
    tabActive: function (p) {
      if (p == this.curView) return "ul-tab-item ul-tab-item-active";
      else return "ul-tab-item";
    },
    tabClick: function (tab) {
      this.curView = tab;
      this.$router.push(this.curView);
    },
    tabCloseClick: function (tab) {
      if (this.curView == tab && this.tabs.length == 1) return;
      let index = this.tabs.findIndex((d) => d == tab);
      this.tabs.splice(index, 1);
      if (this.curView == tab) this.curView = this.tabs[this.tabs.length - 1];
    },
    getUrlName(path, data) {
      for (const item of data) {
        if (item.url == path) return item.name;
        if (item.children.length > 0) {
          let temp = this.getUrlName(path, item.children);
          if (temp) return temp;
        }
      }
    },
  },
  computed: {
    getPathName() {
      return (data) => {
        return this.getUrlName(data, this.data);
      };
    },
  },
  data() {
    return {
      activeName: "",
      openNames: [],
      data: [],
      curView: this.$route.path,
      tabs: [this.$route.path],
    };
  },
  created() {
   this.http
      .post(`/menu/QueryAllNoWithMenuAction`)
      .then((data) => {
        if (data.data.state == 200) {
          this.data = data.data.data;
        }
      });
  },
  watch: {
    $route(to) {
      if (this.tabs.findIndex((d) => d == to.path) <= -1)
        this.tabs.push(to.path);
      this.curView = to.path;
    },
    data(val) {
      let ids = [];
      let activeName = "";
      function getUrlId(path, data) {
        for (const item of data) {
          if (item.url == path) {
            activeName = item.id;
            return true;
          }
          if (item.children.length > 0) {
            let temp = getUrlId(path, item.children);
            if (temp) {
              ids.push(item.id);
              return temp;
            }
          } 
        }
      }

      getUrlId(this.curView, val);
      this.activeName = activeName;
      //ids = ids.slice(ids.length - 2, 1);
      this.openNames = ids;
      console.log("----------");
      console.log(ids);
      this.$nextTick(() => {
        this.$refs.menuName.updateOpened();
        this.$refs.menuName.updateActiveName();
      });
    },
  },
};
</script>

<style scoped>
.ul-tab-item {
  display: inline-block;
  padding: 0 10px;
  box-sizing: border-box;
  line-height: 62px;
  height: 62px;
}
.ul-tab-item-active {
  border-bottom: 2px solid #027aff;
}
.ul-tab-item i {
  padding: 0 5px;
  cursor: pointer;
}
.ul-tab-item span {
  display: inline-block;
  cursor: pointer;
}
.layout {
  height: 100%;
  background: #f5f7f9;
  position: relative;
  border-radius: 4px;
  overflow: hidden;
}
.layout-logo {
  width: 100px;
  height: 30px;
  background: #5b6270;
  border-radius: 3px;
  float: left;
  position: relative;
  top: 15px;
  left: 20px;
}
.layout-nav {
  width: 420px;
  margin: 0 auto;
  margin-right: 20px;
}
.layout-footer-center {
  text-align: center;
}
.ivu-menu-light{
  height: 100%;
}
</style>