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
<style lang="scss" scoped>
.admin-layout{
  margin-top: 100px;
}
</style>
<script>
import componentSideMenu from '~/components/admin/SideMenu'
import componentTopBar from '~/components/admin/TopBar'
import notification from '~/components/Notification'

export default {
  // middleware: ['connected', 'admin'],
  components: {
    componentSideMenu,
    componentTopBar,
    notification
  }
}
</script>
