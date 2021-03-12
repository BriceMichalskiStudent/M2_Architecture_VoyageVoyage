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
<style>
/* Problème css order. Impossible to override vutify, this is the only way : (https://stackoverflow.com/questions/56665934/nuxt-vuetify-how-to-control-the-order-in-which-css-files-are-loaded) */
@import "assets/vutify-override.css";

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
