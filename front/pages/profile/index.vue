<template>
  <p v-if="$fetchState.pending">
    Récupération en cours...️
  </p>
  <p v-else-if="$fetchState.error">
    Une erreur est survenue :(
  </p>
  <section v-else class="col-md-8 offset-md-2 row">
    <h1>
      Mon Compte
    </h1>
    <div class="content col-md-8 offset-md-2">
      <img v-if="this.$auth.user.imgUrl !== undefined && this.$auth.user.imgUrl !== ''" :src="this.$auth.user.imgUrl" class="profile">
      <img v-else src="/img/placeholder-profile.png" class="profile">
      <form method="post" @submit.prevent="updateAccount">
        <div class="input-group">
          <label>Prenom</label>
          <input v-model="editedUser.firstName" type="text" required>
        </div>
        <div class="input-group">
          <label>Nom</label>
          <input v-model="editedUser.lastName" type="text" required>
        </div>
        <div class="input-group">
          <label>Numero de telephone</label>
          <input v-model="editedUser.phone" type="text" required>
        </div>
        <div class="input-group">
          <label>Adresse email</label>
          <input v-model="editedUser.mail" type="email" required>
        </div>
        <h2> Changer de mots de passe </h2>
        <div class="input-group">
          <label>Mots de passe</label>
          <input v-model="editedUser.password" type="password">
        </div>
        <div class="input-group">
          <label>Validation mots de passe</label>
          <input v-model="editedUser.validatePassword" type="password">
        </div>
        <Button type="submit" custom="primary large" anchor="Mettre a jour !" />
      </form>
      <hr>
      <h2> Zone de danger !</h2>
      <form method="post" @submit.prevent="deleteAccount">
        <Button type="submit" custom="Secondary large" anchor="Supprimer mon compte !" />
      </form>
    </div>
  </section>
</template>
<style lang="scss" scoped>
@media only screen and (max-width: map-get($grid-breakpoints, 'md')){
  .row{
    margin: 0;
  }
}
.profile{
  width: 100px;
  height: 100px;
  border-radius: 50%;
  border: 1px solid black;
  object-fit: cover;
  margin: 40px;
  align-self: center;
}
.content{
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
form{
  display: flex;
  flex-flow: wrap;
  justify-content: flex-end;
  .input-group{
    label{
      display: unset;
    }
    margin: 0 20px;
    width: calc(50% - 40px);
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')){
      margin: 0 10px;
      width: calc(100% - 20px);
    }
  }
  h2{
    width: calc(100% - 40px);
    margin: 30px 20px;
  }
  button{
    margin: 20px 0;
  }
}
</style>
<script>

import Button from '~/components/Button'
export default {
  components: { Button },
  middleware: 'connected',
  transition: 'opacity',
  fetch () {
    Object.assign(this.user, this.$auth.user)
    this.editedUser.firstName = this.user.firstName
    this.editedUser.lastName = this.user.lastName
    this.editedUser.phone = this.user.phone
    this.editedUser.mail = this.user.mail
  },
  data () {
    return {
      title: 'Page index',
      meta_desc: 'Je suis le magnifique content',
      user: {},
      editedUser: {
        firstName: '',
        lastName: '',
        phone: '',
        mail: '',
        password: '',
        validatePassword: ''
      }
    }
  },
  methods: {
    async updateAccount () {
      if (this.editedUser.password !== '' && this.editedUser.validatePassword !== '') {
        if (this.editedUser.password !== this.editedUser.validatePassword) {
          this.$store.commit('sendNotification', {
            status: 'error',
            message: 'Les mots de passe que vous avez renseigner ne sont pas identique  !'
          })
        } else {
          delete this.editedUser.validatePassword
        }
      } else {
        delete this.editedUser.password
        delete this.editedUser.validatePassword
      }
      await this.$axios
        .patch('/user/' + this.$auth.user._id, this.editedUser)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Votre compte a ete modifier avec succès !'
          }))
        .catch(error => (
          this.$store.commit('sendNotification', {
            status: 'error',
            message: error
          })
        ))
      this.$auth.fetchUser()
      this.$router.push('/profile')
    },
    async deleteAccount () {
      await this.$axios
        .delete('/user/' + this.user._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Votre compte a ete supprimer avec succès !'
          }))
        .catch(error => (
          this.$store.commit('sendNotification', {
            status: 'error',
            message: error
          })
        ))
      await this.$auth.logout()
      this.$router.push('/')
    }
  }
}
</script>
