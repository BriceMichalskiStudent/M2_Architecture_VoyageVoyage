<template>
  <div class="col-md-10 offset-md-1">
    <v-data-table
      :headers="headers"
      :items="users"
      sort-by="firstName"
      class="elevation-1"
    >
      <template v-slot:item.imgUrl="{ item }">
        <v-img
          v-if="item.imgUrl !== ''"
          :src="item.imgUrl"
          contain
          class="img_preview"
          height="70px"
          width="70px"
        />
        <v-img
          v-else
          src="/img/placeholder-profile.png"
          contain
          class="img_preview"
          height="70px"
          width="70px"
        />
      </template>
      <template v-slot:top>
        <v-toolbar
          flat
        >
          <v-divider
            class="mx-4"
            inset
            vertical
          />
          <v-spacer />
          <v-dialog
            v-model="dialog"
            max-width="800px"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                color="primary"
                dark
                class="mb-2"
                v-bind="attrs"
                v-on="on"
              >
                Cree un utilisateur
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.firstName"
                        label="First name"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.lastName"
                        label="Last name"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.phone"
                        label="Telephone"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.mail"
                        label="Email"
                        type="email"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.password"
                        label="Password"
                        type="password"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-file-input
                        v-model="editedItem.imgFile"
                        label="Profile picture"
                        type="file"
                      />
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer />
                <v-btn
                  color="secondary darken-1"
                  text
                  @click="close"
                >
                  Annuler
                </v-btn>
                <v-btn
                  color="primary darken-1"
                  text
                  @click="save"
                >
                  Sauvegarder
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogDelete" max-width="800px">
            <v-card>
              <v-card-title class="headline">
                <p class="admin-big">
                  <b>Cet action est irreversible.</b><br>
                  Etes-vous sur de vouloir supprimer ?
                </p>
              </v-card-title>
              <v-card-actions>
                <v-spacer />
                <v-btn color="primary darken-1" text @click="closeDelete">
                  Annuler
                </v-btn>
                <v-btn color="secondary darken-1" text @click="deleteItemConfirm">
                  Oui, supprimer
                </v-btn>
                <v-spacer />
              </v-card-actions>
            </v-card>
          </v-dialog>
          <v-dialog v-model="dialogGrant" max-width="800px">
            <v-card>
              <v-card-title class="headline">
                <p class="admin-big">
                  <b>Etes-vous sur de vouloir accorder les privileges administrateur a l'utilisateur : {{ editedItem.firstName + ' ' + editedItem.lastName }} ?</b>
                </p>
              </v-card-title>
              <v-card-actions>
                <v-spacer />
                <v-btn color="secondary darken-1" text @click="closeGrant">
                  Annuler
                </v-btn>
                <v-btn color="primary darken-1" text @click="grantItemConfirm">
                  Oui, passer admin
                </v-btn>
                <v-spacer />
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon
          medium
          color="primary"
          @click="grantItem(item)"
        >
          mdi-arrow-up-bold
        </v-icon>
        <v-icon
          medium
          color="blue darken-1"
          class="mr-2"
          @click="editItem(item)"
        >
          mdi-pencil
        </v-icon>
        <v-icon
          medium
          color="secondary"
          @click="deleteItem(item)"
        >
          mdi-delete
        </v-icon>
      </template>
      <template v-slot:no-data>
        <v-btn
          color="primary"
          @click="initialize"
        >
          Réessayer
        </v-btn>
      </template>
    </v-data-table>
  </div>
</template>
<script>
export default {
  layout: 'admin',
  data: () => ({
    rules: {
      required: value => !!value || 'Required.'
    },
    dialog: false,
    dialogDelete: false,
    dialogGrant: false,
    headers: [
      { text: 'Photo de profile', value: 'imgUrl' },
      { text: 'Nom', value: 'lastName' },
      { text: 'Prenom', value: 'firstName' },
      { text: 'Email', value: 'mail' },
      { text: 'Telephone', value: 'phone' },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    users: [],
    editedIndex: -1,
    editedItem: {
      firstName: '',
      lastName: '',
      mail: '',
      phone: '',
      password: '',
      imgFile: null
    },
    defaultItem: {
      firstName: '',
      lastName: '',
      mail: '',
      phone: '',
      imgUrl: ''
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Nouvel utilisateur' : 'Editer l\'utilisateur'
    }
  },

  watch: {
    dialog (val) {
      val || this.close()
    },
    dialogDelete (val) {
      val || this.closeDelete()
    }
  },

  created () {
    this.initialize()
  },

  methods: {
    initialize () {
      this.$axios
        .get('/user')
        .then(response => (this.users = response.data))
    },

    editItem (item) {
      this.editedIndex = this.users.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    grantItem (item) {
      this.editedIndex = this.users.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogGrant = true
    },

    grantItemConfirm () {
      const idAdmin = '5fa6a6239a28712fcc4a3e70'
      let alreadyAdmin = false
      for (let i = 0; i < this.editedItem.Roles.length; i++) {
        if (this.editedItem.Roles[i] === idAdmin) {
          alreadyAdmin = true
        }
      }
      if (alreadyAdmin === true) {
        this.$store.commit('sendNotification', {
          status: 'info',
          message: 'l\'utilisateur est deja administrateur !'
        })
      } else {
        this.editedItem.Roles.push(idAdmin)
        this.$axios
          .patch('/user/' + this.editedItem._id, this.editedItem)
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'l\'utilisateur est maintenant administrateur !'
            }))
          .catch((error) => {
            this.$store.commit('sendNotification', {
              status: 'error',
              message: error
            })
          })
      }
      this.closeGrant()
    },

    deleteItem (item) {
      this.editedIndex = this.users.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },

    deleteItemConfirm () {
      this.users.splice(this.editedIndex, 1)
      this.closeDelete()
      this.$axios
        .delete('/user/' + this.editedItem._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Utilisateur supprimer !'
          }))
    },

    close () {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeDelete () {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeGrant () {
      this.dialogGrant = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    async save () {
      const user = {
        _id: this.editedItem._id,
        firstName: this.editedItem.firstName,
        lastName: this.editedItem.lastName,
        mail: this.editedItem.mail,
        password: this.editedItem.password,
        phone: this.editedItem.phone
      }

      if (this.editedIndex > -1) {
        if (this.editedItem.imgFile !== undefined) {
          const formData = new FormData()
          formData.append('img', this.editedItem.imgFile)
          formData.append('user', JSON.stringify(user))
          await this.$axios.put('/user', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
            .then(
              this.$store.commit('sendNotification', {
                status: 'success',
                message: 'Utilisateur modifier avec succès !'
              }))
            .catch(error => (
              this.$store.commit('sendNotification', {
                status: 'error',
                message: error
              })
            ))
          this.initialize()
        } else {
          this.$axios
            .patch('/user/' + this.editedItem._id, user)
            .then(
              this.$store.commit('sendNotification', {
                status: 'success',
                message: 'Utilisateur modifier avec succès !'
              }))
            .catch(error => (
              this.$store.commit('sendNotification', {
                status: 'error',
                message: error
              })
            ))
          this.initialize()
        }
      } else {
        const formData = new FormData()
        formData.append('img', this.editedItem.imgFile)
        formData.append('user', JSON.stringify(user))
        await this.$axios.post('user', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Utilisateur cree avec succès !'
            })
          )
          .catch(error => (
            this.$store.commit('sendNotification', {
              status: 'error',
              message: error
            })
          ))
        this.initialize()
      }
      this.close()
    }
  }
}
</script>
