<template>
  <div>
    <template v-for="item in items">
      <Submenu
        :key="item.id"
        v-if="item.children.length > 0 && item.actionType == '菜单'"
        :name="item.name"
      >
        <template slot="title">
          <Icon :type="item.icon" />
          {{ item.name }}
        </template>

        <sub-menu-item :items="item.children"> </sub-menu-item>
      </Submenu>
      <MenuItem :to="item.url" :key="item.id" :name="item.name" v-else>{{
        item.name
      }}</MenuItem>
    </template>
  </div>
</template>

 


<script>
export default {
  name: "sub-menu-item",
  props: ["items"],
};
</script>

<style>
.ivu-menu-submenu-title,
.ivu-menu-item {
  white-space: nowrap;
}
</style>