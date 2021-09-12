<template>
  <div class="min-menu">
    <div v-for="item in items" :key="item.url">
      <Divider type="horizontal" />
      <Dropdown @on-click="itemClick" placement="right-start">
        <a v-if="first" href="javascript:void(0)">
          <Icon color="#fff" size="25" :type="item.icon"></Icon>
        </a>
        <DropdownItem :name="item" v-else>
          {{ item.name }}
          <Icon type="ios-arrow-forward"></Icon>
        </DropdownItem>
        <DropdownMenu slot="list">
          <template v-for="i in item.children">
            <DropdownItem :name="i.url" v-if="i.children == 0" :key="i.name">{{
              i.name
            }}</DropdownItem>
            <min-menu
              v-else
              :items="i.children"
              :key="i.name"
              :first="false"
            ></min-menu>
          </template>
        </DropdownMenu>
      </Dropdown>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    itemClick(url) {
    
      this.$router.push({
        path: url,
      });
    },
  },
  props: {
    items: {
      type: Array,
      default: function () {
        return [];
      },
    },
    first: {
      type: Boolean,
      default: true,
    },
  },
};
</script>

<style>
.min-menu > div {
  text-align: center;
}
</style>