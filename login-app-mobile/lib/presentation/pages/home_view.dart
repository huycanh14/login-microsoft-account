import 'dart:io';

import 'package:aad_oauth/aad_oauth.dart';
import 'package:aad_oauth/model/config.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_sign_in/google_sign_in.dart';

import '../../common/components/base_input/text/base_input_text.dart';
import '../../common/configs/service_location.dart';
import '../../common/validation/form_validation_manager.dart';
import '../../injection.dart';

class HomeView extends StatefulWidget {
  const HomeView({super.key, required this.title});

  final String title;

  @override
  State<HomeView> createState() => _HomeViewState();
}

class _HomeViewState extends State<HomeView> {
  final GlobalKey<FormState> _form = GlobalKey<FormState>();
  final TextEditingController _email = TextEditingController();
  final formValidationManager = FormValidationManager();
  final dio = Dio();
  String valueDefault = "111";
  final GoogleSignIn _googleSignIn = GoogleSignIn(
    scopes: <String>[
      'email',
      'https://www.googleapis.com/auth/contacts.readonly',
    ],
  );

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _googleSignIn.signInSilently();
  }

  @override
  void dispose() {
    super.dispose();
    formValidationManager.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final Config config = Config(
      tenant: 'organizations',
      clientId: 'd4850bec-4af5-44ea-97ed-04d811729054',
      scope: 'openid profile offline_access',
      redirectUri: Platform.isAndroid
          ? 'msauth://com.alec_edu.login_app_mobile/2jmj7l5rSw0yVb%2FvlWAYkK%2FYBwk%3D'
          : 'msauth.com.alecEdu.loginAppMobile://auth',
      navigatorKey: locator<NavigationService>().navigatorKey,
    );
    final AadOAuth oauth = AadOAuth(config);
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              'You have pushed the button this many times:',
            ),
            Form(
              key: _form,
              child: Column(
                children: [
                  BaseInputText(
                    id: UniqueKey().toString(),
                    errorColor: Colors.red,
                    controller: _email,
                    valueDefault: valueDefault,
                    rules: "required",
                    title: 'Email',
                    formValidationManager: formValidationManager,
                    onChanged: (e) {
                      valueDefault = e;
                      print(valueDefault);
                      print(e);
                    },
                  ),
                  BaseInputText(
                    id: UniqueKey().toString(),
                    errorColor: Colors.red,
                    valueDefault: valueDefault,
                    rules: "required|min_length:5",
                    formValidationManager: formValidationManager,
                    title: '1111',
                    readOnly: true,
                    onChanged: (e) => {valueDefault = e},
                  ),
                ],
              ),
            ),
            Text(valueDefault),
            ElevatedButton(
              onPressed: () async {
                try {
                  var result = await _googleSignIn.signIn();
                  if (result != null) {
                    final ggAuth = await result.authentication;
                    var resApi = await dio.post(
                      "https://79d0-2405-4802-27a-e3d0-d89a-1d7e-bc42-be20.ap.ngrok.io/api/auth/login/external-google",
                      data: {
                        "token": ggAuth.idToken,
                        "client": Platform.isAndroid ? "Android" : "iOS",
                      },
                    );
                    if (resApi.data['success'] == true) {
                      print("Dang nhap thanh cong");
                    } else {
                      print(resApi.data["message"]);
                    }
                  }
                  print(result);
                } catch (error) {
                  print(error);
                }
              },
              child: const Text('SIGN IN GOOGLE'),
            ),
            ElevatedButton(
              onPressed: () async {
                await oauth.logout();
                print('Logged out');
              },
              child: const Text("Logout"),
            ),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          try {
            await oauth.login();
            var accessToken = await oauth.getAccessToken();
            print('Logged in successfully, your access token: $accessToken');
          } catch (e) {
            print(e);
          }
        },
        tooltip: 'Next',
        child: const Icon(Icons.arrow_forward),
      ),
    );
  }
}
