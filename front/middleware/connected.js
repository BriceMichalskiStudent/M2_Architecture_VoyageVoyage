export default function ({ store, redirect }) {
  if (store.state.auth.loggedIn !== true) {
    store.commit('sendNotification', {
      status: 'info',
      message: 'Vous devez etre connecter pour acceder a cette page !'
    })
    return redirect('/login')
  } else {
    for (let i = 0; i < store.state.auth.user.Roles.length; i++) {
      if (store.state.auth.user.Roles[i].code === 'ADMIN') {
        store.commit('setIsAdmin', true)
      }
    }
  }
}
