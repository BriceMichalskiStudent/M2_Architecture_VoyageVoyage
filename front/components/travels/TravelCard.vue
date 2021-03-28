<template>
  <nuxt-link :to="{ path: '/travels/' + travel.id }">
    <section class="travelCard">
      <div class="travelCard__header">
        <img v-if="travel.author.image.url === '' || travel.author.image.url === undefined " class="travel-image" src="/img/placeholder-profile.png">
        <img v-else :src="travel.author.image.url">
        <div class="travelCard__headerInfo">
          <a href="https://twitter.com/adhamdannaway">{{ travel.author.name }}</a>
          <p>{{ travel.place.city }}, {{ travel.place.country }}</p>
        </div>
      </div>
      <div class="travelCard__image">
        <img v-if="travel.images[0] === '' || travel.images[0] === undefined " class="travel-image" src="/img/travel-placeholder.jpg">
        <img v-else :src="travel.images[0].url">
      </div>
      <div class="travelCard__content">
        <a class="icon-heart" />
        <a class="icon-message-square" />
        <p class="marginReset">
          <a href="#">{{ travel.nbLike }} J'aime</a>
        </p>
        <p>
          {{ travel.description }}
        </p>
      </div>
    </section>
  </nuxt-link>
</template>
<script>
import oneTravel from '~/assets/dataSchema/oneTravel.json'

export default {
  // props: {
  //   travel: {
  //     type: Object,
  //     default () {
  //     }
  //   }
  // },
  data () {
    return {
      travel: {}
    }
  },
  created () {
    this.travel = oneTravel
  }
}
</script>
<style scoped lang="scss">
.travelCard{
  width: 33%;
  border: 1px solid #ccc;
  background: #fff;
  border-radius: 4px;
  &__header{
    padding: 15px;
    display: flex;
    align-items: center;
    img {
      width: 40px;
      height: 40px;
      border-radius: 20px;
      margin-right: 15px;
    }
    &Info {
      p{
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
  &__image{
    img{
      width: 100%;
    }
  }
  &__content{
    padding: 5px 10px;
    *[class^="icon-"]{
      font-size: 20px;
      line-height: 30px;
      margin: 10px 2px;
      font-weight: unset;
    }
    a{
      color: #000;
      text-decoration: none;
      font-weight: bold;
    }
  }
}
.marginReset{
  margin: 0;
}
</style>
