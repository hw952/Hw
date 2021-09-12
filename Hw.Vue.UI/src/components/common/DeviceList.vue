<template>
  <div class="device-list">
    <div class="device-list-tools">
      <ButtonGroup>
        <Button @click="deleteClick" size="small">
          <Checkbox v-model="single"></Checkbox>
        </Button>

        <div class="ivu-dropdown ivu-group-search">
          <Input style="width:120px" v-model="searchKey" size="small" />
          <Button
            icon="ios-search"
            @click="search"
            size="small"
            type="primary"
            >{{ $t("search") }}</Button
          >
        </div>

        <!-- <Dropdown
          v-click-outside="dropClick2"
          ref="box"
          :visible="dropVisible"
          trigger="custom"
        >
          <Button @click.stop.prevent="dropClick" size="small" type="primary">
            {{ $t("filterColumn") }}
            <Icon type="ios-arrow-down"></Icon>
          </Button>
          <DropdownMenu slot="list">
            <template v-for="item in columns2">
              <DropdownItem :key="item.key">
                <Checkbox
                  :disabled="getColumns2Count && item.visible"
                  v-model="item.visible"
                  >{{ item.title }}</Checkbox
                ></DropdownItem
              >
            </template>
          </DropdownMenu>
        </Dropdown> -->
      </ButtonGroup>
    </div>

    <div class="device-list-content">
      <template v-for="(item, index) in fleetDevice">
        <div
          v-if="data[index].visible || data[index].visible == undefined"
          class="device-list-item"
          :style="{ 'padding-left': item.left * 20 + 'px' }"
          :key="index"
        >
          <div>
            <Checkbox
              :indeterminate="data[index].indeterminate"
              v-model="data[index].check"
              @on-change="selectedChange(data[index])"
            ></Checkbox>
            <Icon v-if="item.type == 'fleet'" type="md-list" />
            <Icon v-else type="md-subway" />
            <span v-on:click="clickSelected(index, item)">
              {{ item.name }}</span
            >
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script>

export default {
  props: ["value"],
  data() {
    return {
      data: [],
      searchKey: "",
    };
  },
  methods: {
    search: function () {
      if (this.searchKey) {
        for (let i = 0; i < this.data.length; i++) {
          let item = this.data[i];
          if (item.name.indexOf(this.searchKey) >= 0 || item.type=='fleet') {
            item.visible = true;
          } else item.visible = false;
          this.$set(this.data, i, item);
        }
      } else
        for (let i = 0; i < this.data.length; i++) {
          let item = this.data[i];
          item.visible = true;
          this.$set(this.data, i, item);
        }
    },
    renderDevice(item) {
      //单选处理/父节点半选处理
      for (let i = 0; i < this.data.length; i++) {
        const element = this.data[i];
        if (element.guid != item.guid && element.check == true) {
          element.check = false;
          this.$set(this.data, i, element);
        }
        if (element.indeterminate) {
          element.indeterminate = false;
          this.$set(this.data, i, element);
        }
      }
      //父节点半选处理
      if (item.check) {
        let j = this.data.findIndex((d) => d.guid == item.guid);
        let jdata = this.data[j];
        let left = jdata.left;
        for (let t = j - 1; t >= 0; t--) {
          const element = this.data[t];
          if (element.left < left) {
            element.indeterminate = true;
            this.$set(this.data, t, element);
            left = element.left;
          }
        }
      }
    },

    clickSelected(index, item) {
      if (!item.check || item.check == undefined) item.check = true;
      else item.check = false;
      this.$set(this.data, index, item);
      this.renderDevice(item);
      if (item.check) {
        this.$emit("input", item.id);
      } else {
        this.$emit("input", "");
      }
    },
    selectedChange(item) {
      this.renderDevice(item);
      if (item.check) {
        this.$emit("input", item.id);
      } else {
        this.$emit("input", "");
      }
    },
  },
  computed: {
    fleetDevice() {
      return this.$store.state.fleetDevice;
    },
  },
  created() {
    if (this.$store.state.fleetDevice.length == 0) {
      let p1 =this.http
        .post(`/Device/Query?page=1&size=19999`)
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            this.$store.commit("setDevice", data.data.data);
          }
        });
      let p2 = this.http
        .post(`/Fleet/Query?page=1&size=19999`)
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            this.$store.commit("setFleet", data.data.data);
          }
        });
      Promise.all([p1, p2]).then(() => {
        this.$store.commit("setfleetDevice");
        this.data = JSON.parse(JSON.stringify(this.$store.state.fleetDevice));
      });
    } else
      this.data = JSON.parse(JSON.stringify(this.$store.state.fleetDevice));
  },
};
</script>

<style>
.device-list {
  height: 100%;
  overflow: auto;
  display: flex;
  flex-direction: column;
  padding: 10px 0 0 10px;
}
.device-list-content {
  flex: 1;
  overflow: auto;
}
.device-list-tools {
  overflow: hidden;
}
.device-list-item {
  margin: 5px 0px;
  height: 30px;
  line-height: 30px;
}
.device-list-item span {
  cursor: pointer;
}
.ivu-btn > span > .ivu-checkbox-wrapper {
  margin: 0px;
}
</style>