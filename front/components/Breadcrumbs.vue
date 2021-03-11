<template>
  <div class="breadcrumbs">
    <div
      v-for="(link, i) in breadcrumbsLinks"
      :key="i"
    >
      <nuxt-link
        v-if="i === 0"
        to="/"
        style="color: red"
      >
        {{ link.replace('-', ' ') }}
      </nuxt-link>
      <nuxt-link
        v-else-if="i === 1"
        :to="`/${link}`"
        style="color: red"
      >
        {{ link.replace('-', ' ') }}
      </nuxt-link>
      <nuxt-link
        v-else
        :to="link"
        style="color: red"
      >
        {{ link.replace('-', ' ') }}
      </nuxt-link>
      <span v-if="i !== (breadcrumbsLinks.length - 1)"> > </span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Breadcrumbs',
  computed: {
    breadcrumbsLinks () {
      let tmp = []
      if (this.$route.matched) {
        this.$route.matched.forEach((link) => {
          tmp = link.path.split('/')
          tmp.splice(0, 1)
          tmp.unshift('home')
        })
      }
      return tmp
    }
  }
}
</script>
