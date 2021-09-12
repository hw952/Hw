<template>
  <div class="demo-split">
    <Split v-model="split1">
      <div slot="left" class="demo-split-pane demo-split-pane-left">
        <device-list v-model="selected"></device-list>
      </div>
      <div slot="right" class="demo-split-pane main">
        <div class="main-tools">
          <ButtonGroup size="small">
            <Button
              @click="
                liveType = 1;
                split3 = 0;
              "
              :type="liveType == 1 ? 'primary' : 'default'"
            >
              地图
            </Button>
            <Button
              @click="
                liveType = 2;
                split3 = 0.7;
              "
              :type="liveType == 2 ? 'primary' : 'default'"
            >
              视频/地图
            </Button>
          </ButtonGroup>
        </div>
        <div class="main-map-info">
          <Split
            @on-move-end="liveTableMoving"
            v-model="split2"
            mode="vertical"
          >
            <div slot="top" class="demo-split-pane">
              <Split @on-moving="liveMapMoving" v-model="split3">
                <div slot="left" class="demo-split-pane">
                  <video-window></video-window>
                </div>
                <div slot="right" class="demo-split-pane">
                  <baidu-map ref="map"></baidu-map>
                </div>
              </Split>
            </div>
            <div slot="bottom" style="padding: 5px" class="demo-split-pane">
              <Tabs class="bottom-tabs" type="card">
                <TabPane label="告警">
                  <div class="tab-content" ref="alarmTable">
                    <Table
                      :height="bottomTableHeight"
                      size="small"
                      border
                      :columns="alarmColumns"
                      :data="alarmData"
                    ></Table>
                  </div>
                </TabPane>
                <TabPane label="未实现">标签二的内容</TabPane>
                <TabPane label="未实现">标签三的内容</TabPane>
              </Tabs>
            </div>
          </Split>
        </div>
      </div>
    </Split>
  </div>
</template>

<script>
import DeviceList from "./common/DeviceList.vue";
import BaiduMap from "./map/BaiduMap.vue";
import VideoWindow from "./video/VideoWindow.vue";

export default {
  components: { DeviceList, BaiduMap, VideoWindow },
  methods: {
    liveMapMoving() {
      this.liveType = 2;
    },
    liveTableMoving() {
      this.bottomTableHeight = this.$refs.alarmTable.offsetHeight - 50;
    },
  },
  created() {
    let that = this;
    this.$EventBus.$on("test", function (data) {
      if (data.AlarmName) {
        that.alarmData.unshift(data);
      }
    });
  },
  watch: {
    selected(val) {
      console.log(val);
      let that = this;
      this.http
        .get(`/DeviceState/LastDeviceState?deviceNo=${val}`)
        .then((data) => {
          console.log(data.data);
          if (data.data.state == 200) {
            //latitude: 22.5583115
            // longitude: 113.946625
            that.$refs.map.addDevice(
              data.data.data.longitude,
              data.data.data.latitude,
              val
            );
          }
        });
    },
  },

  data() {
    return {
      split1: 0.2,
      split2: 0.7,
      selected: "",
      liveType: 2,
      split3: 0.7,
      bottomTableHeight: 200,
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
    };
  },
};
</script>

<style>
.demo-split {
  height: 100%;
}
.demo-split-pane {
  height: 100%;
}

.main {
  display: flex;
  flex-direction: column;
}
.main-tools {
  height: 40px;
  margin: 0;
  padding: 0 10px;
  height: 40px;

  line-height: 40px;
  box-sizing: border-box;
}
.main-map-info {
  flex: 1;
}
.bottom-tabs {
  height: 100%;
}
.ivu-tabs-content,
.ivu-tabs-content > div,
.tab-content {
  height: 100%;
}
</style>