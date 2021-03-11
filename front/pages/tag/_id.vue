<template>
  <div class="col-md-10 offset-md-1 container">
    <h1 class="col-md-6">
      Evènements {{ tag.name }} !
    </h1>
    <p v-if="$fetchState.pending">
      Récupération en cours...️
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <section v-else-if="events !== []" class="events-content col-md-11">
      <section class="events-container">
        <EventCard v-for="event in events" :key="event._id" :event="event" />
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
    const tagId = this.$route.params.id
    await this.$axios
      .get('/tag/' + tagId)
      .then(response => (this.tag = response.data))
    await this.$axios.get('/event',
      {
        params:
          { q: { tags: tagId } }
      })
      .then((response) => {
        if (response.data.length !== 0) {
          this.events = response.data
        }
      })
  },
  data () {
    return {
      title: 'Page events',
      meta_desc: 'Je suis le magnifique content',
      tag: {},
      events: []
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
