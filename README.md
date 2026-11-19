# Mini ERP Varejo

### Experiência de gestão, conhecimento de negócio e tecnologia aplicados ao varejo.

## Sobre o projeto

O Mini ERP Varejo é um projeto de desenvolvimento de uma API REST
voltada ao gerenciamento das operações essenciais de um pequeno
estabelecimento comercial, com foco inicial no segmento supermercadista.

O projeto nasceu da minha experiência de 15 anos como proprietário e
gestor de supermercado, período em que vivenciei diretamente os desafios
operacionais, administrativos, comerciais e de controle de um negócio
varejista.

Minha proposta é transformar essa experiência prática em soluções
tecnológicas, desenvolvendo um sistema que considere não apenas os
aspectos técnicos da programação, mas também as necessidades reais de
quem administra uma operação comercial.

A primeira versão possui escopo experimental e não fiscal, priorizando
os processos fundamentais de cadastro, estoque e vendas.

## Perfil profissional

Minha trajetória profissional combina experiência empresarial, formação
jurídica e desenvolvimento de conhecimentos na área de tecnologia.

-   **Gestão empresarial:** 15 anos de experiência como proprietário e
    gestor de supermercado, com atuação direta na administração e nos
    processos operacionais do varejo.
-   **Formação em Direito:** conhecimento jurídico e visão analítica
    aplicados à compreensão de processos, regras, controles e
    organização.
-   **Ciência de Dados:** atualmente cursando o 3º período, ampliando
    meus conhecimentos em tecnologia, análise de dados e pensamento
    computacional.
-   **Desenvolvimento de software:** desenvolvimento prático de
    aplicações utilizando C#, .NET, ASP.NET Core, Entity Framework Core
    e SQL Server.

Acredito que a experiência de negócio, associada ao conhecimento
técnico, permite desenvolver soluções mais próximas da realidade das
empresas.

## Objetivos do Mini ERP

Desenvolver um sistema de gestão comercial que contemple inicialmente:

-   Organização cadastral de produtos e categorias.
-   Controle e rastreabilidade de estoque.
-   Registro de movimentações de estoque.
-   Gerenciamento de vendas.
-   Controle dos itens comercializados.
-   Aplicação de regras de negócio coerentes com a operação do varejo.

## Tecnologias utilizadas

  Tecnologia              Aplicação
  ----------------------- -------------------------------------------
  C#                      Linguagem de programação
  .NET 10                 Plataforma de desenvolvimento
  ASP.NET Core Web API    Construção da API REST
  Entity Framework Core   Mapeamento e persistência de dados
  SQL Server              Banco de dados relacional
  Swagger / OpenAPI       Documentação e testes da API
  Git                     Versionamento de código
  GitHub                  Hospedagem e gerenciamento do repositório

## Módulos implementados

  -----------------------------------------------------------------------
  Módulo                              Funcionalidades
  ----------------------------------- -----------------------------------
  Categorias                          Cadastro, consulta e atualização

  Produtos                            Cadastro, consulta, atualização e
                                      inativação

  Motivos de ajuste de estoque        Cadastro, consulta, atualização e
                                      inativação

  Movimentações de estoque            Registro e consulta de
                                      movimentações

  Vendas                              Criação e consulta de vendas

  Itens de venda                      Inclusão, consulta, cálculo de
                                      subtotal e atualização do total da
                                      venda
  -----------------------------------------------------------------------

## Regras de negócio

O desenvolvimento considera situações reais da operação de um
supermercado:

-   Separação entre situação cadastral e disponibilidade comercial do
    produto.
-   Bloqueio de vendas de produtos inativos ou indisponíveis.
-   Controle de quantidades e disponibilidade de estoque.
-   Registro histórico das movimentações.
-   Preservação de registros históricos por meio de inativação de
    cadastros.
-   Verificação de estoque antes da inclusão de itens em uma venda.
-   Atualização dos valores dos itens e do total da venda.

## Estrutura do projeto

``` text
MiniERP.Api/
├── Controllers/
├── Data/
├── Domain/
│   └── Entities/
└── Migrations/
```

## Como executar

### Pré-requisitos

-   .NET SDK 10
-   SQL Server
-   Visual Studio Code ou IDE compatível

### Clonar o repositório

``` bash
git clone https://github.com/SEU-USUARIO/MiniERP-Varejo.git
```

### Acessar o projeto

``` bash
cd MiniERP-Varejo
```

### Configurar o banco de dados

Configure a conexão com o SQL Server no arquivo `appsettings.json`,
utilizando suas próprias credenciais e configurações locais.

### Aplicar as migrations

``` bash
dotnet ef database update --project .\MiniERP.Api\MiniERP.Api.csproj
```

### Executar a aplicação

``` bash
dotnet run --project .\MiniERP.Api\MiniERP.Api.csproj
```

A documentação dos endpoints pode ser acessada pelo Swagger, utilizando
o endereço informado no terminal durante a inicialização da API.

## Status do desenvolvimento

**Em desenvolvimento.**

O projeto está sendo construído de forma incremental. Cada módulo passa
por implementação, compilação, testes dos endpoints e versionamento no
Git.

A primeira versão mantém o escopo não fiscal, priorizando a consolidação
das funcionalidades essenciais do sistema.

## Visão profissional

Este projeto representa a convergência entre minha experiência na gestão
empresarial, minha formação jurídica, minha atual formação acadêmica em
Ciência de Dados e meu desenvolvimento técnico na área de software.

Acredito que desenvolver tecnologia para empresas exige mais do que
conhecimento de programação: exige compreender processos, identificar
necessidades, estabelecer regras consistentes e transformar problemas
reais em soluções funcionais.

**Meu objetivo é unir experiência de negócio, capacidade analítica e
desenvolvimento tecnológico para construir soluções que gerem valor para
as organizações.**
