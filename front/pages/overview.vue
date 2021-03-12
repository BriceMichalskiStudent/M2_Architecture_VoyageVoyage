<template>
  <div class="col-md-10 offset-md-1 container">
    <section class="events-head">
      <img src="/img/event-banner.png">
      <h1 class="col-md-6">
        Trouvez <br> l'évènement <br>qui vous interesse&nbsp;!
      </h1>
      <div class="search" />
    </section>
    <p v-if="$fetchState.pending">
      Récupération en cours...️
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <section v-else-if="eventsAll !== []" class="events-content col-md-10">
      <h2>Tous les évènements !</h2>
      <Button link="/events/all" anchor="Voir tous" custom="primary" />
      <EventCarousel :events="eventsAll" />
    </section>
    <p v-if="$fetchState.pending">
      Récupération en cours...️
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <section v-else-if="eventsTag !== []" class="events-content col-md-10">
      <h2>évènements appartenant au TAG : {{ tag.name }} !</h2>
      <Button :link="'/tag/'+ tag._id" anchor="Voir tous" custom="primary" />
      <EventCarousel :events="eventsTag" />
    </section>
  </div>
</template>

<script>
import EventCarousel from '~/components/events/Carousel'
import Button from '~/components/Button'

export default {
  components: { Button, EventCarousel },
  transition: 'opacity',
  async fetch () {
    await this.$axios.get('/event')
      .then(response => (this.eventsAll = response.data))

    let tag = null
    let eventsEmpty = true
    while (eventsEmpty === true) {
      await this.$axios.get('/tag')
        .then((response) => {
          const rand = Math.floor(Math.random() * response.data.length)
          tag = response.data[rand]
          this.tag = response.data[rand]
        })

      await this.$axios.get('/event',
        {
          params:
            { q: { tags: tag } }
        })
        .then((response) => {
          if (response.data.length !== 0) {
            eventsEmpty = false
            this.eventsTag = response.data
          }
        })
    }
  },
  data () {
    return {
      title: 'Page events',
      meta_desc: 'Je suis le magnifique content',
      tag: '',
      eventsAll: [
        {
          id: 0,
          name: 'Nowel',
          description: 'Let\'s go les cado, pour les grand et les mioches',
          dateEnd: '2020-12-24 23:00',
          dateStart: '2020-12-25 12:00',
          image: 'https://images.unsplash.com/photo-1576346618381-facf2a17af3d?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1345&q=80',
          address: '36 30, Allo pere noel',
          posts: [
            { '{Post}': '{Post}' }
          ],
          creator: {
            '{User}': '{User}'
          },
          tags: [
            {
              code: 'THUNES',
              name: 'Famille'
            }
          ],
          localization: {
            '2DSPHERE': '2DSPHERE'
          }
        }
      ],
      eventsTag: []
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
.events-head{
  position: relative;
  z-index: 0;
  height: 390px;
  margin-bottom: 80px;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 300px;
  }
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
  img{
    width: 150%;
    position: absolute;
    top: 80px;
    left: -25%;
    z-index: -1;
    max-height: 300px;
    object-fit: cover;
    object-position: 0 20%;
    background-color: $primary;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      top: 60px;
      height: 200px;
    }
  }
  .search{
    background-color: white;
    border: 1px solid rgba(0,0,0,0.3);
    height: 80px;
    width: 70%;
    margin: 10px auto;
    border-radius: 6px;
    position: absolute;
    bottom: -40px;
    left: 15%;
  }
}
.events-content{
  margin-top: 100px;
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
