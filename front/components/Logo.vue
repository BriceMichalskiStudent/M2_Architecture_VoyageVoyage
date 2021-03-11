<template>
  <svg id="logo" xmlns="http://www.w3.org/2000/svg" :style="{ width: width, height: height }" viewBox="0 0 97 39">
    <rect
      id="Rectangle_5"
      data-name="Rectangle 5"
      width="9"
      height="9"
      rx="1"
      transform="translate(0 7.273)"
      fill="#f7c109"
    />
    <text
      id="Calend_App"
      data-name="Calend
   App"
      transform="translate(54 21)"
      font-size="24"
      font-family="TimesNewRomanPS-BoldMT, Times New Roman"
      font-weight="700"
    ><tspan x="-36.674" y="0">Calend</tspan><tspan x="-30.352" y="12" xml:space="preserve">   App</tspan></text>
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
