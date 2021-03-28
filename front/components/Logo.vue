<template>
  <svg xmlns="http://www.w3.org/2000/svg" width="170" height="80" viewBox="0 0 200 100">
    <g id="Groupe_1" data-name="Groupe 1" transform="translate(-82.805 -256.457)">
      <text id="titre" transform="translate(82.805 300.457)" fill="#fdb01d" font-size="41" font-family="Quicksand, sans-serif"><tspan x="0" y="0">Voyage</tspan></text>
      <text
        id="titre-2"
        data-name="titre"
        transform="translate(138.805 335.457)"
        fill="#6ecdd9"
        font-size="41"
        font-family="Quicksand, sans-serif"
      ><tspan x="0" y="0">Voyage</tspan></text>
    </g>
  </svg>
</template>
<style>
#logo{
  max-width: 100%;
}
</style>
<script>
export default {
  props: {
    height: {
      type: String,
      default: '100px'
    },
    width: {
      type: String,
      default: '100px'
    }
  }
}
</script>
