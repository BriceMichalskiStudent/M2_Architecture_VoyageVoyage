export const state = () => ({
  notification: false,
  notificationStatus: 'info',
  notificationMessage: 'Bye Bye !',
  notificationOutAnimation: false,
  isAdmin: false,
  user: null
})

export const getters = {
  isAuthenticated (state) {
    return state.user
  },
  loggedInUser (state) {
    return state.user
  },
  isAdmin (state) {
    return state.isAdmin
  }
}

export const mutations = {
  sendNotification (state, object) {
    state.notification = true
    state.notificationStatus = object.status
    state.notificationMessage = object.message
  },
  resetNotification (state) {
    state.notification = false
    state.notificationMessage = 'Bye Bye !'
  },
  startOutAnimation (state) {
    state.notificationOutAnimation = true
  },
  endOutAnimation (state) {
    state.notificationOutAnimation = false
  },
  setIsAdmin (state, boolean) {
    state.isAdmin = boolean
  },
  onAuthStateChangedMutation: (state, { authUser }) => {
    console.log('here')
    console.log(authUser)
    const { uid, email, emailVerified } = authUser
    state.user = { uid, email, emailVerified }
  },
  onAuthStateChangedAction: (ctx, { authUser, claims }) => {
    console.log(authUser)
    if (!authUser) {
      // claims = null
      // Perform logout operations
    } else {
      // Do something with the authUser and the claims object...
    }
  }
}
