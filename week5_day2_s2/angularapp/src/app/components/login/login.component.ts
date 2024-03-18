canActivate Method:

This method is required by the CanActivate interface.
It checks if the user is logged in by calling the isLoggedIn() method of the AuthService.
If the user is logged in (isLoggedIn() returns true), it allows access to the route by returning true.
If the user is not logged in, it redirects the user to the login page ('/login') using the Router and returns false to prevent access to the route.