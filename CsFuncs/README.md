# Create and Run azure functions locally

Create project:

```bash
# first isntall dotnet workload
func worload install dotnet

# create the project
func init CsFuncs --stack dotnet

# cd into project
cd CsFuncs


# Search for available templates as they can change name
func new --list                                
Templates for stack: dotnet  (language: c#)
╭──────────────────────────────────────┬──────────────────────────────┬─────────────╮
│ NAME                                 │ TEMPLATE ID                  │ DESCRIPTION │
├──────────────────────────────────────┼──────────────────────────────┼─────────────┤
│ BlobTrigger                          │ blob                         │             │
│ CosmosDBTrigger                      │ cosmos                       │             │
│ DaprPublishOutputBindingIsolated     │ daprPublishOutputBinding     │             │
│ DaprServiceInvocationTriggerIsolated │ daprServiceInvocationTrigger │             │
│ DaprTopicTriggerIsolated             │ daprTopicTrigger             │             │
│ DurableFunctionsEntityClass          │ durableentityclass           │             │
│ DurableFunctionsEntityFunction       │ durableentityfunction        │             │
│ DurableFunctionsOrchestration        │ durable                      │             │
│ EventGridBlobTrigger                 │ eventgridblob                │             │
│ EventGridTrigger                     │ eventgrid                    │             │
│ EventHubTrigger                      │ eventhub                     │             │
│ HttpTrigger                          │ http                         │             │
│ KustoInputBindingIsolated            │ kustoinput                   │             │
│ KustoOutputBindingIsolated           │ kustooutput                  │             │
│ McpPromptTrigger                     │ mcpprompttrigger             │             │
│ McpResourceTrigger                   │ mcpresourcetrigger           │             │
│ McpToolTrigger                       │ mcptooltrigger               │             │
│ MySqlInputBindingIsolated            │ mysqlinput                   │             │
│ MySqlOutputBindingIsolated           │ mysqloutput                  │             │
│ MySqlTriggerBindingIsolated          │ mysqltrigger                 │             │
│ QueueTrigger                         │ queue                        │             │
│ RabbitMQTrigger                      │ rqueue                       │             │
│ ServiceBusQueueTrigger               │ squeue                       │             │
│ ServiceBusTopicTrigger               │ stopic                       │             │
│ SignalRTrigger                       │ signalr                      │             │
│ SqlInputBindingIsolated              │ sqlinput                     │             │
│ SqlOutputBindingIsolated             │ sqloutput                    │             │
│ SqlTriggerBindingIsolated            │ sqltrigger                   │             │
│ TimerTrigger                         │ timer                        │             │
╰──────────────────────────────────────┴──────────────────────────────┴─────────────╯
```

Create an azure function:

```bash
func new --template "http" --name CsHttpTrigger
```

And run the project locally:

```bash
func run
```

>[!Note]
> "azurite" should be installed in your system: `pnpm add -g azurite`
