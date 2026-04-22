import type { NuxtPage } from "nuxt/schema"

// https://v3.nuxtjs.org/api/configuration/nuxt.config
export default defineNuxtConfig({
  vite:{
    server: {
      proxy: {
        '/api': 'http://localhost:5048'
      }
    },
    optimizeDeps: {
      include: [
        '@vue/devtools-core',
        '@vue/devtools-kit',
        '@auth0/auth0-vue',
        '@zhuowenli/vue-feather-icons',
      ]
    }
  },
  nitro: {
    output: {
      publicDir: "../api/wwwroot"
    }
  },
  runtimeConfig: {
    // Keys within public are also exposed client-side
    public: {
      auth0domain: process.env.AUTH0DOMAIN,
      auth0clientId: process.env.AUTH0CLIENTID,
      enableAuth0: process.env.ENABLE_AUTH0 !== 'false' && !!process.env.AUTH0DOMAIN && !!process.env.AUTH0CLIENTID
    }
  },

  srcDir: 'src/',
  ssr: false,
  compatibilityDate: '2024-08-09',
  app: {
    head: {
      link: [
        { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
      ]
    }
  },
  hooks: {
    'pages:extend' (pages) {
      function setMiddleware (pages: NuxtPage[]) {
        for (const page of pages) {
          if (/* some condition */ true) {
            page.meta ||= {}
            // Note that this will override any middleware set in `definePageMeta` in the page
            page.meta.middleware = ['auth']
          }
          if (page.children) {
            setMiddleware(page.children)
          }
        }
      }
      setMiddleware(pages)
    }
  }
})