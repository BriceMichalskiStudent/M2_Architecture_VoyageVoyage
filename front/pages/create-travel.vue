<template>
  <section class="content">
    <h1>
      Cree mon evenement
    </h1>
    <div class="register-container">
      <form method="post" @submit.prevent="saveEvent">
        <label>Titre</label>
        <input
          v-model="title"
          type="text"
          placeholder="Titre"
          required
        >
        <label>Contenu</label>
        <input
          v-model="description"
          type="text"
          placeholder="Contenu"
          required
        >
        <label>Adresse</label>
        <input
          v-model="address"
          type="text"
          placeholder="Address"
          required
        >
        <label>Ville</label>
        <input
          v-model="city"
          type="text"
          placeholder="Ville"
          required
        >
        <label>Date de debut</label>
        <v-menu
          ref="menuDateStart"
          v-model="menuDateStart"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          min-width="290px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="dateStart"
              placeholder="Date de debut"
              label="Date de debut"
              readonly
              v-bind="attrs"
              v-on="on"
            />
          </template>
          <v-date-picker
            ref="picker"
            v-model="dateStart"
            :min="Date().now"
            @change="menuDateStart = false"
          />
        </v-menu>
        <label>Date de fin</label>
        <v-menu
          ref="menu"
          v-model="menuDateEnd"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          min-width="290px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="dateEnd"
              placeholder="Date de fin"
              label="Date de Fin"
              readonly
              v-bind="attrs"
              v-on="on"
            />
          </template>
          <v-date-picker
            ref="picker"
            v-model="dateEnd"
            :min="dateStart"
            required
            @change="menuDateEnd = false"
          />
        </v-menu>
        <p v-if="$fetchState.pending">
          Récupération en cours...
        </p>
        <p v-else-if="$fetchState.error">
          Une erreur est survenue :(
        </p>
        <v-select
          v-else
          v-model="tags"
          :items="items"
          attach
          label="Tags"
          placeholder="Choisissez vos tags"
          multiple
        />
        <input
          type="file"
          name="image"
          placeholder="Uploader une image de couverture"
          @change="onFileChange"
        >
        <Button anchor="Cree !" type="submit" custom="large primary" />
      </form>
    </div>
  </section>
</template>

<script>
import Button from '@/components/Button'
export default {
  layout: 'form',
  middleware: 'connected',
  transition: 'opacity',
  components: { Button },
  async fetch () {
    await this.$axios
      .get('/tag')
      .then((response) => {
        this.objectList = response.data
        for (let i = 0; response.data.length > i; i++) {
          this.items.push(response.data[i].name)
        }
      }
      )
  },

  data () {
    return {
      title: '',
      description: '',
      dateStart: '',
      dateEnd: '',
      creator: '',
      address: '',
      city: '',
      location: null,
      eventPict: null,
      menuDateStart: false,
      menuDateEnd: false,
      items: [],
      objectList: [],
      tags: []
    }
  },

  methods: {
    onFileChange (e) {
      const files = e.target.files || e.dataTransfer.files
      if (!files.length) { return }
      this.eventPict = files[0]
    },

    async saveEvent () {
      try {
        const tempTable = []
        for (let i = 0; this.tags.length > i; i++) {
          const resultat = this.objectList.find(tag => tag.name === this.tags[i])
          tempTable.push(resultat._id)
        }
        this.tags = tempTable

        const addressToFind = this.address + ' ' + this.city
        if (addressToFind.replaceAll(' ', '') !== '') {
          await this.convertAddressToPoint(addressToFind)
        }

        const event = {
          title: this.title,
          description: this.description,
          dateStart: this.dateStart,
          dateEnd: this.dateEnd,
          address: this.address,
          city: this.city,
          creator: this.$auth.user._id,
          location: this.location,
          tags: this.tags
        }
        const formData = new FormData()
        formData.append('img', this.eventPict)
        formData.append('event', JSON.stringify(event))
        await this.$axios.post('event', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
        await this.$store.commit('sendNotification', {
          status: 'success',
          message: 'Vous avez cree votre evenement  !'
        })
      } catch (e) {
        await this.$store.commit('sendNotification', {
          status: 'error',
          message: e.response.data.message
        })
      }
      this.$router.push('/profile/events')
    },
    async convertAddressToPoint (address) {
      const query = address.replaceAll(' ', '+').toLowerCase()
      const response = await this.$axios.get('/helper/address', {
        params: {
          q: query
        }
      })
      this.location = {
        coordinates: [response.data.features[0].geometry.coordinates[1], response.data.features[0].geometry.coordinates[0]],
        type: 'Point'
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
