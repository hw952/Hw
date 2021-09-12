<template>
  <div class="layout">
    <Layout :style="{ heght: '100%' }">
      <Header :style="{ position: 'fixed', width: '100%' }">
        <Menu mode="horizontal" theme="dark" active-name="1">
          <div class="layout-logo">
              <img style="height: 30px;vertical-align: top;" src="../assets/logo.png">
          </div>
          <div class="layout-nav">
            <MenuItem to="/home" name="1">
              <Icon type="ios-navigate"></Icon>
              实时
            </MenuItem>
            <MenuItem to="/track" name="2">
              <Icon type="ios-keypad"></Icon>
              轨迹
            </MenuItem>
            <MenuItem to="/system" name="3">
              <Icon type="ios-analytics"></Icon>
              {{ $t("system") }}
            </MenuItem>
            <MenuItem to="/management" name="4">
              <Icon type="ios-paper"></Icon>
              {{ $t("management") }}
            </MenuItem>
          </div>
        </Menu>
      </Header>
      <Content
        :style="{ margin: '64px 0 0', background: '#fff', minHeight: '500px' }"
      >
        <keep-alive>
          <router-view></router-view>
        </keep-alive>
      </Content>
    </Layout>
  </div>
</template>

<script>
export default {
  created() {
    let that = this;
    var i = 0;
    var ws = new WebSocket(that.global.getWsUrl());
    ws.onopen = function () {
      //当WebSocket创建成功时，触发onopen事件
      console.log("open");
      ws.send("hello"); //将消息发送到服务端
    };
    ws.onmessage = function (e) {
      //当客户端收到服务端发来的消息时，触发onmessage事件，参数e.data包含server传递过来的数据
      console.log(e.data);
      i++;
      console.log(i);
      try {
        that.$EventBus.$emit("test", JSON.parse(e.data));
      } catch(e) {console.log(e)}
    };
    ws.onclose = function () {
      //当客户端收到服务端发送的关闭连接请求时，触发onclose事件
      console.log("close");
    };
    ws.onerror = function (e) {
      //如果出现连接、处理、接收、发送数据失败的时候触发onerror事件
      console.log(e);
    };
  },
};
</script>

<style scoped>
.layout {
  background: #f5f7f9;
  position: relative;
  border-radius: 4px;
  overflow: hidden;
  height: 100vh;
}
.ivu-layout {
  height: 100%;
}
.layout-logo {


    margin-right: 30px;
  height: 30px;

  float: left;
  position: relative;
  top: 15px;
  left: 20px;
}
.layout-nav {
  margin-right: 20px;
  margin: 0 auto;
  margin-left: 20px;
}
.layout-footer-center {
  text-align: center;
}
</style>
