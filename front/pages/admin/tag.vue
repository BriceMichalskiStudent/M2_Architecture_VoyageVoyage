<template>
  <div class="col-md-10 offset-md-1">
    <v-data-table
      :headers="headers"
      :items="tags"
      sort-by="firstName"
      class="elevation-1"
    >
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
                Cree un tag
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
                        v-model="editedItem.code"
                        label="Code"
                        type="text"
                        :rules="[rules.required]"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.name"
                        label="name"
                        type="text"
                        :rules="[rules.required]"
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
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
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
      { text: 'Nom', value: 'name' },
      { text: 'Code', value: 'code' },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    tags: [],
    editedIndex: -1,
    editedItem: {
      name: '',
      code: ''
    },
    defaultItem: {
      name: '',
      code: ''
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Nouveau tag' : 'Editer le tag'
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
        .get('/tag')
        .then(response => (this.tags = response.data))
    },

    editItem (item) {
      this.editedIndex = this.tags.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
      this.editedIndex = this.tags.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },

    deleteItemConfirm () {
      this.tags.splice(this.editedIndex, 1)
      this.closeDelete()
      this.$axios
        .delete('/tag/' + this.editedItem._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Tag supprimer !'
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

    async save () {
      if (this.editedIndex > -1) {
        Object.assign(this.tags[this.editedIndex], this.editedItem)
        this.$axios
          .put('/tag/', this.editedItem)
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Tag modifier avec succès !'
            }))
          .catch(error => (
            this.$store.commit('sendNotification', {
              status: 'error',
              message: error
            })
          ))
      } else {
        await this.$axios.post('/tag', this.editedItem)
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Tag cree avec succès !'
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
f
