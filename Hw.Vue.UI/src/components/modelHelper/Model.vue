<template>
  <div class="hw-page">
    <div class="hw-page-tools">
      <ButtonGroup>
        <Button @click="addClick" size="small" type="primary">{{
          $t("add")
        }}</Button>
        <Button @click="deleteClick" size="small" type="error">{{
          $t("delete")
        }}</Button>

        <div class="ivu-dropdown ivu-group-search">
          <Input size="small" v-model="searchKey" placeholder="" />
          <Button
            icon="ios-search"
            @click="search"
            size="small"
            type="primary"
            >{{ $t("search") }}</Button
          >
        </div>

        <Dropdown
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
      size="small"
      class="hw-page-page"
      :total="total"
      show-sizer
    />

    <Modal
      width="900"
      title="添加"
      :mask-closable="false"
      v-model="modalAdd"
      class-name="vertical-center-modal"
    >
      <add-helper
        v-if="modalAdd"
        :type="modalType"
        :updateData="updateData"
        :model="this.model"
        ref="add"
      ></add-helper>
      <div slot="footer">
        <Button type="text" size="small" @click="addModelCancel">取消</Button>
        <Button
          v-if="modalType == 'add'"
          type="primary"
          size="small"
          @click="addModelOK"
          >确定</Button
        >
        <Button
          v-if="modalType == 'update'"
          type="primary"
          size="small"
          @click="addUpdateModelOK"
          >修改</Button
        >
      </div>
    </Modal>

    <!-- <Modal
      title="修改"
      width="900"
      v-model="modalUpdate"
      class-name="vertical-center-modal"
    >
      <add-helper
        v-if="modalUpdate && addItems.updateItems > 0"
        :items="updateItems"
        :data="updateData"
        ref="update"
      ></add-helper>
      <div slot="footer">
        <Button type="text" size="small" @click="addUpdateModelCancel"
          >取消</Button
        >
        <Button type="primary" size="small" @click="addUpdateModelOK"
          >修改</Button
        >
      </div>
    </Modal> -->
  </div>
</template>

<script>
import ClickOutside from "vue-click-outside";
import AddHelper from "../modelHelper/AddHelper.vue";

export default {
  components: {
    AddHelper,
  },

  directives: {
    ClickOutside,
  },

  methods: {
    search() {
      this.page = 1;
      this.loadData();
    },
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
      this.http.post(`/${this.model}/Delete`, ids).then((data) => {
       
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
      let url = `/${this.model}/Query?page=${this.page}&size=${this.pageSize}`;
      if (that.searchKey) {
        url = `/${this.model}/Search?key=${that.searchKey}&page=${this.page}&size=${this.pageSize}`;
      }

      this.http.post(url).then((data) => {
       
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
    this.http.get(`/${this.model}/GetListDto`).then((data) => {
     
      if (data.data.state == 200) {
        that.columns2.push(...data.data.data);
      }
    });
  },
  data() {
    return {
      model: this.$route.params.model,
      pageSize: 20,
      page: 1,
      total: 0,
      modalType: "",
      modalAdd: false,
      tableHeight: window.innerHeight - 235,
      dropVisible: false,
      updateData: "",
      searchKey: "",
      columns2: [
        {
          type: "selection",
          width: 60,
          align: "center",
          visible: true,
        },
        {
          title: "Action",
          key: "action",
          fixed: "right",
          visible: true,
          width: 150,
          align: "center",
          render: (h, params) => {
            return h("div", [
              h(
                "Button",
                {
                  props: {
                    type: "primary",
                    size: "small",
                  },
                  style: {
                    marginRight: "5px",
                  },
                  on: {
                    click: () => {
                      this.updateData = params.row;
                      this.modalType = "update";
                      this.modalAdd = true;
                    },
                  },
                },
                "编辑"
              ),
              h(
                "Button",
                {
                  props: {
                    type: "error",
                    size: "small",
                  },
                  on: {
                    click: () => {
                      this.del([params.row.id]);
                    },
                  },
                },
                "删除"
              ),
            ]);
          },
        },
      ],
      data3: [],
    };
  },
};
</script>

<style>
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
  background-color: #fff;
  height: calc(100% - 75px);
  display: flex;
  flex-direction: column;
  padding: 10px 10px 0 10px;
  margin: 0 10px 10px 10px;
  box-sizing: border-box;
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
</style>