export default function ({ store, redirect }) {
  if (store.isAdmin === false) {
    store.commit('sendNotification', {
      status: 'danger',
      message: 'Vous n\'avez pas les droits nécessaire pour acceder a cette page !'
    })
    return redirect('/profile')
  }
}
