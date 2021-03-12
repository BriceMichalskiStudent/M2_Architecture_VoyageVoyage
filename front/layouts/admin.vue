<template>
  <v-app>
    <notification />
    <component-side-menu />
    <component-top-bar />
    <v-container
      fluid
      tag="main"
      class="admin-layout"
    >
      <Nuxt />
    </v-container>
  </v-app>
</template>
<script>
import componentSideMenu from '~/components/admin/SideMenu'
import componentTopBar from '~/components/admin/TopBar'
import notification from '~/components/Notification'

export default {
  middleware: ['connected', 'admin'],
  components: {
    componentSideMenu,
    componentTopBar,
    notification
  }
}
</script>
<style>
/* Problème css order. Impossible to override vutify, this is the only way : (https://stackoverflow.com/questions/56665934/nuxt-vuetify-how-to-control-the-order-in-which-css-files-are-loaded) */
@import "assets/vutify-override.css";

</style>
