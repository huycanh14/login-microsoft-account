<template>
  <v-app id="inspire">
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
              <v-btn color="primary" @click="loginWithMicrosoftAccount">
                Login</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-app>
</template>

<script lang="ts">
import * as qs from 'querystring'
import { defineComponent, useContext } from '@nuxtjs/composition-api'
// import axios from 'axios'
// import * as msal from '@azure/msal-browser'

export default defineComponent({
  name: 'InspirePage',
  setup() {
    const { $axios } = useContext()
    const loginWithMicrosoftAccount = async () => {
      try {
        const accessToken =
          'eyJ0eXAiOiJKV1QiLCJub25jZSI6InhwaElueDQtV0d3UUljU3RRV0cxWklwQWRRc250M0Qxa2gwXzJWQmd5cnciLCJhbGciOiJSUzI1NiIsIng1dCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9iMTk2NDE5OS05NDRjLTRhMDktYjZmYy0zNjQ2OTY0ZDE0NTEvIiwiaWF0IjoxNjY4NzU0NTM4LCJuYmYiOjE2Njg3NTQ1MzgsImV4cCI6MTY2ODc2MDE2MywiYWNjdCI6MSwiYWNyIjoiMSIsImFpbyI6IkFaUUFhLzhUQUFBQUhPOG5CQkRlQUNMdnRJRXhkQjltUWlvTG1NRUJzOE9HZWN5Skd6Mk83b1YyZTViY3pTenJ0RVhYb25CKzJSaCtLSHZnbHBkb3UwL3BjOUgxZ0JlUTlNaUR1UDZ3U0dTMnp4elU2dDBYUFBhUGkzWmduL3F1Z2IrT2hLUy94VUViRzl3STBtcWo2ajF0UVFkQU1oRHA1b2lldnhhM29sdFdqejUzVTBMWnk5UEkzZXhUOEt0SStZT0I0NDRNSklCbiIsImFsdHNlY2lkIjoiNTo6MTAwM0JGRkQ5OURGNjI5NCIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiZGVtby1sb2dpbi1taWNyb3NvZnQtYWNjb3VudCIsImFwcGlkIjoiZDQ4NTBiZWMtNGFmNS00NGVhLTk3ZWQtMDRkODExNzI5MDU0IiwiYXBwaWRhY3IiOiIwIiwiZW1haWwiOiIxNjIxMDUwMDU4QHN0dWRlbnQuaHVtZy5lZHUudm4iLCJpZHAiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9jODUyZDYyYi0zMDMyLTRjZGMtOTZhYi0zMGU0MzY4ZmFiZDcvIiwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiNTguMTg2LjY5LjE0MSIsIm5hbWUiOiJEQU5HIEhVWSBDQU5IICxEQ0NUQ1Q2MUEiLCJvaWQiOiIyNjg3ODY2Mi01OWQ0LTQ3OGQtOTgzZC05YzNhYTQ2NTlmNWUiLCJwbGF0ZiI6IjUiLCJwdWlkIjoiMTAwMzIwMDI0MkUzMUJBMyIsInJoIjoiMC5BVlVBbVVHV3NVeVVDVXEyX0RaR2xrMFVVUU1BQUFBQUFBQUF3QUFBQUFBQUFBQ0lBT1EuIiwic2NwIjoiTWFpbC5TZW5kIG9wZW5pZCBwcm9maWxlIFVzZXIuUmVhZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6IkthRTRDRVNqQ1gwZFVkbjd2MnQtX00tVU1FUXRzWVkzUjJEdGxfT09RVE0iLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiQVMiLCJ0aWQiOiJiMTk2NDE5OS05NDRjLTRhMDktYjZmYy0zNjQ2OTY0ZDE0NTEiLCJ1bmlxdWVfbmFtZSI6IjE2MjEwNTAwNThAc3R1ZGVudC5odW1nLmVkdS52biIsInV0aSI6Ijd2NVhiQUpnNzBXNmNMa0hQVmhzQUEiLCJ2ZXIiOiIxLjAiLCJ3aWRzIjpbIjEzYmQxYzcyLTZmNGEtNGRjZi05ODVmLTE4ZDNiODBmMjA4YSJdLCJ4bXNfc3QiOnsic3ViIjoiZ1RlVUIxV0pmV05zUmhEUXNZU3NiS283Ny1pRGFpdUZ6SU4ycGxRTHZpVSJ9LCJ4bXNfdGNkdCI6MTY1ODU4MTEzOH0.qvsI-h9V_N-Z5Zwlk-DQZX3vySq3GoLey-pXGLOq5W5FYI9ZxJgracXPoRCriU-Xc_nKp1iZWfMKN8noD5pxG_aUoc9sFCfrQE7GuQXl888nJDPCN1CNJRVLYn8kJ98dG3I5MncL4ucIBmAP1OW5m1i3GYCJzHocm3YWKsaJ6EKZS6rGuw6OXD5tnkFFs7pxne4TUSLIIkJm83gtIl4LCwRNIBOP9vgkHVEWTg9YjUf-e77uBp5eTf0mbRbu3AwUckZsyyC9Tr8lT0UN6cFp1YkD9q-QQfvAMtgVgxDaUIBdPicEs3aO5Bm8FCvPP8q5-KiKbyc8g9ccij6EyYe_Pg'

        const body = qs.stringify({
          grant_type: 'password',
          username: 'user@ms',
          password: accessToken,
        })
        const response = await $axios.$post(
          `https://my-api.humg.edu.vn/api/auth/login`,
          body,
        )
        // const response = await fetch(
        //   'https://my-api.humg.edu.vn/api/auth/login',
        //   {
        //     method: 'POST', // *GET, POST, PUT, DELETE, etc.
        //     mode: 'cors', // no-cors, *cors, same-origin
        //     cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        //     credentials: 'same-origin', // include, *same-origin, omit
        //     headers: {
        //       // 'Content-Type': 'application/json',
        //       'Content-Type': 'application/x-www-form-urlencoded',
        //     },
        //     redirect: 'follow', // manual, *follow, error
        //     referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        //     body, // body data type must match "Content-Type" header
        //   }
        // )
        console.log(response)
      } catch (err) {
        // handle error
        console.error(err)
      }
    }

    return {
      loginWithMicrosoftAccount,
    }
  },
})
</script>
