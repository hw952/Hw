<template>
  <div class="layout-tab">
    <div class="layout-tab-left">
      <Button type="text" size="small">
        <Icon type="ios-arrow-back" />
      </Button>
    </div>
    <div class="layout-tab-content">
      <div
        @click="tabSelect(item)"
        :class="layoutItemStyle(item)"
        :key="index"
        v-for="(item, index) in menuList"
      >
        <span>{{ item.name }}</span>
        <Icon
          v-show="!item.canclose"
          @click.stop="closeTab(item)"
          style="margin-left: 10px"
          type="md-close-circle"
        />
      </div>
    </div>
    <div class="layout-tab-right">
      <Dropdown style="margin-left: 10px">
        <Button size="small" type="text">
          <Icon type="ios-arrow-down"></Icon>
        </Button>
        <DropdownMenu slot="list">
          <DropdownItem>驴打滚</DropdownItem>
          <DropdownItem>炸酱面</DropdownItem>
          <DropdownItem disabled>豆汁儿</DropdownItem>
          <DropdownItem>冰糖葫芦</DropdownItem>
          <DropdownItem divided>北京烤鸭</DropdownItem>
        </DropdownMenu>
      </Dropdown>

      <Button type="text" size="small" style="margin-left: 0px">
        <Icon type="ios-arrow-forward" />
      </Button>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  computed: {
    layoutItemStyle() {
      return function (item) {
        let s = [
          "layout-tab-item",
          this.$store.getters.activeMenu.url == item.url
            ? "layout-tab-item-selected"
            : "",
        ];
        return s;
      };
    },
  },
  methods: {
    ...mapActions([
      "updateActiveMenu",
      "removeOpenMenu", //also supports payload `this.updateActiveMenu(amount)`
    ]),

    tabSelect(item) {
      if (this.$store.getters.activeMenu.url == item.url) return;
      this.$router.push({ path: item.url });
    },
    closeTab(item) {
      let index = this.menuList.findIndex((d) => d.url == item.url);
      this.removeOpenMenu(item);
      this.menuList.splice(index, 1);
      if (this.$store.getters.activeMenu.url == item.url) {
        this.$router.push({
          path: this.menuList[this.menuList.length - 1].url,
        });
      }
    },
    activeMenu(to) {
      let openNames = [];
      function find(list) {
        for (const l of list) {
          if (l.children.length > 0) {
            openNames.push(l.name);
            let temp = find(l.children);
            if (!temp) {
              openNames.slice(openNames.length - 1, 1);
            }
            return temp;
          } else {
            if (l.url == to.path) return l;
          }
        }
      }

      let menu = find(this.$store.getters.menuList);

      if (!this.menuList.some((d) => d.url == to.path))
        this.menuList.push(menu);
      if (menu) {
        menu.openNames = openNames;
        this.updateActiveMenu(menu);
      } else {
        this.menuList[0].openNames = openNames;
        this.updateActiveMenu(this.menuList[0]);
      }
    },
  },
  data() {
    return {
      menuList: this.$store.getters.openMenuList,
    };
  },
  watch: {
    $route(to) {
      this.activeMenu(to);
    },
  },
};
</script>

<style>
.layout-tab {
  height: 40px;
  margin: 10px;
  display: flex;
  align-items: center;
}
.layout-tab-item {
  background: #fff;
  padding: 4px 10px;
  border-radius: 4px;
  cursor: pointer;
  margin: 0 5px;
}
.layout-tab-item-selected {
  color: blue;
}
.layout-tab-item i {
  margin-left: 10px;
  cursor: pointer;
}
.layout-tab-content {
  flex: 1;
  display: flex;
  align-items: center;
}
</style>