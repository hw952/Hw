

<template>
  <div class="track-main">
    <baidu-map ref="map"></baidu-map>
    <div class="track-search">
      <Card style="width: 350px">
        <Row>
          <Col span="24">
            <div class="track-search-device">
              <device-list v-model="selectDeviceId"></device-list>
            </div>
          </Col>
        </Row>
        <Row>
          <Col span="8"><div class="ivu-row-lable">日期</div> </Col>
          <Col span="16"
            ><DatePicker
              v-model="selectDate"
              style="width: 100%"
              class="index-time"
              format="yyyy-MM-dd"
              type="date"
            ></DatePicker
          ></Col>
        </Row>

        <Row>
          <Col span="8"><div class="ivu-row-lable">时间</div> </Col>
          <Col span="16">
            <TimePicker
              v-model="selectTime"
              style="width: 100%"
              :value="value2"
              format="HH:mm:ss"
              type="timerange"
              placement="bottom-end"
              placeholder="Select time"
            ></TimePicker>
          </Col>
        </Row>
        <Row>
          <Col span="24">
            <Button @click="search" type="primary" long>搜索</Button></Col
          >
        </Row>
      </Card>
    </div>
    <div class="track-detail">
      <Card style="width: 100%">
        <Tabs value="name1">
          <TabPane label="轨迹详情" name="name1">
            <Table
              :height="bottomTableHeight"
              size="small"
              border
              :columns="alarmColumns"
              :data="alarmData"
            ></Table
          ></TabPane>
          <TabPane label="速度" name="name2">
            <div
              class="echart"
              id="echart-line"
              :style="{ float: 'left', width: '100%', height: '220px' }"
            ></div>
          </TabPane>
        </Tabs>
      </Card>
    </div>
  </div>
</template>

<script>
import DeviceList from "./common/DeviceList.vue";
import BaiduMap from "./map/BaiduMap.vue";
import { dateFormat } from "./common/common";
import * as echarts from "echarts";

export default {
  data() {
    return {
      alarmColumns: [
        {
          title: "设备ID",
          key: "deviceID",
        },
        {
          title: "设备名称",
          key: "deviceName",
        },
        {
          title: "时间",
          key: "dtu",
        },
        {
          title: "告警名称",
          key: "AlarmName",
        },
        {
          title: "经度",
          key: "longitude",
        },
        {
          title: "维度",
          key: "latitude",
        },
      ],
      alarmData: [],
      selectDate: new Date(),
      selectTime: ["00:00:00", "23:59:59"],
      selectDeviceId: "",
      bottomTableHeight: 220,
      option: {},
    };
  },
  created() {},

  methods: {
    search() {
      const { selectDate, selectTime, selectDeviceId } = this;
      if (!selectDeviceId) {
        return;
      }
      let that = this;
      this.axios
        .get(
          `/DeviceState/DeviceStateTrack?deviceNo=${selectDeviceId}&begin=${
            dateFormat(selectDate, "yyyy-MM-dd") + " " + selectTime[0]
          }&end=${dateFormat(selectDate, "yyyy-MM-dd") + " " + selectTime[1]}`
        )
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            that.alarmData = data.data.data;
            that.$refs.map.drawDeviceTrack(that.alarmData);

            let { name, xData, yData } = {
              name: "",
              xData: that.alarmData.map((d) => d.dtu),
              yData: that.alarmData.map((d) => d.speed),
            };
            this.initChart(name, xData, yData);
          }
        });
    },
    initChart(name, xData, yData) {
      let getchart = echarts.init(document.getElementById("echart-line"));
      this.option = {
        tooltip: {
          trigger: "axis",
        },
        legend: {
          data: [name],
          bottom: "0%",
        },
        grid: {
          top: "10%",
          left: "3%",
          right: "8%",
          bottom: "11%",
          containLabel: true,
        },

        xAxis: {
          type: "category",
          boundaryGap: false,
          data: xData,
        },
        yAxis: {
          type: "value",
        },
        series: [
          {
            name: name,
            type: "line",
            stack: "总量",
            data: yData,
          },
        ],
      };

      getchart.setOption(this.option);
      //随着屏幕大小调节图表
      window.addEventListener("resize", () => {
        getchart.resize();
      });
    },
  },

  components: { BaiduMap, DeviceList },
};
</script>
    

<style scoped>
.ivu-row-lable {
  line-height: 32px;
}
.ivu-row {
  padding: 5px 0;
}
.track-main {
  width: 100%;
  height: 100%;
}
.track-search {
  position: absolute;
  top: 100px;
  bottom: 60px;
  width: 350px;
}
.track-detail {
  position: absolute;
  bottom: 0;
  right: 60px;
  left: 410px;
  height: 300px;
}
.ivu-card,
.ivu-card-body {
  height: 100%;
}
.track-search-device {
  height: 300px;
}
</style>
