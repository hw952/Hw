<template>
  <Form
    ref="formValidate"
    :model="formValidate"
    :rules="ruleValidate"
    :label-width="80"
  >
    <template v-for="len in Math.ceil(items.length / 2)">
      <Row :key="len">
        <template v-for="item in items.slice((len - 1) * 2, len * 2)">
          <Col :key="item.name" span="12">
            <FormItem :label="item.lable" :prop="item.name">
              <template v-if="item.type == 'text'">
                <Input v-model="formValidate[item.filed]" />
              </template>

              <template v-if="item.type == 'selectIcon'">
                <icon-select v-model="formValidate[item.filed]"></icon-select>
              </template>

              <template v-if="item.type == 'password'">
                <Input type="password" v-model="formValidate[item.filed]" />
              </template>

              <i-switch
                v-model="formValidate[item.filed]"
                v-if="item.type == 'switch'"
              ></i-switch>

              <template v-if="item.type == 'date'">
                <DatePicker
                  v-model="formValidate[item.filed]"
                  type="date"
                ></DatePicker>
              </template>

              <template v-if="item.type == 'treeCommbox'">
                <template v-if="commbooxData[item.filed]">
                  <select-tree
                    class="tree-select"
                    v-model="formValidate[item.filed]"
                    :treeData="commbooxData[item.filed]"
                  ></select-tree>
                </template>
              </template>

              <template v-if="item.type == 'treeMultipleCommbox'">
                <template v-if="commbooxData[item.filed]">
                  <select-tree
                    class="tree-select"
                    v-model="formValidate[item.filed]"
                    :treeData="commbooxData[item.filed]"
                    :multiple="true"
                  ></select-tree>
                </template>
              </template>

              <template v-if="item.type == 'emunMultipleCommbox'">
                <Select multiple v-model="formValidate[item.filed]">
                  <template v-if="commbooxData[item.filed]">
                    <Option
                      v-for="comboxItem in commbooxData[item.filed]"
                      :value="comboxItem.id"
                      :key="comboxItem.id"
                      >{{ comboxItem.name }}</Option
                    >
                  </template>
                </Select>
              </template>

              <template v-if="item.type == 'emunCommbox'">
                <Select v-model="formValidate[item.filed]">
                  <template v-if="commbooxData[item.filed]">
                    <Option
                      v-for="comboxItem in commbooxData[item.filed]"
                      :value="comboxItem.id"
                      :key="comboxItem.id"
                      >{{ comboxItem.name }}</Option
                    >
                  </template>
                </Select>
              </template>
            </FormItem>
          </Col>
        </template>
      </Row>
    </template>
  </Form>
</template>

<script>
import IconSelect from "../common/IconSelect.vue";
import SelectTree from "../common/SelectTree.vue";

export default {
  components: { IconSelect, SelectTree },
  data() {
    // let formValidate = {};

    // this.items.forEach((item) => {
    //   if (this.data == "") formValidate[item.filed] = item.default;
    //   else formValidate[item.filed] = this.data[item.filed];
    // });

    return {
      data:{},
      commbooxData: {},
      formValidate: {},
      items: [],
      ruleValidate: {
        name: [
          {
            required: true,
            message: "The name cannot be empty",
            trigger: "blur",
          },
        ],
        mail: [
          {
            required: true,
            message: "Mailbox cannot be empty",
            trigger: "blur",
          },
          { type: "email", message: "Incorrect email format", trigger: "blur" },
        ],
        city: [
          {
            required: true,
            message: "Please select the city",
            trigger: "change",
          },
        ],
        gender: [
          {
            required: true,
            message: "Please select gender",
            trigger: "change",
          },
        ],
        interest: [
          {
            required: true,
            type: "array",
            min: 1,
            message: "Choose at least one hobby",
            trigger: "change",
          },
          {
            type: "array",
            max: 2,
            message: "Choose two hobbies at best",
            trigger: "change",
          },
        ],
        date: [
          {
            required: true,
            type: "date",
            message: "Please select the date",
            trigger: "change",
          },
        ],
        time: [
          {
            required: true,
            type: "string",
            message: "Please select time",
            trigger: "change",
          },
        ],
        desc: [
          {
            required: true,
            message: "Please enter a personal introduction",
            trigger: "blur",
          },
          {
            type: "string",
            min: 20,
            message: "Introduce no less than 20 words",
            trigger: "blur",
          },
        ],
      },
    };
  },

  props: ["type", "model", "updateData"],
  created() {
    if (this.type == "add") {
      this.http.get(`/${this.model}/GetAddDto`).then((data) => {
        if (data.data.state == 200) {
          this.items = data.data.data.filter((d) => d.filed !== "id");
        }
      });
    } else if (this.type == "update") {
      this.http
        .get(`/${this.model}/GetUpdate?id=${this.updateData.id}`)
        .then((data) => {
          if (data.data.state == 200) {

            this.data = JSON.parse(JSON.stringify(data.data.data));

            this.http.get(`/${this.model}/GetUpdateDto`).then((data) => {
              if (data.data.state == 200) {
                this.items = JSON.parse(
                  JSON.stringify(data.data.data.filter((d) => d.filed !== "id"))
                );
              }
            });

          }
        });
    }
  },
  methods: {
    validate() {
      return this.$refs.formValidate.validate();
    },
    resetFields() {
      for (var key in this.formValidate) {
        let item = this.items.find((d) => d.filed == key);
        this.formValidate[key] = item.default;
      }
      this.data = "";
      return this.$refs.formValidate.resetFields();
    },
  },
  watch: {
    data(newVal) {
      if (newVal == "") return;
      for (let index = 0; index < this.items.length; index++) {
        const item = this.items[index];
        //this.formValidate[item.filed]=newVal[item.filed]
        this.$set(this.formValidate, item.filed, newVal[item.filed]);
      }
    },
    items(newVal) {
      let that = this;

      for (let index = 0; index < newVal.length; index++) {
        const item = newVal[index];

        if (this.type == "add") {
          this.$set(this.formValidate, item.filed, item.default);
        }
        //当前是修改时，获取具体的值这里是附和下拉树多选框
        //目前只有角色菜单修改时用的到
        // else if (item.valueUrl && this.type == "update") {
        //   that.$root.$options.axios
        //     .post(`${item.valueUrl}?id=${this.updateData["id"]}`)
        //     .then((data) => {
        //       if (data.data.state == 200) {
        //         that.$set(that.formValidate, item.filed, data.data.data);
        //       }
        //     });
        // }
        else if (this.type == "update") {
          if (!this.data) {
            this.$set(this.formValidate, item.filed, item.default);
          } else
            this.$set(
              this.formValidate,
              item.filed,
              this.data[item.filed]
            );
        }

        //当前是请求下拉框的的具体值
        if (item.url) {
          that.$set(that.commbooxData, item.filed, []);
          this.http.post(item.url).then((data) => {
            if (data.data.state == 200) {
              //that.commbooxData[item.filed] = data.data.data;
              item.data = data.data.data;
              that.$set(that.commbooxData, item.filed, data.data.data);
              if (item.valueUrl && this.type == "update") {
                this.http
                  .get(`${item.valueUrl}?id=${this.updateData["id"]}`)
                  .then((d2) => {
                    if (d2.data.state == 200) {
                      that.$set(that.formValidate, item.filed, d2.data.data);
                      that.$set(
                        that.commbooxData,
                        item.filed,
                        JSON.parse(JSON.stringify(data.data.data))
                      );
                    }
                  });
              }
            }
          });
        }
      }
    },
  },
};
</script>

<style>
.tree-select .ivu-select-selected-value {
  padding-left: 0 !important;
}
</style>