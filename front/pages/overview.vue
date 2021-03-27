<template>
  <div class="col-md-10 offset-md-1 container">
    <section class="overview-head">
      <br>
      <h1 class="col-md-6">
        Fil d'actualité
      </h1>
    </section>
    <!--    <p v-if="$fetchState.pending">-->
    <!--      Récupération en cours...️-->
    <!--    </p>-->
    <!--    <p v-else-if="$fetchState.error">-->
    <!--      Une erreur est survenue :(-->
    <!--    </p>-->
    <section class="overview-content col-md-10">
      <h2>Les derniers voyages !</h2>
      <travel-list :travels="travelAll" />
    </section>
  </div>
</template>

<script>
import TravelList from '~/components/travels/TravelList'

export default {
  components: { TravelList },
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
      travelAll: [
        {
          id: 0,
          name: 'Nowel',
          description: 'Let\'s go les cado, pour les grand et les mioches',
          nbLike: 4,
          images: [
            {
              image: 'https://images.unsplash.com/photo-1576346618381-facf2a17af3d?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1345&q=80',
              description: '',
              legend: ''
            },
            {
              image: 'https://images.unsplash.com/photo-1576346618381-facf2a17af3d?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1345&q=80',
              description: '',
              legend: ''
            }
          ],
          creator: {
            '{User}': '{User}'
          },
          place: {
            city: '',
            country: '',
            localization: {
              '2DSPHERE': '2DSPHERE'
            }
          }
        }
      ]
    }
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
.overview-head{
  position: relative;
  z-index: 0;
  h1{
    font-size: 58px;
    line-height: 68px;
    z-index: 1;
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
