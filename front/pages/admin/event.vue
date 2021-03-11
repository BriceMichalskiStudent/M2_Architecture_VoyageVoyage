<template>
  <div class="col-md-10 offset-md-1">
    <v-data-table
      :headers="headers"
      :items="events"
      sort-by="date"
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
      <template v-slot:item.dateStart="{ item }">
        <span>{{ new Date(item.dateStart).toLocaleString() }}</span>
      </template>
      <template v-slot:item.dateEnd="{ item }">
        <span>{{ new Date(item.dateEnd).toLocaleString() }}</span>
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
                Cree un Event
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
                      <v-file-input
                        v-model="editedItem.imgFile"
                        label="Illustration de l'evennement"
                        type="file"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.title"
                        label="Title"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.description"
                        label="description"
                      />
                    </v-col>

                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.city"
                        label="Ville"
                        :rules="[rules.required]"
                        @change="convertAddressToPoint"
                      />
                    </v-col>

                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-text-field
                        v-model="editedItem.address"
                        label="Adresse"
                        :rules="[rules.required]"
                        @change="convertAddressToPoint"
                      />
                    </v-col>

                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-menu
                        ref="menu-start-date"
                        v-model="menuStartDate"
                        :close-on-content-click="false"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.dateStart"
                            label="Date de Debut"
                            prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          />
                        </template>
                        <v-date-picker
                          ref="picker"
                          v-model="editedItem.dateStart"
                          locale="fr"
                          :rules="[rules.required]"
                          @change="menuStartDate = false"
                        />
                      </v-menu>

                      <v-menu
                        ref="menuStartTime"
                        v-model="menuStartTime"
                        :close-on-content-click="false"
                        :nudge-right="40"
                        :return-value.sync="editedItem.hourStart"
                        transition="scale-transition"
                        offset-y
                        max-width="290px"
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.hourStart"
                            label="Heure de debut"
                            prepend-icon="mdi-clock-time-four-outline"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          />
                        </template>
                        <v-time-picker
                          v-if="menuStartTime"
                          v-model="editedItem.hourStart"
                          format="24hr"
                          full-width
                          :rules="[rules.required]"
                          @click:minute="$refs.menuStartTime.save(editedItem.hourStart)"
                        />
                      </v-menu>
                    </v-col>

                    <v-col
                      cols="12"
                      sm="6"
                      md="12"
                    >
                      <v-menu
                        ref="menu-end"
                        v-model="menuEndDate"
                        :close-on-content-click="false"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.dateEnd"
                            value=""
                            label="Date de Fin"
                            prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          />
                        </template>
                        <v-date-picker
                          ref="picker"
                          v-model="editedItem.dateEnd"
                          locale="fr"
                          :rules="[rules.required]"
                          @change="menuEndDate = false"
                        />
                      </v-menu>

                      <v-menu
                        ref="menuEndTime"
                        v-model="menuEndTime"
                        :close-on-content-click="false"
                        :nudge-right="40"
                        :return-value.sync="editedItem.hourEnd"
                        transition="scale-transition"
                        offset-y
                        max-width="290px"
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="editedItem.hourEnd"
                            label="Heure de Fin"
                            prepend-icon="mdi-clock-time-four-outline"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                          />
                        </template>
                        <v-time-picker
                          v-if="menuEndTime"
                          v-model="editedItem.hourEnd"
                          format="24hr"
                          full-width
                          :rules="[rules.required]"
                          @click:minute="$refs.menuEndTime.save(editedItem.hourEnd)"
                        />
                      </v-menu>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer />
                <v-btn
                  color="primary darken-1"
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
                <b>Cet action est irreversible.</b><br>
                Etes-vous sur de vouloir supprimer ?
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
    adrress: '',
    coordinates: {},
    dialog: false,
    dialogDelete: false,
    menuStartDate: false,
    menuEndDate: false,
    menuStartTime: false,
    menuEndTime: false,
    headers: [
      { text: 'Illustration', value: 'imgUrl' },
      { text: 'Titre', value: 'title' },
      { text: 'Description', value: 'description' },
      { text: 'Ville', value: 'city' },
      { text: 'Debut', value: 'dateStart' },
      { text: 'Fin', value: 'dateEnd' },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    events: [],
    editedIndex: -1,
    editedItem: {
      title: '',
      description: '',
      dateStart: '',
      hourStart: '',
      dateEnd: '',
      hourEnd: '',
      creator: '',
      address: '',
      city: '',
      location: [],
      imgFile: null
    },
    defaultItem: {
      title: '',
      description: '',
      dateStart: '',
      hourStart: '',
      dateEnd: '',
      hourEnd: '',
      creator: '',
      address: '',
      city: '',
      location: [],
      imgFile: null
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Nouvel Event' : 'Editer l\'event'
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
        .get('/event')
        .then((response) => {
          this.events = response.data
        })
    },

    editItem (item) {
      this.editedIndex = this.events.indexOf(item)
      this.editedItem = Object.assign({}, item)
      const startDate = new Date(this.editedItem.dateStart)
      const endDate = new Date(this.editedItem.dateEnd)

      this.editedItem.hourStart = startDate.getHours().toString().padStart(2, '0') + ':' + startDate.getMinutes().toString().padStart(2, '0')
      this.editedItem.dateStart = startDate.getFullYear() + '-' + (startDate.getMonth() + 1).toString().padStart(2, '0') + '-' + startDate.getDate().toString().padStart(2, '0')

      this.editedItem.hourEnd = endDate.getHours().toString().padStart(2, '0') + ':' + endDate.getMinutes().toString().padStart(2, '0')
      this.editedItem.dateEnd = endDate.getFullYear() + '-' + (endDate.getMonth() + 1).toString().padStart(2, '0') + '-' + endDate.getDate().toString().padStart(2, '0')

      this.dialog = true
    },

    deleteItem (item) {
      this.editedIndex = this.events.indexOf(item)
      this.editedItem = Object.assign({}, item)
      const startDate = new Date(this.editedItem.dateStart)
      const endDate = new Date(this.editedItem.dateEnd)

      this.editedItem.hourStart = startDate.getHours().toString().padStart(2, '0') + ':' + startDate.getMinutes().toString().padStart(2, '0')
      this.editedItem.dateStart = startDate.getFullYear() + '-' + (startDate.getMonth() + 1).toString().padStart(2, '0') + '-' + startDate.getDate().toString().padStart(2, '0')

      this.editedItem.hourEnd = endDate.getHours().toString().padStart(2, '0') + ':' + endDate.getMinutes().toString().padStart(2, '0')
      this.editedItem.dateEnd = endDate.getFullYear() + '-' + (endDate.getMonth() + 1).toString().padStart(2, '0') + '-' + endDate.getDate().toString().padStart(2, '0')

      this.dialogDelete = true
    },

    deleteItemConfirm () {
      this.events.splice(this.editedIndex, 1)
      this.closeDelete()
      this.$axios
        .delete('/event/' + this.editedItem._id)
        .then(
          this.$store.commit('sendNotification', {
            status: 'success',
            message: 'Evennement supprimé !'
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
      let item = Object.assign({}, this.editedItem)

      // Fucking Date
      const timeStartSplit = item.hourStart.split(':')
      item.dateStart = new Date(item.dateStart)
      item.dateStart.setHours(timeStartSplit[0], timeStartSplit[1], 0, 0)
      item.dateStart = JSON.stringify(item.dateStart).replaceAll('"', '')

      const timeEndSplit = item.hourEnd.split(':')
      item.dateEnd = new Date(item.dateEnd)
      item.dateEnd.setHours(timeEndSplit[0], timeEndSplit[1], 0, 0)
      item.dateEnd = JSON.stringify(item.dateEnd).replaceAll('"', '')

      const formData = new FormData()
      formData.append('img', item.imgFile)

      if (this.editedIndex > -1) {
        formData.append('event', JSON.stringify(item))
        Object.assign(this.events[this.editedIndex], item)
        await this.$axios
          .put('/event/', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Evennement modifier avec success !'
            })

          )
          .catch(error => (
            this.$store.commit('sendNotification', {
              status: 'error',
              message: error
            })
          ))
        this.initialize()
      } else {
        item.creator = this.$auth.user._id
        formData.append('event', JSON.stringify(item))
        await this.$axios
          .post('/event/', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
          .then(
            this.$store.commit('sendNotification', {
              status: 'success',
              message: 'Evennement cree avec success !'
            }),
            item = Object.assign({}, this.defaultItem)
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
    },

    async convertAddressToPoint () {
      const address = this.editedItem.address + ' ' + this.editedItem.city
      const query = address.replaceAll(' ', '+').toLowerCase()
      const response = await this.$axios.get('/helper/address', {
        params: {
          q: query
        }
      })
      this.editedItem.location = {
        coordinates: [response.data.features[0].geometry.coordinates[1], response.data.features[0].geometry.coordinates[0]],
        type: 'Point'
      }
    }
  }
}
</script>
