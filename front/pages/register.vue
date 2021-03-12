<template>
  <section class="content">
    <h1>
      Inscription
    </h1>
    <div class="register-container">
      <form method="post" @submit.prevent="register">
        <label>Nom</label>
        <input
          v-model="lastName"
          type="text"
          name="username"
          placeholder="Nom"
          required
        >
        <label>Prénom</label>
        <input
          v-model="firstName"
          type="text"
          name="username"
          placeholder="Prénom"
          required
        >
        <label>Adresse mail</label>
        <input
          v-model="mail"
          type="email"
          name="mail"
          placeholder="Adresse mail"
          required
        >
        <label>Numéro de telephone</label>
        <input
          v-model="phone"
          type="text"
          name="username"
          placeholder="Numéro de telephone"
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
        <input
          type="file"
          name="imag"
          placeholder="Confirmer le mots de passe"
          @change="onFileChange"
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
      firstName: '',
      lastName: '',
      mail: '',
      password: '',
      passwordConfirmation: '',
      phone: '',
      profilePict: null,
      error: null
    }
  },

  methods: {
    onFileChange (e) {
      const files = e.target.files || e.dataTransfer.files
      if (!files.length) { return }
      this.profilePict = files[0]
    },
    async register () {
      if (this.password !== this.passwordConfirmation) {
        await this.$store.commit('sendNotification', {
          status: 'error',
          message: 'Les mots de passe que vous avez renseigner ne sont pas identique  !'
        })
      } else {
        try {
          const user = {
            firstName: this.firstName,
            lastName: this.lastName,
            mail: this.mail,
            password: this.password,
            phone: this.phone
          }
          const formData = new FormData()
          formData.append('img', this.profilePict)
          formData.append('user', JSON.stringify(user))
          await this.$axios.post('user', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
          await this.$auth.loginWith('local', {
            data: {
              mail: this.mail,
              password: this.password
            }
          })
          await this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Vous vous etes désormais inscrit a MyCalendApp  !'
          })
          this.$router.push('/profile')
        } catch (e) {
          await this.$store.commit('sendNotification', {
            status: 'error',
            message: e.response.data.message
          })
        }
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
