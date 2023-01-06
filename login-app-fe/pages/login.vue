<template>
  <v-app id="login">
    <transition name="fade">
      <v-alert v-if="show" :type="success ? 'success' : 'error'">
        {{ messsage }}</v-alert
      >
    </transition>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Login form</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  name="login"
                  label="Login"
                  type="text"
                ></v-text-field>
                <v-text-field
                  id="password"
                  name="password"
                  label="Password"
                  type="password"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <div id="googleButton" data-itp_support="false"></div>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="loginWithMicrosoftAccount">
                Login
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-app>
</template>

<script lang="ts">
// import * as qs from 'querystring'
import {
  defineComponent,
  onMounted,
  ref,
  useContext,
} from '@nuxtjs/composition-api'
// import axios from 'axios'
// import * as msal from '@azure/msal-browser'
declare const google: any

export default defineComponent({
  name: 'InspirePage',
  setup() {
    const { env, $axios } = useContext()
    const loginWithMicrosoftAccount = async () => {}
    const messsage = ref('')
    const success = ref(true)
    const show = ref(false)

    onMounted(() => {
      // window.onload = () => {
      //   google.accounts.id.initialize({
      //     client_id: env.CLIENT_ID_GOOGLE,
      //     callback: handleCredentialResponse,
      //     context: 'signin',
      //   })
      //   google.accounts.id.renderButton(
      //     document.getElementById('googleButton'),
      //     { theme: 'outline', size: 'large' } // customization attributes
      //   )
      //   google.accounts.id.prompt() // also display the One Tap dialog
      // }
      google.accounts.id.initialize({
        client_id: env.CLIENT_ID_GOOGLE,
        callback: handleCredentialResponse,
        context: 'signin',
      })
      google.accounts.id.renderButton(document.getElementById('googleButton'), {
        theme: 'outline',
        size: 'large',
      })
      google.accounts.id.prompt()
    })

    const handleCredentialResponse = async (google: any) => {
      // console.log(response)
      const response = await $axios.$post(
        `http://localhost:5001/api/auth/login/external-google`,
        { token: google?.credential }
      )
      if (response.resultCode !== 201) {
        success.value = false
        messsage.value = response.message
      } else {
        success.value = true
        messsage.value = 'Dang nhap thanh cong'
      }
      show.value = true
      setTimeout(() => {
        show.value = false
      }, 3000)
    }

    return {
      messsage,
      success,
      show,
      loginWithMicrosoftAccount,
    }
  },
})
</script>
