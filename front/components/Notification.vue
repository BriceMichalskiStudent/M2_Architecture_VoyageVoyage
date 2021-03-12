<template>
  <section :class="[$store.state.notificationStatus,'notification',{visible:$store.state.notification}, {visibleOut:$store.state.notificationOutAnimation}]">
    <div class="notification-icon">
      <span v-if="$store.state.notificationStatus === 'error'" class="icon-error" />
      <span
        v-else-if="$store.state.notificationStatus === 'success' "
        class="icon-success"
      />
      <span
        v-else
        class="icon-info"
      />
    </div>
    <p class="notification-message">
      {{ $store.state.notificationMessage }}
    </p>
    <div class="notification-close" @click="hideNotifications()">
      <span class="icon-clear" />
    </div>
  </section>
</template>

<script>
export default {
  name: 'Notification',
  methods: {
    hideNotifications () {
      this.$store.commit('resetNotification')
      this.$store.commit('startOutAnimation')
      setTimeout(() => {
        this.$store.commit('endOutAnimation')
      }, 600)
    }
  }
}
</script>
<style lang="scss" scoped >
.notification-icon{
  span{
    font-size: 50px;
  }
}
.notification {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 6;
  color: white;
  font-weight: 700;
  padding: 2rem;
  display: flex;
  flex-direction: row;
  align-items: center;
  transform: translateY(-100%);
  .notification-message {
    text-align: center;
    color: white;
    flex: 1;
    margin: 0;
    padding: 0 2rem;
  }
  .notification-close {
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0.5rem;
    border-radius: 4px;
    cursor: pointer;
    transition: background 0.3s;
    font-size: 26px;
    &:hover {
      background: rgba(0, 0, 0, 0.12);
    }
  }
  &.success {
    background: #10c15c;
    &::after{
      background: #10c15c;
    }
  }
  &.error{
    background: #ed1c24;
    &::after{
      background: #ed1c24;
    }
  }
  &.info{
    background: #0b22e2;
    &::after {
      background: #0b22e2;
    }
  }
  &::after {
    content: "";
    position: absolute;
    height: 10%;
    width: 100%;
    bottom: 100%;
    left: 0;
  }
  &.visible {
    box-shadow: 0 2px 2px 2px rgba(0, 0, 0, 0.12);
    animation-name: notification-in;
    animation-duration: 0.6s;
    animation-timing-function: ease-in-out;
    animation-fill-mode: forwards;
  }
  &.visibleOut {
    box-shadow: 0 2px 2px 2px rgba(0, 0, 0, 0.12);
    animation-name: notification-out;
    animation-duration: 0.6s;
    animation-timing-function: ease-in-out;
  }
}

@keyframes notification-in {
  0% {
    transform: translateY(-100%);
  }
  50% {
    transform: translateY(10%);
  }
  100% {
    transform: translateY(0);
  }
}
@keyframes notification-out {
  0% {
    transform: translateY(0);
  }
  50% {
    transform: translateY(10%);
  }
  100% {
    transform: translateY(-100%);
  }
}
</style>
