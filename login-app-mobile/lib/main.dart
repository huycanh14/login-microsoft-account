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
    final _router = FactoryNavRoutesSingleton().items;
    return OrientationBuilder(builder: (_, orientation) {
      return ScreenUtilInit(
        designSize: orientation == Orientation.portrait ? const Size(411, 823) : const Size(360, 690),
        minTextAdapt: true,
        splitScreenMode: true,
        builder: (BuildContext context, child) {
          return MaterialApp.router(
            routerConfig: _router,
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
