<template>
  <div>
    <div class="logo">{{collapsed?"L":'Logo'}}</div>

    <Menu
      v-show="!collapsed"
      ref="side_menu"
      theme="dark"
      width="auto"
      :open-names="activeOpenName"
      :active-name="activeName"
    >
      <sub-menu-item :items="data"></sub-menu-item>
    </Menu>

    <min-menu v-show="collapsed" :items="data"></min-menu>
  </div>
</template>



<script>
import SubMenuItem from "./SubMenuItem.vue";
import { mapActions } from "vuex";
import MinMenu from "./MinMenu.vue";

export default {
  components: { SubMenuItem, MinMenu },
  data() {
    return {
      data: [],
    };
  },
  props: {
    collapsed: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    activeName() {
      return this.$store.getters.activeMenu.name;
    },
    activeOpenName() {
      return this.$store.getters.openNames;
    },
  },

  methods: {
    ...mapActions(["updateMenuLis"]),
  },
  created() {
   this.http.post(`/Menu/Query`).then((data) => {
      if (data.data.state == 200) {
        this.data = data.data.data;
        this.updateMenuLis(data.data.data);
        this.$nextTick(() => {
          this.$refs.side_menu.updateOpened();
          this.$refs.side_menu.updateActiveName();
        });
      }
    });
  },
};
</script>

<style  scoped>
.logo {
  margin-top: 10px;
  overflow: hidden;
  color: #fff;
  font-size: 50px;
  text-align: center;
}
</style>

