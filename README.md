# popMongo
Serviço de armazenamento de dados rápidos para o RealTime em MongoDB

## Variaveis de ambiente para o Docker
```bash
MONGO_SERVER_HOST               # Apontar para o IP do banco MongoDB
Default: localhost

MONGO_SERVER_DATABASE_NAME      # Apontar o nome do banco
Default: realtime

MONGO_SERVER_COLLECTION         # Apontar o nome da "tabela" (Collection)
Default: veiculos

MONGO_SERVER_PORT               # Apontar a porta do servidor do MongoDB
Default: 27017

MONGO_SERVER_USER               # Apontar o usuario do MongoDB
Default: root

MONGO_SERVER_PASSWORD           # Apontar a senha do usuario acima
Default: rootpass

MONGO_SERVER_AUTH_METHOD        #Apontar o método de autenticação q vai na string de conexão do MongoDB
Default: ?authenticationDatabase=admin

RABBIT_HOST                     # Apontar o IP do servidor de RabbitMQ
Default: CETURB

RABBIT_TOPIC                    # Apontar o nome do topico a ser escutado
Default: CETURB

RABBIT_CHANNEL                  # Apontar o nome do canal no tópico
Default: realtime.mongo

RABBIT_KEY                      # Apontar a chave de roteamento a ser usada para ouvir
Default: "#"

NODE_ENV                        # Em produção, setar o valor production
Default: null
```

## Requiriments para produção
para rodar em produção, basta o .Net Core 2.1 instalado, além do MongoDB e do rabbit.

### Requiriments para desenvolvimento
Para rodar localmente em ambiente de teste, é necessário
.Net Core 2.1 (para rodar o app principal)
Node 8+ (para usar os scripts pré configurados)
Docker (para subir os serviços de RabbitMQ e MONGOServer)



## Ferramentas de teste
Iniciar o banco docker de teste
```bash
npm run mongo
```

Iniciar o RabbitMQ de teste
```bash
npm run rabbit
```

Depois de subir o banco e o rabbit, é só startar a aplicação
```bash
dotnet run
```
o dotnet run já baixa as dependencias e executa tudo sozinho.


Para ver funcionando, vc pode executar scripts js que envia uma ou milhares de mensagens de uma vez só para o RabbitMQ.
```bash
npm run send:rabbit         # Envia uma mensagem simples ao Rabbit
npm run sendflood:rabbit    # Envia milhares de mensagens ao rabbit (situação próxima do real)
```

Parar o banco docker de teste (exclui os dados)
```bash
npm run stop:mongo
```

Parar todos os containers
```bash
npm run stopdocker
```
