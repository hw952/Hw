<template>
  <div class="demo-split">
    <Split v-model="split1">
      <div slot="left" class="demo-split-pane">
        <device-list v-model="selected"></device-list>
      </div>
      <div slot="right" class="demo-split-pane">
        <div class="hw-page">
          <div class="hw-page-tools">
            <ButtonGroup>
            
              <div class="ivu-dropdown ivu-group-search">              
                <Button @click="loadData" icon="ios-search" size="small" type="primary">{{
                  $t("search")
                }}</Button>
              </div>

              <Dropdown
                v-click-outside="dropClick2"
                ref="box"
                :visible="dropVisible"
                trigger="custom"
              >
                <Button
                  @click.stop.prevent="dropClick"
                  size="small"
                  type="primary"
                >
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
              </Dropdown>
            </ButtonGroup>
          </div>

          <Table
            row-key="id"
            border
            size="small"
            ref="table"
            :height="tableHeight"
            :columns="getColumns2"
            :data="data3"
          >
          </Table>

          <Page
            @on-change="onPageChange"
            @on-page-size-change="onPageSizeChange"
            :page-size="pageSize"
            :updateData="updateData"
            size="small"
            class="hw-page-page"
            :total="total"
            show-sizer
          />

        </div>
      </div>
    </Split>
  </div>
</template>

<script>
import ClickOutside from "vue-click-outside";
import DeviceList from "../common/DeviceList.vue";

export default {
  components: {  
    DeviceList,
  },

  directives: {
    ClickOutside,
  },

  methods: {
    onPageSizeChange(pageSize) {
      this.pageSize = pageSize;
      this.loadData();
    },
    onPageChange(page) {
      this.page = page;
      this.loadData();
    },
    addUpdateModelOK() {
      this.updateData = Object.assign(
        this.updateData,
        this.$refs.add.formValidate
      );
      if (JSON.stringify(this.$refs.add.validate())) {
        this.http
          .post(`/${this.model}/Update`, this.updateData)
          .then((data) => {
            console.log(data.data);
            if (data.data.state == 200) {
              this.$refs.add.resetFields();
              this.modalAdd = false;
              this.loadData();
            }
          });
      }
    },

    addModelOK() {
      if (JSON.stringify(this.$refs.add.validate())) {
       this.http
          .post(`/${this.model}/Add`, this.$refs.add.formValidate)
          .then((data) => {
            console.log(data.data);
            if (data.data.state == 200) {
              this.$refs.add.resetFields();
              this.modalAdd = false;
              this.loadData();
            }
          });
      }
    },
    del(ids) {
      let that = this;
      this.http
        .post(`/${this.model}/Delete`, ids)
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            that.$Message.info("ok");
            that.loadData();
          }
        });
    },
    deleteClick() {
      let that = this;
      let selectedList = this.$refs.table.getSelection();
      if (selectedList.length == 0) {
        this.$Message.info("请至少选择一行数据");
        return;
      }
      let ids = [];
      for (var i = 0; i < selectedList.length; i++) {
        ids.push(selectedList[i].id);
      }
      that.del(ids);
    },

    addModelCancel() {
      this.$refs.add.resetFields();
      this.modalAdd = false;
    },
    addClick() {
      this.modalType = "add";
      this.modalAdd = true;
    },
    dropClick() {
      this.dropVisible = true;
    },
    dropClick2() {
      this.dropVisible = false;
    },    
    loadData() {
      let that = this;
      if(this.selected!='')
      this.http
        .post(`/Report/Query${this.model}?deviceno=${this.selected}&page=${this.page}&size=${this.pageSize}`)
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            that.data3 = data.data.data;
            that.total = data.data.dataCount;
          }
        });
    },
  },
  computed: {
    getColumns2() {
      return this.columns2.filter((d) => d.visible);
    },
    getColumns2Count() {
      return this.columns2.filter((d) => d.visible).length > 1 ? false : true;
    },
  },
  created() {
    let that = this;
    that.loadData();
    this.http.get(`/report/GetListDto?type=${this.model}`).then((data) => {
      console.log(data.data);
      if (data.data.state == 200) {
        that.columns2.push(...data.data.data);
      }
    });
  },
  data() {
    return {
      selected: "",
      split1: 0.2,
      model: this.$route.params.model,
      pageSize: 20,
      page: 1,
      total: 0,
      modalType: "",
      modalAdd: false,
      tableHeight: window.innerHeight - 250,
      dropVisible: false,
      updateData: "",
      columns2: [
        {
          type: "selection",
          width: 60,
          align: "center",
          visible: true,
        },       
      ],
      data3: [],
    };
  },
};
</script>

<style scoped>
.vertical-center-modal {
  display: flex;
  align-items: center;
  justify-content: center;
}
.vertical-center-modal .ivu-modal {
  top: -50px;
}
.ivu-btn-group .ivu-group-search {
  display: inline-flex;
}
.ivu-btn-group .ivu-group-search button {
  border-radius: 0;
}
.ivu-btn-group .ivu-group-search .ivu-input-small {
  border-radius: 0px;
}
.ivu-btn-group .ivu-dropdown {
  vertical-align: top;
}
.ivu-btn-group .ivu-dropdown:not(:first-child):not(:last-child) {
  border-radius: 0;
}
.hw-page-tools .ivu-checkbox-wrapper {
  width: 100%;
}
.ivu-btn-group .ivu-dropdown:last-child button {
  border-top-left-radius: 0;
  border-bottom-left-radius: 0;
}
.hw-page {
  height: 100%;
  display: flex;
  flex-direction: column;
}
.hw-page-header {
  height: 42px;
  box-sizing: border-box;
}
.hw-page-tools {
  height: 42px;

  box-sizing: border-box;
}
.hw-page-table {
  flex: 1;
}
.hw-page-page {
  height: 42px;
  line-height: 42px;
  box-sizing: border-box;
}
.ivu-table-cell-tree {
  width: 14px !important;
  height: 14px !important;
  vertical-align: text-top !important;
}
.demo-split {
  height: 100%;
}
.demo-split-pane {
  padding: 10px;
  height: 100%;
  overflow: auto;
}
</style>