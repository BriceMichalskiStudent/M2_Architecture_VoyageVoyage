<template>
  <section class="content">
    <h1>
      Inscription
    </h1>
    <div class="register-container">
      <form method="post" @submit.prevent="register">
        <label>Adresse mail</label>
        <input
          v-model="mail"
          type="email"
          name="mail"
          placeholder="Adresse mail"
          required
        >
        <label>Mot de passe</label>
        <input
          v-model="password"
          type="password"
          name="password"
          placeholder="Mot de passe"
          required
        >
        <label>Confirmer le mots de passe</label>
        <input
          v-model="passwordConfirmation"
          type="password"
          name="passwordConfirmation"
          placeholder="Confirmer le mots de passe"
          required
        >
        <Button anchor="S'inscrire" type="submit" custom="large" />
      </form>
      <hr>
      <p>
        Vous avez deja un compte ?
      </p>
      <nuxt-link to="/login">
        Se Connecter
      </nuxt-link>
    </div>
  </section>
</template>

<script>
import Button from '@/components/Button'
export default {
  layout: 'form',
  transition: 'opacity',
  components: { Button },

  data () {
    return {
      mail: '',
      password: '',
      passwordConfirmation: ''
    }
  },
  methods: {
    register () {
      if (this.password !== this.passwordConfirmation) {
        this.$store.commit('sendNotification', {
          status: 'error',
          message: 'Les mots de passe que vous avez renseigner ne sont pas identique  !'
        })
      } else {
        this.$fire
          .auth
          .createUserWithEmailAndPassword(this.mail, this.password)
          .then((r) => {
            console.log(r)
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Vous vous etes désormais inscrit a MyCalendApp  !'
            })
            this.$router.push('/profile')
          }
          )
          .catch((e) => {
            this.$store.commit('sendNotification', {
              status: 'error',
              message: e.response.data.message
            })
          })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.content{
  width: 70%;
  margin: 0 auto;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
    width: 90%;
    margin: 0 auto;
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
  input{
    width: calc(50% - 13px);
    margin: 10px 5px;
    display: inline-flex;
    &:last-of-type{
      margin-right: calc(50% + 5px);
    }
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
      width: 100%;
      margin: 10px 0;
    }
  }
  button{
    margin: 20px 0;
    display: inline-block;
  }
}

.register-container{
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
