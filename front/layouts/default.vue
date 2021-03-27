<template>
  <v-app>
    <notification />
    <navbar />
    <v-container
      fluid
      tag="main"
      class="default-layout"
    >
      <Nuxt />
    </v-container>
  </v-app>
</template>
<style lang="scss" scoped>
.default-layout{
  margin-top: 100px;
}
</style>
<script>
import navbar from '~/components/Navbar'
import notification from '~/components/Notification'

export default {
  components: {
    navbar,
    notification
  }
}
</script>
