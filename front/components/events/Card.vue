<template>
  <nuxt-link :to="{ path: '/event/' + event._id }" class="event-card">
    <p v-if="event.tags !== 'undefined' && event.tags.length !== 0 " class="event-tag">
      {{ event.tags[0].name }}
    </p>
    <span class="icon-arrow-right" />
    <img v-if="event.imgUrl === '' || event.imgUrl === undefined " class="event-image" src="/static/img/placeholder-animation-banner.jpg">
    <img v-else class="event-image" :src="event.imgUrl">
    <section class="event-info">
      <p class="event-date">
        {{ $moment(event.dateStart).format("MMM DD") }}
      </p>
      <div class="event-summary">
        <h3> {{ event.title }}</h3>
        <p> {{ event.description }}</p>
      </div>
    </section>
    <p class="event-position">
      {{ event.city }} <span class="icon-location_on" />
    </p>
  </nuxt-link>
</template>
<script>
export default {
  props: {
    event: {
      type: Object,
      default () {
        return {
          id: 0,
          name: 'Soirée D&D',
          description: 'Grosser soirée D&D Rdv Musée d\'art contemporain, villeurbanne wjbxsakm dewjdksxb scjkwbcwb jckcwe we dwe od',
          dateEnd: '2020-02-24 2:00',
          dateStart: '2020-02-23 21:00',
          imgUrl: 'img/placeholder-animation-banner.jpg',
          city: 'Musée d\'art contemporain, villeurbanne',
          posts: [
            { '{Post}': '{Post}' }
          ],
          creator: {
            '{User}': '{User}'
          },
          tags: [
            {
              code: 'KITCHEN',
              name: 'Cuisine'
            },
            {
              code: 'SPORT',
              name: 'PISCINE'
            }
          ],
          localization: {
            '2DSPHERE': '2DSPHERE'
          }
        }
      }
    }
  }
}
</script>
<style scoped lang="scss">
.event-card{
  width: 350px;
  height: 400px;
  border-radius: 10px;
  display: inline-block;
  position: relative;
  overflow: hidden;
  margin: 0;
  background-color: white;
  -webkit-box-shadow: -2px 2px 25px 0px rgba(0,0,0,0.2);
  -moz-box-shadow: -2px 2px 25px 0px rgba(0,0,0,0.2);
  box-shadow: -2px 2px 35px 0px rgba(0,0,0,0.2);
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    width: 250px;
    height: 340px;
  }
  span.icon-arrow-right{
    position: absolute;
    right: 15px;
    top: calc(230px - 75px);
    -webkit-box-shadow: 0 0 0 4px $secondary;
    -moz-box-shadow: 0 0 0 4px $secondary;
    box-shadow: 0 0 0 4px $secondary;
    color: white;
    background-color: $secondary;
    width: 56px;
    height: 56px;
    display: flex;
    align-content: center;
    align-items: center;
    flex-direction: column;
    justify-content: center;
    font-size: 24px;
    border-radius: 10px;
    transition-duration: 0.6s;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      width: 36px;
      height: 36px;
      right: 15px;
      top: calc(190px - 55px);
      font-size: 20px;
      border-radius: 4px;
    }
  }
  &:hover span.icon-arrow-right{
    -webkit-box-shadow: 0 0 0 -1px $secondary;
    -moz-box-shadow: 0 0 0 -1px $secondary;
    box-shadow: 0 0 0 -1px $secondary;
  }
  p.event-tag{
    position: absolute;
    display: inline-block;
    text-align: center;
    height: auto;
    min-width: 150px;
    padding: 5px 20px;
    background-color: $primary;
    font-family: "Montserrat Bold", sans-serif;
    color: white;
    z-index: 1;
    border-radius: 0 6px 6px 0;
    top: 20px;
    left: 0;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      width: 100%;
      top: 0;
      border-radius: unset;
    }
  }
  .event-info {
    height: 130px;
    display: flex;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      height: auto;
      padding: 10px 0;
    }
    .event-date {
      box-sizing: border-box;
      margin: 0;
      width: 30%;
      padding: 10px;
      text-align: center;
      font-family: "Montserrat Bold", sans-serif;
      font-size: 30px;
      line-height: 40px;
      align-self: center;
      @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
        width: 35%;
        font-size: 25px;
        line-height: 35px;
        padding: 5px;
      }
    }

    .event-summary {
      box-sizing: border-box;
      width: 70%;
      text-align: left;
      padding: 10px 20px;
      overflow: hidden;
      position: relative;
      margin-bottom: 10px;
      @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
        width: 65%;
        margin: 0;
        padding: 5px 10px;
        align-content: center;
        justify-content: center;
        height: auto;
        display: flex;
      }
      &::after{
        content: '';
        position: absolute;
        bottom: 0;
        left: 0;
        width: 100%;
        text-align: center;
        margin: 0; padding: 30px 0;
        background-image: linear-gradient(to bottom, transparent, #ffffff);
        @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
          display: none;
        }
      }
      h3{
        padding: 10px 0;
        color: black;
        font-size: 27px;
        line-height: 37px;
        @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
          font-size: 25px;
          line-height: 35px;
          padding: 5px 0;
          align-self: center;
        }
      }
      p{
        padding-bottom: 10px;
        font-size: 14px;
        line-height: 18px;
        @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
          display: none;
        }
      }
    }
  }
  p.event-position{
    width: 100%;
    padding: 0 10px;
    text-align: right;
    font-size: 20px;
    line-height: 25px;
    font-family: "Times New Roman", sans-serif;
    margin: 0;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      font-size: 18px;
      line-height: 23px;
      padding: 10px 5px;
    }
    span{
      color: $primary;
      font-size: 25px;
      line-height: 25px;
      vertical-align: baseline;
      padding: 0 2px;
      @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
        font-size: 18px;
        line-height: 18px;
        padding: 0;
      }
    }
  }
  .event-image{
    width: 100%;
    margin: 0;
    height: 230px;
    object-fit: cover;
    @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
      height: 190px;
    }
  }
}
</style>
