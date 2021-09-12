<template>
  <div>
    <template v-for="item in items">
      <Submenu
        :key="item.id"
        v-if="item.children.length > 0 && item.meunAction == 0"
        :name="item.id"
      >
        <template slot="title">
          <Icon type="ios-stats" />
          {{ item.title }}
        </template>

        <sub-menu-item :items="item.children"> </sub-menu-item>
      </Submenu>
      <MenuItem :to="item.url" :key="item.id" :name="item.id" v-else>{{
        item.title
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
</style>