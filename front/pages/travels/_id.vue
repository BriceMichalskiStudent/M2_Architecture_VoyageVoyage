<template>
  <!--  <p v-if="$fetchState.pending">-->
  <!--    Récupération en cours...️-->
  <!--  </p>-->
  <!--  <p v-else-if="$fetchState.error">-->
  <!--    Une erreur est survenue :(-->
  <!--  </p>-->
  <section class="col-md-10 offset-md-1 row">
    <div class="col-md-4 travel-map">
      <Maps :location="travel.location" />
    </div>
    <div class="offset-md-6 col-md-6 single-travel-content">
      <h1>{{ travel.title }}</h1>
      <UserInfo :author="travel.author" :place="travel.place" class="user-info" />
      <ImageList :images="travel.images" />
      <p>
        {{ travel.description }}
      </p>
      <button v-if="loggedInUser !== null && alreadySubscribe === false && owner === false" class="primary button large" @click="subscribe">
        S'inscrire !
      </button>
      <button v-else-if="alreadySubscribe === true" class="button large" @click="unSubscribe">
        Se désinscrire !
      </button>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'
import Maps from '~/components/Maps'
import oneTravel from '~/assets/dataSchema/oneTravel.json'

export default {
  components: { Maps },
  transition: 'opacity',
  // async fetch () {
  //   const travelId = this.$route.params.id
  //   let currenttravel = {}
  //   await this.$axios
  //     .get('/travel/' + travelId)
  //     .then((response) => {
  //       this.travel = response.data
  //       currenttravel = response.data
  //     })
  //
  //   if (this.loggedInUser !== null) {
  //     for (let i = 0; this.$auth.user.travels.length > i; i++) {
  //       if (this.$auth.user.travels[i] === travelId) {
  //         this.alreadySubscribe = true
  //       }
  //     }
  //     if (this.$auth.user._id === currenttravel.creator._id) {
  //       this.owner = true
  //     }
  //   }
  // },
  data () {
    return {
      title: 'Page index',
      meta_desc: 'Je suis le magnifique content',
      alreadySubscribe: false,
      owner: false,
      travel: {}
    }
  },
  computed: {
    ...mapGetters(['loggedInUser'])
  },
  created () {
    this.travel = oneTravel
  },
  methods: {
    async subscribe () {
      await this.$axios.put('/travel/' + this.travel._id + '/join/' + this.$auth.user._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Vous etes désormais inscrit a l\'evenement : ' + this.travel.title
          }))
        .catch(error => (
          this.$store.commit('sendNotification', {
            status: 'error',
            message: error
          })
        ))
      this.alreadySubscribe = true
    },
    async unSubscribe () {
      await this.$axios.put('/travel/' + this.travel._id + '/unjoin/' + this.$auth.user._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Vous n\'etes plus inscrit a l\'evenement : ' + this.travel.title
          }))
        .catch(error => (
          this.$store.commit('sendNotification', {
            status: 'error',
            message: error
          })
        ))
      this.alreadySubscribe = false
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
<style lang="scss" scoped>
.travel-map{
  position: fixed;
}
.single-travel-content {
  z-index: 3;
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  align-content: center;
  justify-content: center;
  h1 {
    text-align: center;
    padding: 20px 0;
  }
  .user-info{
    align-self: center;
    width: 50%;
  }
  img {
    padding: 0;
    object-fit: cover;
    width: 100%;
    height: 300px;
    margin: 20px 0;
  }

  button {
    margin: 10px 0;
    float: right;
  }
}
</style>
