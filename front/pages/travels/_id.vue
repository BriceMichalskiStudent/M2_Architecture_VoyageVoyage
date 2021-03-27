<template>
  <!--  <p v-if="$fetchState.pending">-->
  <!--    Récupération en cours...️-->
  <!--  </p>-->
  <!--  <p v-else-if="$fetchState.error">-->
  <!--    Une erreur est survenue :(-->
  <!--  </p>-->
  <section class="col-md-10 offset-md-1 row">
    <div class="col-md-6 single-travel-content">
      <h1>{{ travel.title }}</h1>
      <img v-if="travel.imgUrl === '' || travel.imgUrl === undefined " class="travel-image" src="/img/placeholder-animation-banner.jpg">
      <img v-else :src="travel.imgUrl">
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
    <div class="col-md-5 travel-map">
      <Maps :location="travel.location" />
    </div>
    <svg xmlns="http://www.w3.org/2000/svg" class="red-bg" width="403.125" height="919.935" viewBox="0 0 403.125 919.935">
      <g id="graph" transform="translate(184.861 -229.685)">
        <path id="Tracé_6" data-name="Tracé 6" d="M15454.531-1865.795s113.014,22.237,129.818,250.821,109.182,246.768,107.959,367.752-169.816,304.948-237.777,225.6S15454.531-1865.795,15454.531-1865.795Z" transform="translate(-15544.437 2152)" fill="#ff645f" />
      </g>
    </svg>

    <svg xmlns="http://www.w3.org/2000/svg" class="light-red-bg" width="403.125" height="867.379" viewBox="0 0 403.125 867.379">
      <path
        id="Tracé_7"
        data-name="Tracé 7"
        d="M15454.531-1865.795s214.662-49.009,140.008,184.681,75.651,334.135,97.77,433.892-102.6,322.63-263.153,162.942S15454.531-1865.795,15454.531-1865.795Z"
        transform="matrix(0.996, -0.087, 0.087, 0.996, -15142.382, 3230.855)"
        fill="#ff645f"
        opacity="0.35"
      />
    </svg>
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
@media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
  .row {
    margin: 0;
  }
}
.single-travel-content{
  z-index: 3;
  h1{
    padding: 30px 0;
  }
  img{
    padding: 0;
    object-fit: cover;
    width: 100%;
    height: 300px;
    margin: 20px 0;
  }
  button{
    margin: 10px 0;
    float: right;
  }
}
.travel-map{
  z-index: 3;
  position: fixed;
  right: 40px;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    position: unset;
  }
}
.light-red-bg{
  position: fixed;
  top: 3vh;
  left: -100px;
  height: 110vh;
  z-index: 1;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 120vh;
    top: 0;
  }
}
.red-bg{
  position: fixed;
  top: 12vh;
  left: -120px;
  height: 90vh;
  z-index: 2;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 100vh;
    top: 80px;
  }
}
.button{
  z-index: 2;
  padding: 10px 30px;
  background-color: $secondary!important;
  color: white;
  border-radius: 10px;
  position: relative;
  text-align: center;
  &::after{
    content: '';
    width: 100%;
    height: 100%;
    position: absolute;
    top:0;
    left: 0;
    background-color: $secondary!important;;
    opacity: 0.35;
    z-index: -1;
    border-radius: 10px;
    transform: rotate(0);
    transition-duration: 0.2s;
    transition-timing-function: ease-in-out;
  }
  &:hover{
    &::after{
      transform: rotate(5deg);
    }
  }
}
.primary{
  background-color: $primary!important;
  &::after{
    background-color: $primary!important;;
  }
}
.large{
  padding: 10px 10%;
}
</style>
