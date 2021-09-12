<template>
  <div
    v-click-outside="ClickOutCollapsed"
    class="ivu-select ivu-select-single ivu-select-default"
  >
    <div
      @click.stop="isCollapsed = !isCollapsed"
      tabindex="0"
      class="ivu-select-selection"
    >
      <input type="hidden" />
      <div class="">
        <!---->
        <!---->
        <span :class="placeholder">{{
          value ? value : "Select"
        }}</span>
        <!---->
        <!---->
        <i :class="collapsedIcon"></i>
      </div>
    </div>
    <div
      v-show="isCollapsed"
      class="ivu-select-dropdown"
      style="
        position: absolute;
        will-change: top, left;
        transform-origin: center top;
        width: 100%;
        top: 33px;
        left: 0px;
      "
      x-placement="bottom-start"
    >
      <Icon size="26" type="ios-add" @click="selected('ios-add')" />
      <Icon size="26" type="md-add" @click="selected('md-add')" />
      <Icon
        size="26"
        type="ios-add-circle"
        @click="selected('ios-add-circle')"
      />
      <Icon
        size="26"
        type="ios-add-circle-outline"
        @click="selected('ios-add-circle-outline')"
      />
      <Icon size="26" type="md-add-circle" @click="selected('md-add-circle')" />
      <Icon size="26" type="ios-alarm" @click="selected('ios-alarm')" />
      <Icon
        size="26"
        type="ios-alarm-outline"
        @click="selected('ios-alarm-outline')"
      />
      <Icon size="26" type="md-alarm" @click="selected('md-alarm')" />
      <Icon size="26" type="ios-albums" @click="selected('ios-albums')" />
      <Icon
        size="26"
        type="ios-albums-outline"
        @click="selected('ios-albums-outline')"
      />
      <Icon size="26" type="md-albums" @click="selected('md-albums')" />
      <Icon size="26" type="ios-alert" @click="selected('ios-alert')" />
      <Icon
        size="26"
        type="ios-alert-outline"
        @click="selected('ios-alert-outline')"
      />
      <Icon size="26" type="md-alert" @click="selected('md-alert')" />
      <Icon
        size="26"
        type="ios-american-football"
        @click="selected('ios-american-football')"
      />
      <Icon
        size="26"
        type="ios-american-football-outline"
        @click="selected('ios-american-football-outline')"
      />
      <Icon
        size="26"
        type="md-american-football"
        @click="selected('md-american-football')"
      />
      <Icon size="26" type="ios-analytics" @click="selected('ios-analytics')" />
      <Icon
        size="26"
        type="ios-analytics-outline"
        @click="selected('ios-analytics-outline')"
      />
      <Icon size="26" type="md-analytics" @click="selected('md-analytics')" />
      <Icon size="26" type="logo-android" @click="selected('logo-android')" />
      <Icon size="26" type="logo-angular" @click="selected('logo-angular')" />
      <Icon size="26" type="ios-aperture" @click="selected('ios-aperture')" />
      <Icon
        size="26"
        type="ios-aperture-outline"
        @click="selected('ios-aperture-outline')"
      />
      <Icon size="26" type="md-aperture" @click="selected('md-aperture')" />
      <Icon size="26" type="logo-apple" @click="selected('logo-apple')" />
      <Icon size="26" type="ios-apps" @click="selected('ios-apps')" />
      <Icon
        size="26"
        type="ios-apps-outline"
        @click="selected('ios-apps-outline')"
      />
      <Icon size="26" type="md-apps" @click="selected('md-apps')" />
      <Icon size="26" type="ios-appstore" @click="selected('ios-appstore')" />
      <Icon
        size="26"
        type="ios-appstore-outline"
        @click="selected('ios-appstore-outline')"
      />
      <Icon size="26" type="md-appstore" @click="selected('md-appstore')" />
    </div>
  </div>
</template>

<script>
import ClickOutside from "vue-click-outside";
export default {
  directives: {
    ClickOutside,
  },
  props: {
    value: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      isCollapsed: false,
    };
  },
  computed: {
    collapsedIcon() {
      return [
        "ivu-icon",
        this.isCollapsed ? "ivu-select-collapsed" : "",
        "ivu-icon-ios-arrow-down",
        "ivu-select-arrow",
      ];
    },
    placeholder() {
      return [
        this.value ? "ivu-select-selected-value" : "ivu-select-placeholder",
      ];
    },
  },
  methods: {
    ClickOutCollapsed() {
      this.isCollapsed = false;
    },
    selected(type) {
      this.value = type;
      this.$emit("input", this.value);
      this.isCollapsed = false;
    },
  },
};
</script>
<style scoped>
.ivu-select {
  position: relative;
}
.ivu-select-collapsed {
  transform: translateY(-50%) rotate(180deg);
  display: inline-block;
}
.ivu-select-dropdown i {
  margin: 10px;
}
</style>