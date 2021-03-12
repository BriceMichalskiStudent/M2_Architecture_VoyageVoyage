<template>
  <section class="info-bar">
    <p class="big">
      <span class="icon-calendar" /> {{ date }}
    </p><p class="big">
      <span class="icon-map-pin" /> {{ place }}
    </p>
    <p class="big">
      <span class="icon-tag" /> {{ tag }}
    </p>
  </section>
</template>
<script>
export default {
  props: {
    date: {
      type: String,
      default: '01 / 10 / 2020'
    },
    place: {
      type: String,
      default: 'Villeurbanne'
    },
    tag: {
      type: String,
      default: 'No tag'
    }
  }
}

</script>
<style lang="scss" scoped>
.info-bar{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    flex-direction: column;
  }
}
span{
  font-size: 20px;
  line-height: 30px;
  padding: 0 10px;
  color: $primary;
  vertical-align: bottom;
}
</style>
