param location string = resourceGroup().location
param containerAppName string
param environmentName string = '${containerAppName}'
param image string
param registryServer string = 'ghcr.io'
param registryUsername string
@secure()
param registryPassword string
param auth0Domain string
param auth0ClientId string
@secure()
param resendApiKey string
param blobContainerName string
param dbServer string
param dbPort string
param dbUsername string
@secure()
param dbPassword string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-03-01-preview' = {
  name: '${containerAppName}-logs'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource containerEnvironment 'Microsoft.App/managedEnvironments@2024-04-01' = {
  name: environmentName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

resource containerApp 'Microsoft.App/containerApps@2024-04-01' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerEnvironment.id
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: 80
        transport: 'Auto'
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
      registries: [
        {
          server: registryServer
          userName: registryUsername
          passwordSecretRef: 'containerRegistryPassword'
        }
      ]
      secrets: [
        {
          name: 'resendApiKey'
          value: resendApiKey
        }
        {
          name: 'dbPassword'
          value: dbPassword
        }
      ]
      env: [
        {
          name: 'AUTH0DOMAIN'
          value: auth0Domain
        }
        {
          name: 'AUTH0CLIENTID'
          value: auth0ClientId
        }
        {
          name: 'RESEND_API_KEY'
          secretRef: 'resendApiKey'
        }
        {
          name: 'BLOBCONTAINERNAME'
          value: blobContainerName
        }
        {
          name: 'DB_SERVER'
          value: dbServer
        }
        {
          name: 'DB_PORT'
          value: dbPort
        }
        {
          name: 'DB_USERNAME'
          value: dbUsername
        }
        {
          name: 'DB_PASSWORD'
          secretRef: 'dbPassword'
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: image
          resources: {
            cpu: 0.5
            memory: '1.0Gi'
          }
          probes: [
            {
              name: 'httpProbe'
              type: 'Liveness'
              httpGet: {
                path: '/'
                port: 80
              }
              initialDelaySeconds: 5
              periodSeconds: 10
            }
          ]
        }
      ]
    }
  }
}

output containerAppUrl string = 'https://${containerApp.properties.configuration.ingress.fqdn}'
