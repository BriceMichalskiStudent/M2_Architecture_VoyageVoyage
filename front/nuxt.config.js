export default {
  // Target (https://go.nuxtjs.dev/config-target)
  target: 'server',
  ssr: true,

  // Global page headers (https://go.nuxtjs.dev/config-head)
  head: {
    title: 'MyCalendAPP',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },

  router: {
    linkExactActiveClass: 'exact-active-link'
  },

  // Global CSS (https://go.nuxtjs.dev/config-css)
  css: [
    'vue-slick-carousel/dist/vue-slick-carousel.css',
    '@/assets/general.css',
    '@/assets/icons/icons.css'
  ],

  // Plugins to run before rendering page (https://go.nuxtjs.dev/config-plugins)
  plugins: [{ src: '@/plugins/vClickOutside', ssr: true }, { src: './plugins/vSlickCarousel.js' }],

  // Auto import components (https://go.nuxtjs.dev/config-components)
  components: true,

  // Modules for dev and build (recommended) (https://go.nuxtjs.dev/config-modules)
  buildModules: [
    '@nuxtjs/vuetify',
    '@nuxtjs/eslint-module',
    '@nuxtjs/moment'
  ],
  moment: {
    locales: ['fr']
  },

  // Modules (https://go.nuxtjs.dev/config-modules)
  modules: [
    '@nuxtjs/axios',
    '@nuxtjs/pwa',
    '@nuxtjs/style-resources',
    '@nuxtjs/auth',
    ['nuxt-gmaps', {
      key: process.env.API_KEY_Gmap
  }]
  ],

  styleResources: {
    scss: [
      '@/assets/variables.scss'
    ]
  },
  vuetify: {
    theme: {
      light: true,
      themes: {
        light: {
          primary: '#f7c109',
          secondary: '#ff645f'
        }
      }
    }
  },
  axios: {
    baseURL: process.env.backend_url || 'http://localhost:3001'
  },
  auth: {
    strategies: {
      local: {
        endpoints: {
          login: { url: 'token', method: 'post', propertyName: 'token' },
          user: { url: 'user/current', method: 'get', propertyName: '' },
          logout: false
        }
      }
    }
  },

  // Build Configuration (https://go.nuxtjs.dev/config-build)
  build: {
    transpile: ['@nuxtjs/auth'],
    extend (config, ctx) {
      // Exécuter ESLint lors de la sauvegarde
      if (ctx.isDev && ctx.isClient) {
        config.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /(node_modules)/,
          options: {
            fix: true
          }
        })
      }
    }
  }
}
