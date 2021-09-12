<template>
  <baidu-map
    id="allmap"
    @ready="mapReady"
    :scroll-wheel-zoom="true"
    class="map"
  >
  </baidu-map>
</template>

<script>
export default {
  data() {
    return {
      point: "",
      map: {},
      curDevice: "",
      curDeviceId: "",
      BMap: {},
      DeviceTrack:"",
    };
  },
  created() {
    let that = this;
    this.$EventBus.$on("test", function (data) {
      if (data.deviceID == that.curDeviceId) {
        var point = new that.BMap.Point(data.longitude, data.latitude);
        if (that.curDevice) {
          that.curDevice.setPosition(point);
          that.map.setCenter(point);
        }
      }
    });
  },
  methods: {
    mapReady({ BMap, map }) {
      // 选择一个经纬度作为中心点
      this.BMap = BMap;
      this.point = new BMap.Point(113.27, 23.13);
      map.centerAndZoom(this.point, 12);
      this.map = map;
    },
    addDevice(lng, lat, deviceID) {
      this.deviceID = deviceID;
      var point = new this.BMap.Point(lng, lat);
      var marker = new this.BMap.Marker(point);
      if (this.curDevice) {
        this.map.clearOverlays(this.curDevice);
      }
      this.curDevice = marker;
      this.map.addOverlay(marker);
      this.map.setCenter(point);
    },
    drawDeviceTrack(track) {
      let that = this;
      if(that.DeviceTrack)
       this.map.addOverlay(that.DeviceTrack);
      let points = track.map((d) => {
        return new that.BMap.Point(d.longitude, d.latitude);
      });
      var polyline = new that.BMap.Polyline(points, {
        strokeColor: "blue",
        strokeWeight: 2,
        strokeOpacity: 0.5,
      });
      that.DeviceTrack=polyline;
      this.map.addOverlay(polyline);
    },
  },
};
</script>

<style>
.map {
  width: 100%;
  height: 100%;
}
</style>