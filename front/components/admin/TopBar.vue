<template>
  <div>
    <div class="top-element">
      <form method="get">
        <div class="input-group">
          <span class="icon-search" />
          <input
            id="search"
            type="text"
            placeholder="search"
            name="search"
            class="search"
          >
        </div>
      </form>
    </div>
  </div>
</template>
<style lang="scss" scoped>
.top-element{
  position: absolute;
  top: 0;
  height: 160px;
  z-index: 0;
  width: calc(100% - 60px);
  margin-left: 60px;
  padding-bottom: 70px;
  display: flex;
  flex-flow: row nowrap;
  justify-content: center;
  align-items: center;
  background-color: $primary;
  color: white;
  form{
    flex-flow: row wrap;
    align-items: center;
    .input-group{
      width: 350px;
      transition: all 0.15s cubic-bezier(0.68, -0.55, 0.265, 1.55);
      border-color: rgba(0, 0, 0, 0.6);
      background-color: rgba(255, 255, 255, 0.9);
      border-radius: 2rem;
      font-weight: 400;
      display: flex;
      flex-direction: row;
      align-items: center;
      span{
        color: black;
        padding: 0.5rem 0.75rem;
      }
      input{
        padding: 0.5rem 0.5rem;
        width: 80%;
        border: none;
      }
    }
  }
}
</style>
<script>

export default {
  data () {
    return {
      isActive: false,
      test: true
    }
  },
  methods: {
    myFilter () {
      this.isActive = !this.isActive
    }
  }
}
</script>
