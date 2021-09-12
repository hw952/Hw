<template>
  <div v-click-outside="hide" class="ivu-select ivu-select-default">
    <div tabindex="0" class="ivu-select-selection ivu-form-item-content">
      <div @mouseover="mouseover" @mouseleave="mouseleave">
        <div @click="clickInputShow" style="padding: 0 10px">
          <template v-for="(item, index) in multipleShowVal">
            <div v-if="multiple" class="ivu-tag ivu-tag-checked" :key="index">
              <span class="ivu-tag-text">{{ item }}</span>
              <!-- <i
                class="ivu-icon ivu-icon-ios-close"
                @click.stop="removeVal(index)"
              ></i> -->
            </div>
          </template>
          <span
            v-if="!multiple && queryVal != ''"
            class="ivu-select-selected-value"
            >{{ queryVal }}</span
          >
          <span
            v-if="multipleHideVal.length === 0 && multiple"
            class="ivu-select-placeholder"
            style=""
            >请选择</span
          >
          <span
            v-if="!value && !multiple"
            class="ivu-select-placeholder"
            style=""
            >请选择</span
          >
          <span class="" style="display: none"></span>
        </div>
        <i :class="collapsedIcon" @click="clickIcon"></i>
      </div>
      <div
        v-show="showTree"
        class="ivu-select-dropdown"
        style="
          max-height: 500px;
          min-height: 300px;
          overflow-y: scroll;
          z-index: 9999;
          width: 100%;
        "
      >
        <div style="width: 95%; margin-left: 10px" v-show="showQuery">
          <Input
            v-model="queryTreeVal"
            placeholder="请输入筛选条件"
            @on-change="selectTreeChange"
          />
        </div>
        <div style="width: 95%; margin-left: 10px">
          <Tree
            :data="queryData"
            :multiple="multiple"
            :show-checkbox="multiple"
            :check-directly="multiple"
            ref="tree"
            @on-check-change="selectChange"
            @on-select-change="selectChange"
            :render="renderContent"
          ></Tree>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import ClickOutside from "vue-click-outside";
export default {
  directives: {
    ClickOutside,
  },
  name:'SelectTree',
  props: {
    treeData: {
      type: Array,
    },
    value: {},
    filterable: {
      type: Boolean,
      default: false,
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    clearable: {
      type: Boolean,
      default: false,
    },
    showQuery: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: true,
    },
  },
  data() {
    return {
      queryVal: "",
      hideValue: "",
      valueType: "string",
      selectNode: [],
      multipleShowVal: [],
      multipleHideVal: [],
      showTree: false,
      iconVal: "ios-arrow-down",
      cloneData: JSON.parse(JSON.stringify(this.treeData)),
      showData: [],
      queryData: [],
      queryTreeVal: "",
    };
  },
  computed: {
   collapsedIcon() {
      return [
        "ivu-icon",
        this.showTree ? "ivu-select-collapsed" : "",
        "ivu-icon-ios-arrow-down",
        "ivu-select-arrow",
      ];
    },
  },
  methods: {
   
    renderContent(h, { data }) {
      return h(
        "span",
        {
          style: {
            display: "inline-block",
            width: "100%",
          },
        },
        [
          h("span", [
            h("Icon", {
              props: {
                type: "ios-paper-outline",
              },
              style: {
                marginRight: "8px",
              },
            }),
            h("span", data.name),
          ]),
          h("span", {
            style: {
              display: "inline-block",
              float: "right",
              marginRight: "32px",
            },
          }),
        ]
      );
    },

    hide() {
      this.showTree = false;
    },
    clickIcon() {
      if (this.disabled) {
        if (this.iconVal === "ios-close-circle") {
          this.clearVal();
        } else {
          this.showSelectTree();
        }
      }
    },
    pickTree(val) {
      if (this.valueType === "string") {
        for (let i = 0; i < this.showData.length; i++) {
          if (this.showData[i].id === val) {
            this.showData[i].selected = false;
          } else if (this.showData[i].children !== undefined) {
            this.recursionPickTree(val, this.showData[i].children);
          }
        }
      } else {
        for (let j = 0; j < val.length; j++) {
          for (let i = 0; i < this.showData.length; i++) {
            if (this.showData[i].id === val[j]) {
              this.showData[i].selected = false;
            } else if (this.showData[i].children !== undefined) {
              this.recursionPickTree(val[j], this.showData[i].children);
            }
          }
        }
      }
    },
    recursionPickTree(val, data) {
      for (let i = 0; i < data.length; i++) {
        if (data[i].id === val) {
          data[i].selected = false;
        } else if (
          data[i].children !== undefined &&
          data[i].children.length > 0
        ) {
          this.recursionPickTree(val, data[i].children);
        }
      }
    },
    selectTreeChange() {
      this.queryData = JSON.parse(JSON.stringify(this.showData));
      if (this.queryTreeVal !== "") {
        let removeData = [];
        for (let i = 0; i < this.queryData.length; i++) {
          let check = this.recursionTreeData(
            this.queryTreeVal,
            this.queryData[i]
          );
          if (!check) {
            removeData.push(i);
          }
        }
        this.queryData = this.queryData.filter((o, index) => {
          return !removeData.includes(index);
        });
      }
    },
    recursionTreeData(query, data) {
      let isCheck = false;
      let removeData = [];
      // 只验证最底层的节点是否有符合要求的值
      if (data.children !== undefined) {
        for (let i = 0; i < data.children.length; i++) {
          if (!isCheck) {
            isCheck = this.recursionTreeData(query, data.children[i]);
            if (!isCheck) {
              removeData.push(i);
            }
          } else {
            if (!this.recursionTreeData(query, data.children[i])) {
              removeData.push(i);
            }
          }
        }
      } else {
        // 验证当前节点是否有符合要求的值
        if (data.name.indexOf(query) !== -1) {
          return true;
        } else {
          return false;
        }
      }
      data.children = data.children.filter((o, index) => {
        return !removeData.includes(index);
      });
      // 如果子节点中有一个符合要求，则父节点就直接返回true，不做删除，若子节点没有一个符合要求，则再次验证当前的节点
      if (isCheck) {
        return isCheck;
      }
      // 表示当前的节点有值在里面
      if (data.name.indexOf(query) !== -1) {
        return true;
      } else {
        return false;
      }
    },
    clearVal() {
      if (this.disabled) {
        if (
          this.clearable &&
          !this.multiple &&
          this.iconVal === "ios-close-circle"
        ) {
          this.pickTree(this.hideValue);
          this.queryVal = "";
          this.hideValue = "";
          if (this.showTree) {
            this.iconVal = "ios-arrow-up";
          } else {
            this.iconVal = "ios-arrow-down";
          }
          this.$emit("input", "");
        }
      }
    },
    mouseover() {
      if (
        this.clearable &&
        !this.multiple &&
        this.iconVal !== "ios-close-circle" &&
        this.hideValue !== ""
      ) {
        this.iconVal = "ios-close-circle";
      }
    },
    mouseleave() {
      if (this.clearable && !this.multiple) {
        if (this.showTree) {
          this.iconVal = "ios-arrow-up";
        } else {
          this.iconVal = "ios-arrow-down";
        }
      }
    },
    selectChange(obj) {
      let hideVal;
      let showVal;
      if (this.multiple) {
        this.multipleShowVal = [];
        this.multipleHideVal = [];
        for (let i = 0; i < obj.length; i++) {
          this.multipleShowVal.push(obj[i].name);
          this.multipleHideVal.push(obj[i].id);
        }
        this.$emit("input", this.multipleHideVal);
        hideVal = this.multipleHideVal;
        showVal = this.multipleShowVal;
      } else {
        if (obj.length === 0) {
          this.queryVal = "";
          this.hideValue = "";
          this.$emit("input", "");
        } else {
          this.queryVal = obj[0].name;
          this.hideValue = obj[0].id;
          this.$emit("input", obj[0].id);
        }
        this.showTree = false;
        this.iconVal = "ios-arrow-down";
        hideVal = this.hideValue;
        showVal = this.queryVal;
        this.$emit("on-select-change", this.queryVal, this.hideValue);
      }
      this.$emit("on-select-change", hideVal, showVal);
    },
    // 点击图标的时候展示树形菜单
    clickInputShow() {
      if (this.disabled) {
        this.showSelectTree();
      }
    },
    showSelectTree() {
      if (this.showTree) {
        this.showTree = false;
      } else {
        this.showTree = true;
      }
      if (this.iconVal !== "ios-close-circle") {
        if (this.iconVal === "ios-arrow-down") {
          this.iconVal = "ios-arrow-up";
        } else if (this.iconVal === "ios-arrow-up") {
          this.iconVal = "ios-arrow-down";
        }
      }
    },
    // 重置菜单的数据
    resetTreeData() {
      this.cloneData = JSON.parse(JSON.stringify(this.treeData));
    },
    // 多选模式的时候删除节点的数据
    removeVal(index) {
      if (this.disabled) {
        for (let i = 0; i < this.showData.length; i++) {
          if (this.showData[i].id === this.multipleHideVal[index]) {
            this.hideValue = this.treeData[i].id;
            this.queryVal = this.treeData[i].name;
            this.showData[i].selected = false;
          } else if (this.showData[i].children !== undefined) {
            this.recursionRemoveTreeData(
              this.showData[i].children,
              this.multipleHideVal[index]
            );
          }
        }
        this.multipleShowVal.splice(index, 1);
        this.multipleHideVal.splice(index, 1);
      }
    },
    recursionRemoveTreeData(data, val) {
      for (let i = 0; i < data.length; i++) {
        if (data[i].id === val) {
          data[i].selected = false;
          return;
        } else if (
          data[i].children !== undefined &&
          data[i].children.length > 0
        ) {
          this.recursionRemoveTreeData(data[i].children, val);
        }
      }
    },
    // 验证当前下拉
    checkValueType() {
      if (typeof this.id !== "string") {
        this.valueType = "array";
      }
    },
    initQueryMultiple() {
      if (!this.value) return;
      for (let i = 0; i < this.value.length; i++) {
        for (let j = 0; j < this.cloneData.length; j++) {
          if (this.cloneData[j].id === this.value[i]) {
            this.multipleShowVal.push(this.cloneData[j].name);
            this.multipleHideVal.push(this.cloneData[j].id);
            this.$emit("input", this.multipleHideVal);
            this.cloneData[j].selected = true;
            this.cloneData[j].checked = true;
            break;
          } else if (this.cloneData[j].children !== undefined) {
            this.recursionQueryTreeData(
              this.cloneData[j].children,
              this.value[i]
            );
          }
        }
      }
      this.queryData = JSON.parse(JSON.stringify(this.cloneData));
    },
    initQueryVal() {
      for (let i = 0; i < this.cloneData.length; i++) {
        if (this.cloneData[i].id === this.value) {
          this.hideValue = this.cloneData[i].id;
          this.queryVal = this.cloneData[i].name;
          this.$emit("input", this.cloneData[i].id);
          this.cloneData[i].selected = true;
          return;
        } else if (this.cloneData[i].children !== undefined) {
          this.recursionQueryTreeData(this.cloneData[i].children, this.value);
        }
      }
    },
    initQuery() {
      if (this.multiple) this.initQueryMultiple();
      else this.initQueryVal();
    },
    recursionQueryTreeData(data, val) {
      for (let i = 0; i < data.length; i++) {
        if (data[i].id === val) {
          // 当前会多选
          if (this.multiple) {
            this.multipleShowVal.push(data[i].name);
            this.multipleHideVal.push(data[i].id);
            this.$emit("input", this.multipleHideVal);
            data[i].checked = true;
          } else {
            this.hideValue = data[i].id;
            this.queryVal = data[i].name;
            this.$emit("input", data[i].id);
          }
          data[i].selected = true;
        } else if (data[i].children !== undefined) {
          this.recursionQueryTreeData(data[i].children, val);
        }
      }
    },
    removeShowVal(data, val, showVal, hideVal) {
      for (let j = 0; j < data.length; j++) {
        if (data[j].id === val) {
          showVal.push(data[j].name);
          hideVal.push(data[j].id);
          break;
        } else if (
          data[j].children !== undefined &&
          data[j].children.length > 0
        ) {
          this.removeShowVal(data[j].children, val, showVal, hideVal);
        }
      }
    },
    setSelected(val, data) {
      for (let item of data) {
        if (item.id == val) {
          item.selected = true;
          return item;
        }
        if (item.children && item.children.length > 0) {
          return this.setSelected(val, item.children);
        }
      }
    },
  },
  watch: {
    value(val) {
      console.log("selectedTree");
      if (val === "") {
        this.pickTree(this.hideValue);
        this.queryVal = "";
        this.hideValue = "";
        this.selectChange([]);
        let temp_data = JSON.parse(JSON.stringify(this.cloneData));
        this.queryData = temp_data;

        if (this.showTree) {
          this.iconVal = "ios-arrow-up";
        } else {
          this.iconVal = "ios-arrow-down";
        }
        if (this.multiple) this.$emit("input", []);
        else this.$emit("input", "");
      }
    },
    treeData(val) {
      console.log("treeData");
      this.cloneData = JSON.parse(JSON.stringify(val));
      this.showData = JSON.parse(JSON.stringify(this.cloneData));
      this.queryData = this.showData;
      let multipleHideValNew = [];
      let multipleShowValNew = [];
      if (this.multiple) {
        for (let i = 0; i < this.multipleHideVal.length; i++) {
          this.removeShowVal(
            this.showData,
            this.multipleHideVal[i],
            multipleShowValNew,
            multipleHideValNew
          );
        }
        this.multipleHideVal = multipleHideValNew;
        this.multipleShowVal = multipleShowValNew;
        this.initQueryMultiple();
      } else {
        this.removeShowVal(
          this.showData,
          this.hideValue,
          multipleShowValNew,
          multipleHideValNew
        );
        if (multipleHideValNew.length === 0) {
          this.pickTree(this.hideValue);
          this.queryVal = "";
          this.hideValue = "";
        }
        this.initQueryVal();
      }
    },
  },
  mounted() {
    this.checkValueType();
    // 要先初始化选中的数据，再将初始化好的数据赋值给到当前的tree，否则将导致选中的效果显示不出来
    if (this.multiple) {
      this.initQueryMultiple();
    } else {
      this.initQueryVal();
    }
    this.showData = JSON.parse(JSON.stringify(this.cloneData));
    this.queryData = this.showData;
  },
};
</script>
<style scoped>
.ivu-select-placeholder {
  display: block;
  height: 30px;
  line-height: 30px;
  color: #c5c8ce;
  font-size: 14px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  padding-right: 22px;
}
.ivu-select-selected-value {
  display: block;
  height: 30px;
  line-height: 30px;
  font-size: 14px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  padding-right: 24px;
}
.ivu-select-collapsed {
  transform: translateY(-50%) rotate(180deg);
  display: inline-block;
}
</style>