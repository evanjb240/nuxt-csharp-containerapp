# nuxt-csharp-containerapp
Template for Nuxt + C# + Azure Container app setup

## Running Locally
```bash
npm install
```
To start the development server as both frontend and backend together, run:
```bash
npm run dev
```

## Generating Frontend Models
1. Start the backend from /
```bash
npm run dev:api
```
2. From a new terminal, you will see swagger.json generated after running:
```bash
npm run fetch-swagger
```
3. To generate the full typescript models under client/src/api, run:
```bash
npm run gen:api
```
## Deployment

1. Look at appsettings.json → add these GitHub Secrets
2. Add secrets to GitHub
3. CI/CD passes them as env vars → .NET picks them up via AddEnvironmentVariables()
4. Viewable env variables we can add directly to container app and use azure key vault