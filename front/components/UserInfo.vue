<template>
  <div class="userInfo">
    <img v-if="author.image.url === '' || author.image.url === undefined " class="travel-image" src="/img/placeholder-profile.png">
    <img v-else :src="author.image.url">
    <div class="travelCard__headerInfo">
      <a href="https://twitter.com/adhamdannaway">{{ author.name }}</a>
      <p>{{ place.city }}, {{ place.country }}</p>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    author: {
      type: Object,
      default () {}
    },
    place: {
      type: Object,
      default () {}
    }
  }
}
</script>
<style scoped lang="scss">
.userInfo {
  padding: 10px;
  display: flex;
  align-items: center;

  img {
    width: 40px;
    height: 40px;
    border-radius: 20px;
    margin-right: 15px;
  }

  &Info {
    p {
      margin: 0;
    }

    a {
      font-size: 12px;
      font-weight: bold;
      text-decoration: none;
      color: #000;
    }
  }
}
</style>
