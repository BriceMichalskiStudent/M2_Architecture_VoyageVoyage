<template>
  <section class="profile">
    <div v-click-outside="externalClick" class="in-bar" @click="triggerProfile">
      <img v-if="loggedInUser.imgUrl !== undefined && loggedInUser.imgUrl !== ''" :src="loggedInUser.imgUrl">
      <img v-else src="/img/placeholder-profile.png">
      <div class="data">
        <p>{{ loggedInUser.firstName }} {{ loggedInUser.lastName }}</p>
        <p><span>{{ nbEvents }}</span> évènements a venir</p>
      </div>
    </div>
    <div :class="['action', {active : profileActivate}]">
      <ul>
        <li>
          <nuxt-link to="/profile">
            Mon compte
          </nuxt-link>
        </li>
        <li>
          <nuxt-link to="/profile/events">
            Mes évènements
          </nuxt-link>
        </li>
        <li>
          <a @click="logout">
            Déconnexion
          </a>
        </li>
      </ul>
    </div>
  </section>
</template>
<script>
import vClickOutside from 'v-click-outside'
import { mapGetters } from 'vuex'

export default {
  directives: {
    clickOutside: vClickOutside.directive
  },
  fetch () {
    this.nbEvents = this.$auth.user.events.length
  },
  data () {
    return {
      nbEvents: 0,
      profileActivate: false
    }
  },
  computed: {
    ...mapGetters(['loggedInUser'])
  },
  methods: {
    async logout () {
      await this.$auth.logout()
    },
    triggerProfile () {
      this.profileActivate = !this.profileActivate
    },
    externalClick (event) {
      this.profileActivate = false
    }
  }
}
</script>
<style lang="scss" scoped>
@media only screen and (max-width: map-get($grid-breakpoints, 'md')){
  .profile {
    height: auto;
    background-color: white;
    box-shadow: -3px 6px 25px -5px rgba(195,195,195,0.76);
    border-radius: 6px;
    width: 100%;
  }
}
.in-bar{
  cursor: pointer;
  height: 50px;
  position: relative;
  box-shadow: none;
  transition-duration: 0.6s;
  border-radius: 4px;
  padding: 0 10px;
  &:hover{
    background-color: rgba(247, 193, 9, 0.5);
  }
  img{
    width: 40px;
    height: 40px;
    border-radius: 50%;
    margin: 5px;
    display: inline-block;
    object-fit: cover;
  }
  .data{
    display: inline-block;
    height: 40px;
    margin: 5px;
    vertical-align: top;
    p{
      padding: 0;
      margin: 0;
      font-size: 14px;
      line-height: 19px;
      &:first-of-type{
        font-family:'Montserrat Bold', sans-serif;
      }
      &:last-of-type{
        font-family: "Montserrat Italic", sans-serif;
        color: #939393;
        & span{
          font-family:'Montserrat Bold', sans-serif;
        }
      }
    }
  }
}
.action{
  opacity: 0;
  transition-duration: 0.6s;
  height: auto;
  border-radius: 6px;
  position: absolute;
  margin: 10px;
  left: -10px;
  bottom: -158px;
  width: 100%;
  background-color: white;
  box-shadow: -3px 6px 25px -5px rgba(195,195,195,0.76);
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    opacity: 1;
    position: unset;
    left: unset;
    bottom: unset;
    background-color: unset;
    margin: 0;
  }
  &.active{
    opacity: 1;
  }
  ul{
    padding: 0;
    li{
      list-style: none;
      a{
        height: 44px;
        padding: 10px 15px;
        position: relative;
        width: 100%;
        font-size: 14px;
        line-height: 24px;
        color: black;
        display: inline-block;
        &:after{
          opacity: 0;
          transition-duration: 0.4s;
          content: '';
          position: absolute;
          width: 0;
          height: 0;
          top: 15px;
          right: 20px;
          border-top: 7px solid transparent;
          border-bottom: 7px solid transparent;
          border-right: 11px solid $secondary;
          border-radius: 2px;
        }
        &:hover:after{
          opacity: 1;
        }
      }
      &:last-of-type{
        border-top: solid 1px #c3c3c3;
      }
    }
  }
}
</style>
