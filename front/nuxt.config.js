export default {
  // Target (https://go.nuxtjs.dev/config-target)
  target: 'static',
  ssr: false,

  server: {
    port: process.env.NUXT_PORT || 80 // par défaut: 3000
  },

  // Global page headers (https://go.nuxtjs.dev/config-head)
  head: {
    title: 'Voyage Voyage',
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
    '@/assets/default-style.css'
  ],

  // Plugins to run before rendering page (https://go.nuxtjs.dev/config-plugins)
  plugins: [{ src: '@/plugins/vClickOutside', ssr: true }],

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
    [
      'nuxt-gmaps', {
        key: process.env.API_KEY_Gmap
      }
    ],
    [
      '@nuxtjs/firebase',
      {
        config: {
          apiKey: 'AIzaSyDLmj6r_9jQnnQ6u5TJFwBdWTf1gcN4OIk',
          projectId: 'userwebapi-6e989',
          authDomain: 'zalbani.dev',
          storageBucket: '<storageBucket>',
          messagingSenderId: '<messagingSenderId>',
          appId: '1091974395328',
          measurementId: '<measurementId>'
        },
        services: {
          auth: true // Just as example. Can be any other service.
        }
      }
    ]
  ],
  auth: {
    persistence: 'local',
    initialize: {
      onAuthStateChangedMutation: 'onAuthStateChangedMutation',
      onAuthStateChangedAction: 'onAuthStateChangedAction',
      subscribeManually: false
    },
    ssr: false, // default
    emulatorPort: 9099,
    emulatorHost: 'http://localhost'
  },
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
          primary: '#FDB01D',
          secondary: '#6ECDD9'
        }
      }
    }
  },
  axios: {
    baseURL: process.env.backend_url || 'http://localhost:3001'
  },

  // Build Configuration (https://go.nuxtjs.dev/config-build)
  build: {
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
