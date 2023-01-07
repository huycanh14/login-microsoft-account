import 'dart:io';

import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:url_strategy/url_strategy.dart';

import 'common/configs/init.dart';
import 'common/routers/factory_routes_singleton.dart';
import 'presentation/controllers/loading_controller/loading_controller_cubit.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: FirebaseOptions(
      appId: Platform.isAndroid ? "1:747698519209:android:39cfec06623d3c486b3db8" : '1:747698519209:ios:6269870f49a435bc6b3db8',
      apiKey: Platform.isAndroid ? "AIzaSyApEeGaZI8XuDtVM6CTV4M3bPADQyqp40U" : "AIzaSyBBb2aTE88d8a2ql4Wgmg4fLwRzmixDhtI",
      messagingSenderId: '747698519209',
      projectId: 'alectapidev',
    ),
    name: Platform.isAndroid? 'AlecEduDevAndroid' : 'AlecEduDeviOS',
  );
  await initApp();
  setPathUrlStrategy();
  startApp();
}

void startApp() {
  runApp(MultiBlocProvider(
    providers: [
      BlocProvider<LoadingControllerCubit>(
        create: (BuildContext context) => LoadingControllerCubit(),
      ),
    ],
    child: const MyApp(),
  ));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    final router = FactoryNavRoutesSingleton().items;
    return OrientationBuilder(builder: (_, orientation) {
      return ScreenUtilInit(
        designSize: orientation == Orientation.portrait ? const Size(411, 823) : const Size(360, 690),
        minTextAdapt: true,
        splitScreenMode: true,
        builder: (BuildContext context, child) {
          return MaterialApp.router(
            routerConfig: router,
            title: "MyApp",
            debugShowCheckedModeBanner: false,
            theme: ThemeData(
              primarySwatch: Colors.blue,
              visualDensity: VisualDensity.adaptivePlatformDensity,
              appBarTheme: const AppBarTheme(
                systemOverlayStyle: SystemUiOverlayStyle(
                  statusBarIconBrightness: Brightness.light,
                ),
              ),
            ),
            builder: (context, widget) {
              return MediaQuery(
                data: MediaQuery.of(context).copyWith(textScaleFactor: 1),
                child: SafeArea(
                  top: false,
                  bottom: false,
                  left: false,
                  right: false,
                  child: widget ?? Container(),
                ),
              );
            },
          );
        },
      );
    });
  }
}
