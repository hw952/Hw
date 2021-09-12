<template>
  <div CLASS="container">
    <div>
      <img src="../assets/logo.png" />
      <Input
        prefix="ios-contact"
        placeholder="Enter name"
        style="width: auto"
      />
      <Input
        v-model="value18"
        type="password"
        password
        placeholder="Enter password..."
        style="width: auto"
      />    

      <Button @click="login" type="primary">登录</Button>
    </div>
  </div>
</template>

<script>

export default {

  methods: {
    login() {
      this.$router.push("/default");
    },
  },
};
</script>

<style scoped>
.container {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.container > div {
  display: flex;
  flex-direction: column;
}
.container > div > * {
  margin: 5px;
}
</style>
