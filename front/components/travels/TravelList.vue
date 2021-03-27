<template>
  <section>
    <div v-for="event in travels" :key="event._id" class="img-wrapper">
      <TravelCard :event="event" />
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
        return [
          {
            id: 0,
            name: 'Soirée D&D',
            description: 'Grosser soirée D&D Rdv Musée d\'art contemporain, villeurbanne wjbxsakm dewjdksxb scjkwbcwb jckcwe we dwe od',
            dateEnd: '2020-02-24 2:00',
            dateStart: '2020-02-23 21:00',
            imgUrl: 'img/placeholder-animation-banner.jpg',
            address: 'Musée d\'art contemporain, villeurbanne',
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
          },
          {
            id: 1,
            name: 'Oupla',
            description: 'Grosser dossier lo',
            dateEnd: '2020-07-02 2:00',
            dateStart: '2020-07-02 21:00',
            image: 'https://images.unsplash.com/photo-1572914339448-b0479ad30fd5?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80',
            address: 'Chez, moi',
            posts: [
              { '{Post}': '{Post}' }
            ],
            creator: {
              '{User}': '{User}'
            },
            tags: [
              {
                code: 'KITCHEN',
                name: 'Fiesta'
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
        ]
      }
    }
  },
  data () {
    return {
      slickOptions: {
        slidesToShow: 3,
        arrows: true,
        infinite: false,
        responsive: [
          {
            breakpoint: 960,
            settings: {
              slidesToShow: 2,
              arrows: true,
              infinite: false
            }
          },
          {
            breakpoint: 600,
            settings: {
              slidesToShow: 1,
              arrows: true,
              infinite: false
            }
          }
        ]
      }
    }
  }
}
</script>

<style lang="scss">
.carousel-container{
  width: 100%;
  padding: 10px 0 60px;
  position: relative;
  margin-bottom: 60px;
}
.carousel-wrapper {
  width: 1200px;
  @media only screen and (max-width: map-get($grid-breakpoints, 'md')) {
    width: 600px;
    padding: 0;
  }
  @media only screen and (max-width: map-get($grid-breakpoints, 'sm')) {
    max-width: 300px;
    padding: 0;
  }
  span.slick-arrow{
    position: absolute;
    color: black;
    font-size: 35px;
    line-height: 45px;
    &.slick-prev{
      bottom: 0;
      left: 0;
    }
    &.slick-next{
      bottom: 0;
      right: 0;
    }
  }
}
.slick-slider{
  position: unset!important;
}
.slick-list{
  overflow: unset!important;
}
.img-wrapper img {
  margin: auto;
  background-image: linear-gradient(gray 100%, transparent 0);
}
</style>
