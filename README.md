# API-PRODUTOS

Essa API foi desenvolvida para realizar o teste tecnico, essa API realizar todo o gerenciamento dos produtos, desde a criação até a compra de produtos.

## Tecnologias Utilizadas

* REST
* C#
* Entity Framework Core
* .Net Core
* MySQL

## Pré-requisitos

* [.NET 7 SDK](https://dotnet.microsoft.com/pt-br/download/).
* MySQL


## Configuração

1. Clone o repositório:

```
git clone https://github.com/theus26/API-Produtos
```

2. Acesse o diretório do projeto:

```
cd seu-repositorio
```

3. Configure as variáveis de ambiente do sistema necessárias.

```
ex: 
Nome Da Variável: PRODUTOS_CONNECTION
Valor da Variavel: Server=localhost;Port=3306; DataBase=Produtos; Uid=root; Pwd=; 
```


4. Execute os seguintes comandos para restaurar as dependências e iniciar a API:

```
dotnet restore
dotnet run
```

5. Acesse a API em http://localhost:porta, onde "porta" é a porta configurada para a sua API.

*Observação: Trocar a Url da API de pagamento no AppSettings*

```
 "Urls": {
    "ApiPayament": "https://localhost:7280/Payament/Compras"
  }
```

## Funcionalidades

A API tem diversas funcionalidades, ela realizar o gerenciamento dos produtos, atravéz dos seguintes endpoints:


Exemplo:

* `POST /Produtos/Products`: Criar Produto
* `GET /Produtos/GetAllProducts`: Retorna uma lista com todos os produtos cadastrados.
* `GET /Produtos/GetProductId/{IdProduto}`: Detalha um produto pelo Id
* `DELETE /Produtos/DeleteProduct/{ProdutoId}`: Deletar um produto pelo seu Id.
* `GET /Produtos/PurchaseProduct/`: Realizar a compra do Produto.



## Banco de Dados

O Entity Framework Core é uma estrutura de mapeamento de objeto/relacional. Ele mapeia os objetos de domínio em seu código para entidades em um banco de dados relacional. Na maior parte do tempo, você não precisa se preocupar com a camada de banco de dados, pois o Entity Framework cuida dela para você. Seu código manipula os objetos e as alterações são persistentes em um banco de dados.

Exemplo:

A API utiliza o Entity Framework Core para se comunicar com o banco de dados. O banco de dados padrão é o Mysql. Para configurar o banco de dados:

1. Crie em seu sistema a variavel de conexão, como mostrado no exemplo anterior, logo acima.

2. Antes de executar as migrations para gerar o banco de dados, certifique-se de que a porta 3306 esteja instanciada para poder usar o MySQL. E a sua connctionString está correta, após isso execute:

```
dotnet ef database update
```
_Esse comando executará todas as migrations criadas e irá gerar toda parte do Banco de Dados._
