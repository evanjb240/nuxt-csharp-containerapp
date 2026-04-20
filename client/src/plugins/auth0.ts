import { createAuth0 } from "@auth0/auth0-vue";

export default defineNuxtPlugin((nuxtApp) => {
  const runtimeConfig = useRuntimeConfig();
  const { enableAuth0, auth0domain, auth0clientId } = runtimeConfig.public;

  if (!enableAuth0 || !auth0domain || !auth0clientId) {
    return;
  }

  const auth0 = createAuth0({
    domain: auth0domain as string,
    clientId: auth0clientId as string,
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: 'sample-api',
    }
  });

  if (import.meta.client) {
    nuxtApp.vueApp.use(auth0);
  }
});