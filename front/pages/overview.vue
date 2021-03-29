<template>
  <div class="col-md-10 offset-md-1 container">
    <section class="overview">
      <br>
      <Bird class="svg-bird" />
      <h1 class="overview__title">
        Fil d'actualité
        <Triangle class="svg-triangle" />
        <Sun class="svg-sun" />
      </h1>
    </section>
    <!--    <p v-if="$fetchState.pending">-->
    <!--      Récupération en cours...️-->
    <!--    </p>-->
    <!--    <p v-else-if="$fetchState.error">-->
    <!--      Une erreur est survenue :(-->
    <!--    </p>-->
    <section class="overview-content col-md-10 offset-md-1">
      <h2>Les derniers voyages !</h2>
      <travel-list :travels="travelAll" />
    </section>
  </div>
</template>

<script>
import listTravel from '~/assets/dataSchema/listTravel.json'

import TravelList from '~/components/travels/TravelList'
import Triangle from '~/components/svg/Triangle'
import Sun from '~/components/svg/Sun'
import Bird from '~/components/svg/Bird'

export default {
  components: { TravelList, Sun, Triangle, Bird },
  transition: 'opacity',
  // async fetch () {
  //   await this.$axios.get('/event')
  //     .then(response => (this.eventsAll = response.data))
  //
  //   let tag = null
  //   let eventsEmpty = true
  //   while (eventsEmpty === true) {
  //     await this.$axios.get('/tag')
  //       .then((response) => {
  //         const rand = Math.floor(Math.random() * response.data.length)
  //         tag = response.data[rand]
  //         this.tag = response.data[rand]
  //       })
  //
  //     await this.$axios.get('/event',
  //       {
  //         params:
  //           { q: { tags: tag } }
  //       })
  //       .then((response) => {
  //         if (response.data.length !== 0) {
  //           eventsEmpty = false
  //           this.eventsTag = response.data
  //         }
  //       })
  //   }
  // },
  data () {
    return {
      title: 'Page voyages',
      meta_desc: 'Je suis le magnifique content',
      travelAll: []
    }
  },
  created () {
    this.travelAll = listTravel
  },
  head () {
    return {
      title: this.title,
      meta: [
        { hid: 'description', name: 'description', content: this.meta_desc }
      ]
    }
  }
}
</script>

<style scoped lang="scss">
.overview{
  position: relative;
  z-index: 0;
  .svg-bird{
    z-index: -1;
    position: absolute;
    right: 50px;
    top: 80px;
    width: 70px;
  }
  &__title{
    width: fit-content;
    position: relative;
    padding-bottom: 20px;
    font-size: 70px;
    line-height: 80px;
    z-index: 1;
    .svg-triangle{
      z-index: -1;
      position: absolute;
      left: -100px;
      top: -10px;
    }
    .svg-sun{
      position: absolute;
      z-index: -1;
      right: -50px;
      bottom: -80px;
      width: 150px;
    }
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      font-size: 45px;
      line-height: 55px;
      border-radius: 6px;
      background-color: rgba(255,255,255,0.4);
    }
  }
}
.overview-content{
  h2{
    display: inline-block;
    padding: 30px 0;
    width: 73%;
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      width: 60%;
    }
  }
  a{
    display: inline-block;
    width: 27%;
    text-align: center;
    max-width: 200px;
    float: right;
    top: 20px;
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      padding: 10px 0;
      font-size: 17px;
      line-height: 27px;
      width: 38%;
      top:25px
    }
  }
}
</style>
