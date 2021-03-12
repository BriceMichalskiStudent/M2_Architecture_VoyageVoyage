<template>
  <div class="col-md-10 offset-md-1 container">
    <h1 class="col-md-6">
      Mes évènements !
    </h1>
    <p v-if="$fetchState.pending">
      Récupération en cours...️
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <section v-else-if="eventsCreated !== []" class="events-content col-md-11">
      <h2>Évènements cree </h2>
      <section class="events-container">
        <EventCard v-for="event in eventsCreated" :key="'created_'+event._id" :event="event" />
      </section>
    </section>
    <p v-if="$fetchState.pending">
      Récupération en cours...️
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <section v-else-if="eventsParticipation !== []" class="events-content col-md-11">
      <h2>Vos inscription : </h2>
      <section class="events-container">
        <EventCard v-for="event in eventsParticipation" :key="'participate_'+event._id" :event="event" />
      </section>
    </section>
  </div>
</template>

<script>
import EventCard from '~/components/events/Card'

export default {
  components: { EventCard },
  transition: 'opacity',
  async fetch () {
    await this.$axios.get('/event',
      {
        params:
          { q: { creator: this.$auth.user._id } }
      })
      .then(response => (this.eventsCreated = response.data))

    await this.$axios.get('/user/' + this.$auth.user._id + '/events/')
      .then(response => (this.eventsParticipation = response.data.events))
  },
  data () {
    return {
      title: 'Page events',
      meta_desc: 'Je suis le magnifique content',
      tag: '',
      eventsCreated: [],
      eventsParticipation: []
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
.events-container{
  width: 100%;
  display: flex;
  flex-flow: row wrap;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    align-items: center;
    justify-content: center;
  }
  a{
    width: calc(33% - 40px);
    margin: 20px;
    min-width: 300px;
    min-height: 400px;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      width: calc(50% - 40px);
      min-width: 250px;
    }
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      width: calc(100% - 20px);
      margin: 10px;
      max-width: 300px;
    }
  }
}
.events-content{
  h2{
    display: inline-block;
    padding: 30px 0;
    width: 73%;
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      width: 60%;
    }
  }
}
</style>
