import { useAuth0, User } from "@auth0/auth0-vue";

export default defineNuxtRouteMiddleware(async (to) => {
   const runtimeConfig = useRuntimeConfig();
   const authEnabled = runtimeConfig.public.enableAuth0 as boolean;

   if (!authEnabled) {
      return;
   }

   const auth = useAuth0();
   const globalUser = useState<User | undefined>("auth-user", () => undefined);
   const globalIsAuthenticated = useState<boolean>("is-authenticated", () => false);
   const globalIsLoading = useState<boolean>("auth-loading", () => false);

   try {
      await auth.checkSession();
      //get roles here too
   } catch (err) {
      console.warn("Auth0 session check failed:", err);
   }

   if (auth) {
      globalUser.value = auth.user;
      globalIsAuthenticated.value = auth.isAuthenticated.value;
      globalIsLoading.value = auth.isLoading.value;
   }

   const publicPages = ['/', '/about', '/contactus'];
   const isPublic = publicPages.includes(to.path.toLowerCase());

   if (!auth.isAuthenticated.value && !isPublic) {
      return navigateTo('/');
   }
});