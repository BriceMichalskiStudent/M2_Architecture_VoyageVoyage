<template>
  <nuxt-link :to="{ path: '/travels/' + travel.id }">
    <section class="travelCard">
      <UserInfo :author="travel.author" :place="travel.place" />
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
import UserInfo from '~/components/UserInfo'

export default {
  comments: { UserInfo },
  props: {
    travel: {
      type: Object,
      default () {
      }
    }
  }
}
</script>
<style scoped lang="scss">
.travelCard {
  width: 100%;
  &__image {
    width: 100%;
    height: 200px;
    img {
      width: 100%;
      height: inherit;
      object-fit: cover;
    }
  }

  &__content {
    padding: 5px 10px;

    *[class^="icon-"] {
      font-size: 20px;
      line-height: 30px;
      margin: 10px 2px;
      font-weight: unset;
    }

    a {
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
