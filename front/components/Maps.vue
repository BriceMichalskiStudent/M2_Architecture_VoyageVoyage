<template>
  <GMap
    ref="gMap"
    :center="{lat: location.coordinates[0], lng: location.coordinates[1]}"
    :options="{fullscreenControl: false, streetViewControl: false, mapTypeControl: false, zoomControl: true, gestureHandling: 'cooperative'}"
    :zoom="10"
  >
    <GMapMarker
      :position="{lat: location.coordinates[0], lng: location.coordinates[1]}"
    >
      <GMapInfoWindow :options="{maxWidth: 800}">
        <b>{{ location.name }}</b>
        <br>
        <br>
        <code>
          Lat: {{ location.coordinates[0] }},
          <br>
          Lng: {{ location.coordinates[1] }}
        </code>
      </GMapInfoWindow>
    </GMapMarker>
  </GMap>
</template>

<script>
export default {
  props: {
    location: {
      type: Object,
      default () {
        return {
          coordinates: [
            45.7137185,
            5.1291016
          ],
          type: 'Point'
        }
      }
    }
  }
}
</script>

<style lang="scss">

.GMap__Wrapper {
  width: 100%;
  height: 80vh;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 50vh;
  }
}
</style>
