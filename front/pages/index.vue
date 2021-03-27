<template>
  <section class="col-md-10 offset-md-1 row">
    <div class="col-md-8 home-content">
      <h1>Trouvez et <br>partagez les évènements <br>qui vous interesse&nbsp;!</h1>
      <Button anchor="Découvrir !" link="/events" custom="primary" />
      <p class="col-md-8">
        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Asperiores blanditiis commodi dicta doloremque
        ducimus eaque eius harum inventore, iure nostrum quam quidem repudiandae, sed, sit unde ut velit vitae voluptatum.
        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Asperiores blanditiis commodi dicta doloremque
        ducimus eaque eius harum inventore, iure nostrum quam quidem repudiandae, sed, sit unde ut velit vitae voluptatum.
        <br>
        <br>
        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Asperiores blanditiis commodi dicta doloremque
        ducimus eaque eius harum inventore, iure nostrum quam quidem repudiandae, sed, sit unde ut velit vitae voluptatum.
      </p>
    </div>
    <img class="col-md-4" src="/img/mobie-home.png">
    <p v-if="$fetchState.pending">
      Récupération en cours...
    </p>
    <p v-else-if="$fetchState.error">
      Une erreur est survenue :(
    </p>
    <div v-else-if="events !== []" class="col-md-10 home-discover">
      <h2>Futurs évènements !</h2>
      <Button link="/events/futur" anchor="Voir tous" custom="primary" />
      <EventCarousel :events="events" />
    </div>
    <svg xmlns="http://www.w3.org/2000/svg" class="light-red-bg" viewBox="0 0 1319.759 1132.776">
      <path
        id="Tracé_5"
        data-name="Tracé 5"
        d="M2089.641,987.721c-161.613-17.437-201.809-140.686-510,0s-540.3-107.648-567.215,526.084,111.236,251.554,435.392,350.794,285.339,91.413,626.433-42.09S2089.641,987.721,2089.641,987.721Z"
        transform="matrix(0.995, 0.105, -0.105, 0.995, -801.68, -1021.573)"
        fill="#ff645f"
        opacity="0.246"
      />
    </svg>

    <svg xmlns="http://www.w3.org/2000/svg" class="red-bg" height="892.492" viewBox="0 0 1015.433 892.492">
      <path id="Tracé_4" data-name="Tracé 4" d="M1967.52,979.7c-142.152-15.337-177.507-123.745-448.591,0s-517.509,15.967-422.76,362.518-56.431,327.389,228.692,414.679,329.1,74.5,629.119-42.932S1967.52,979.7,1967.52,979.7Z" transform="translate(-1075.492 -921.135)" fill="#ff645f" />
    </svg>
  </section>
</template>

<script>
import Button from '~/components/Button'
import EventCarousel from '~/components/travels/TravelList'
export default {
  components: { EventCarousel, Button },
  transition: 'opacity',
  async fetch () {
    const today = Date.now()

    await this.$axios.get('/event',
      {
        params:
          { q: { dateStart: { $gte: today } } }
      })
      .then(response => (this.events = response.data))
  },
  data () {
    return {
      title: 'Page index',
      meta_desc: 'Je suis le magnifique content',
      events: []
    }
  },
  head () {
    return {
      title: this.title,
      meta: [
        { hid: 'description', name: 'description', content: this.meta_desc }
      ]
    }
  }
}
</script>

<style scoped lang="scss">
.home-content{
  display: flex;
  flex-direction: column;
  z-index: 3;
  a{
    align-self: center;
    width: 300px;
    margin: 30px auto;
  }
  p{
    margin: 40px 0;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      margin: 10px 0;
    }
  }
}
h1{
  font-size: 90px;
  line-height: 100px;
  padding: 60px 20px;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    font-size: 50px;
    line-height: 60px;
    padding: 40px 10px;
    br{
      display: none;
    }
  }
}
img{
  padding: 40px;
  object-fit: scale-down;
  z-index: 3;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    display: none;
  }
}
.home-discover{
  margin-top: 100px;
  z-index: 3;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    margin-top: 0;
  }
  h2{
    display: inline-block;
    padding: 30px 0;
    width: 73%;
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      width: 60%;
    }
  }
  a{
    display: inline-block;
    width: 27%;
    text-align: center;
    max-width: 200px;
    float: right;
    top: 20px;
    @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
      padding: 10px 0;
      font-size: 17px;
      line-height: 27px;
      width: 38%;
      top:25px
    }
  }
}
.light-red-bg{
  position: fixed;
  top: 3vh;
  right: -100px;
  height: 110vh;
  z-index: 1;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 120vh;
    top: 0;
  }
}
.red-bg{
  position: fixed;
  top: 12vh;
  right: -120px;
  height: 90vh;
  z-index: 2;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    height: 100vh;
    top: 80px;
  }
  //@media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
  //  position: absolute;
  //  height: 54vh;
  //}
}
</style>
