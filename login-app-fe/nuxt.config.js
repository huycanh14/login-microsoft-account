// import path from 'path'
// import fs from 'fs'
import colors from 'vuetify/es5/util/colors'

const config =  {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    titleTemplate: '%s - login-app-fe',
    title: 'login-app-fe',
    htmlAttrs: {
      lang: 'vi',
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' },
      { name: 'google-signin-client_id', content: process.env.CLIENT_ID_GOOGLE},
    ],
    link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },],
    script: [
      // { src: 'https://accounts.google.com/gsi/client', async: true, defer: true, body: false},
      { src: 'https://accounts.google.com/gsi/client',},
    ],
    
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/typescript
    '@nuxt/typescript-build',
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
    '@nuxtjs/composition-api/module'
  ],
  env: {
    ...process.env
  },

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    '@nuxtjs/axios',
    '@nuxtjs/auth-next',
  ],
  router: {
    // middleware: ['auth',],
  },
  auth: {
    strategies: {
      google: {
        clientId: process.env.CLIENT_ID_GOOGLE,
        scope: 'profile email',
        prompt: 'select_account',
        codeChallengeMethod: '',
        responseType: 'code',
        endpoints: {
          token: 'http://localhost:8000/user/google/', // somm backend url to resolve your auth with google and give you the token back
          userInfo: 'http://localhost:8000/auth/user/' // the endpoint to get the user info after you recived the token 
        },
      },
    }
  },
  axios: {
    // proxy: true
    baseURL: 'https://daotaodaihoc.humg.edu.vn/api/',
  },

  // server: {
  //   port: 8080, // default: 3000
  //   host: '0.0.0.0' // default: localhost,
  // },
  // server: {
  //   https: {
  //     key: fs.readFileSync(path.resolve(__dirname, 'key.pem')),
  //     cert: fs.readFileSync(path.resolve(__dirname, 'cert.pem'))
  //   },
  //   // port: 8080, // default: 3000
  //   // host: '0.0.0.0'
  // },

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    theme: {
      dark: true,
      themes: {
        dark: {
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3,
        },
      },
    },
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
    postcss: false,
  },
}
// if (process.env.NODE_ENV === "development") {
//   config.server = {
//     https: {
//       key: fs.readFileSync(path.resolve(__dirname, 'key.pem')),
//       cert: fs.readFileSync(path.resolve(__dirname, 'cert.pem'))
//     },
//      port: 8080,
//     host: '0.0.0.0'
//   }
// }

export default config