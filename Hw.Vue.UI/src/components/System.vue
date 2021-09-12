<template>
  <div>
    <device-list v-model="selected"></device-list>
  </div>
</template>

<script>
import DeviceList from "./common/DeviceList.vue";
export default {
  components: { DeviceList },
  data() {
    return {
      selected: ''
    }
  },
  methods: {
    update() {
      return this.$store.commit("update", {
        index: 1,
        data: {
          name: "SB" + this.$store.state.list.length,
          index: this.$store.state.list.length,
        },
      });
    },
    add() {
      return this.$store.commit("increment", {
        name: "SB" + this.$store.state.list.length,
        index: this.$store.state.list.length,
      });
    },
  },

  computed: {
    todolist() {
      return this.$store.state.list;
    },
  },
};
</script>

<style>
</style>