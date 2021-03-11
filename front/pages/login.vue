<template>
  <section class="content">
    <h1>
      Se connecter
    </h1>
    <div class="login-container">
      <form method="post" @submit.prevent="login">
        <label class="label">Email</label>
        <input
          v-model="mail"
          type="email"
          name="mail"
          placeholder="test@mail.com"
          required
        >
        <label class="label">Password</label>
        <input
          v-model="password"
          type="password"
          name="password"
          placeholder="Mot de passe"
          required
        >
        <Button anchor="Se connecter" type="submit" custom="large" />
      </form>
      <hr>
      <p>
        Vous n'avez pas encore de compte ?
      </p>
      <nuxt-link to="/register">
        Se cree un compte
      </nuxt-link>
    </div>
  </section>
</template>

<script>
import Button from '@/components/Button'

export default {
  components: { Button },
  layout: 'form',
  transition: 'opacity',
  data () {
    return {
      mail: '',
      password: '',
      error: null
    }
  },

  methods: {
    async login () {
      try {
        await this.$auth.loginWith('local', {
          data: {
            mail: this.mail,
            password: this.password
          }
        })
        this.$store.commit('sendNotification', {
          status: 'success',
          message: 'Vous etes désormais connecter !'
        })
        this.$router.push('/profile')
      } catch (e) {
        this.$store.commit('sendNotification', {
          status: 'error',
          message: e
        })
        this.$router.push('/login')
      }
    }
  }
}
</script>
<style lang="scss" scoped>
.content{
  width: 40%;
  margin: 0 auto;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    width: 90%;
  }
}
h1{
  width: 100%;
  height: calc(8vh - 24px);
  text-align: center;
  align-self: flex-start;
  font-size: 30px;
  line-height: 40px;
  padding-top: 10px;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    min-height: calc(8vh - 24px);
    height: auto;
  }
}
form{
  text-align: right;
  button{
    margin: 20px 0;
    display: inline-block;
  }
}

.login-container{
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 62vh;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    min-height: calc(92vh - 40px);
    height: auto;
    margin: 20px 0;
  }
  a{
    text-decoration: underline;
    align-self: flex-end;
  }
}

</style>
