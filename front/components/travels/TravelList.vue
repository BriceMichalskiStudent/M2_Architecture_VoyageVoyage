<template>
  <section>
    <div v-for="travel in travels" :key="travel.id" class="inline">
      <TravelCard :travel="travel" />
    </div>
  </section>
</template>

<script>
import TravelCard from '~/components/travels/TravelCard'
export default {
  components: { TravelCard },
  props: {
    travels: {
      type: Array,
      default () {
        return []
      }
    }
  }
}
</script>
<style lang="scss" scoped>
.inline{
  display: inline-block;
  margin: 10px;
  width: calc(33% - 20px);
  background: #fff;
  box-shadow: 0 0 15px 0 rgba(153, 183, 176, 0.25);
  border-radius: 2px;
  height: 400px;
}
</style>
