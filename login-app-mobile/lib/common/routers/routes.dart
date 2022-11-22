part of 'routes.package.dart';

GoRouter routerGoRoutes = GoRouter(
  initialLocation: RouteConfigName.DEFAULT,
  debugLogDiagnostics: true,
  routes: [
    ShellRoute(
      navigatorKey: locator<NavigationService>().navigatorKey,
      builder: (context, state, child) {
        return child;
      },
      routes: <GoRoute>[
        GoRoute(
          path: RouteConfigName.DEFAULT,
          builder: (_, GoRouterState state) => const HomeView(title: 'demo',),
          redirect: (_, state) {
            return null;
          },
        ),
      ],
    ),
  ],
);
