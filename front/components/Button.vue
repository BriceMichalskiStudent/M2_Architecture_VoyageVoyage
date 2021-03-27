<template>
  <nuxt-link v-if="type === null" :to="link" :class="[custom, 'button']">
    {{ anchor }}
  </nuxt-link>
  <button v-else :class="[custom, 'button']" :type="[type]">
    {{ anchor }}
  </button>
</template>

<script>
export default {
  props: {
    link: {
      type: String,
      default: '/'
    },
    anchor: {
      type: String,
      default: 'Home'
    },
    type: {
      type: String,
      default: null
    },
    custom: {
      type: String,
      default: 'default'
    }
  }
}
</script>
<style lang="scss" scoped>
.button{
  z-index: 2;
  padding: 10px 30px;
  background-color: $secondary!important;
  color: white;
  border-radius: 2px;
  position: relative;
  text-align: center;
  &::after{
    content: '';
    width: 100%;
    height: 100%;
    position: absolute;
    top:0;
    left: 0;
    background-color: $secondary!important;;
    opacity: 0.35;
    z-index: -1;
    border-radius: 2px;
    transform: rotate(0);
    transition-duration: 0.2s;
    transition-timing-function: ease-in-out;
  }
  &:hover{
    &::after{
      transform: rotate(5deg);
    }
  }
}
.primary{
  background-color: $primary!important;
  &::after{
    background-color: $primary!important;;
  }
}
.large{
  padding: 10px 10%;
}
</style>
