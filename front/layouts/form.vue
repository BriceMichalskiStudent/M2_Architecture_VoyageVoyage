<template>
  <v-app>
    <notification />
    <v-container
      fluid
      tag="main"
      class="form-layout"
    >
      <div class="form-container col-md-8 offset-md-2">
        <nuxt-link to="/" class="to-home">
          <span class="icon-clear" />
        </nuxt-link>
        <Nuxt />
      </div>
    </v-container>
  </v-app>
</template>
<script>
import notification from '~/components/Notification'

export default {
  components: {
    notification
  }
}
</script>
<style lang="scss" scoped>
/*Problème css order. Impossible to override vutify, this is the only way : (https://stackoverflow.com/questions/56665934/nuxt-vuetify-how-to-control-the-order-in-which-css-files-are-loaded) */
@import "assets/vutify-override.css";

.form-layout{
  background-color: $primary;
  padding: 0;
  position: relative;
  height: 100vh;
  overflow: hidden;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    height: auto;
    overflow: hidden;
    background-color: white;
  }
  &::after{
    overflow: hidden;
    z-index: 0;
    content: '';
    width: 70%;
    height: 70vh;
    position: absolute;
    top: calc( 50% - 35vh );
    left: 15%;
    background-color: rgba(255, 255, 255, 0.65);
    opacity: 35%;
    border-radius: 240px;
    transform: rotate(17deg);
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
      display: none;
    }
  }
  &::before{
    overflow: hidden;
    z-index: 0;
    content: '';
    width: 66%;
    height: 70vh;
    position: absolute;
    top: calc( 50% - 35vh );
    left: 17%;
    background-color: rgba(255, 255, 255, 0.65);
    opacity: 35%;
    border-radius: 75px;
    transform: rotate(-5deg);
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
      display: none;
    }
  }
}

.form-container {
  z-index: 1;
  display: flex;
  align-items: center;
  margin: 15vh auto;
  height: 70vh;
  background-color: white;
  border-radius: 10px;
  flex-direction: column;
  position: relative;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    border-radius: 0;
    margin: 0;
    height: 100vh;
  }

  .to-home {
    position: absolute;
    top: 20px;
    right: 40px;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      top: 25px;
      right: 10%;
    }
    span {
      font-size: 30px;
    }
  }
}
</style>
