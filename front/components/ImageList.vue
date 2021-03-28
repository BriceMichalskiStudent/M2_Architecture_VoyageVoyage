<template>
  <section class="imageList">
    <div v-for="image in images" :key="image.id" class="imageList__container">
      <img :src="image.url">
    </div>
  </section>
</template>
<script>
export default {
  props: {
    images: {
      type: Array,
      default () {
        return []
      }
    }
  }
}
</script>
<style scoped lang="scss">
.imageList{
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  margin-bottom: 40px;
  &__container{
    padding: 10px;
    width: calc(50% - 20px);
    height: 200px;
    img{
      width: 100%;
      margin: 0;
      height: calc( 200px - 20px);
      object-fit: cover;
    }
    &:first-of-type{
      width: 100%;
      height: 300px;
      img{
        height: calc(300px - 20px);
      }
    }
  }
}

</style>
