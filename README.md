# InvestmentMngmt
## Tecnologias utilizadas: 
- NET 5.0
- Swagger
- Jaeger
- Redis
- Hangfire

## Execução
### Requisitos:
- Docker
- Docker Compose

### Instrução para execução
- ```docker-compose up -d```

Cria todo ambiente com a API, Redis e Jaeger.

## Como acessar
Para acessar o swagger da api: 

- [Swagger](http://localhost/swagger/index.html)

Optei por utilizar o Hangfire para executar o job de limpeza e o job de atualização do cache, mas considerei o Quartz.net, achei mais simples e eficaz o Hangfire.
O job de atualização é executado conforme a expressão cron ("0 0 0 ? * *").
O job de limpeza, precisa ser executado manualmente.

O endereço para acessar a interface do hangfire é:
- [Hangfire](http://localhost/hangfire)

## Observabilidade
Utilizei o Jaeger para atender esse ponto, ele realiza todo o trancing da aplicação e fornece uma interface para busca dos dados.
Para a busca é possível utilizar filtros como:

- reponseCache=true. Requisições que retornaram do cache.
- reponseCache=false. Requisições que não retornaram do cache.
- statusCode. É possível buscar pelos statuscode de erro.

O endereço para acesso à interface do Jaeger é:
- [Jaeger](http://localhost:16686)

## Cache
Utilizei o Redis. Os dados expiram a 00:00.
