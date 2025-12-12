# Antecipação de recebíveis

<p align="center">
<img src="http://img.shields.io/static/v1?label=STATUS&message=CONCLUIDO&color=GREEN&style=for-the-badge"/>
</p>

## 📋 Pré-requisitos
**-Ter o Visual Studio instalado com suporte ao .NET 8.**<br>
**-Ter o SQL Server instalado e configurado.**<br>

## 📁 Acesso ao projeto
Fazer o clone do projeto com o comando: git clone https://github.com/Rafazz04/AntecipacaoRecebivel.git

## 🛠️ Abrir e rodar o projeto
**-Abra a pasta do projeto AntecipacaoRecebivel**<br>
**-Abra a solução do projeto AntecipacaoRecebivel.sln (Abra com Visual Studio)**<br>
**-Com o projeto aberto vá até a aba Ferramentas -> Gerenciador de pacotes nuget -> abra o Console do gerenciador de pacotes -> Rode o comando Update-Database (Padrão: Autenticação com windows Server:LocalHost)**<br>
**-Depois de rodar o migrations pode executar a aplicação(f5)**<br>

## 🔨 Funcionalidades do projeto
O projeto está organizado em **controllers separados**, cada um responsável por um conjunto de funcionalidades. Todas as operações seguem **CQRS** com **Mediator**.

### Empresa (`CompanyController`)
- **Cadastro de empresa** (POST)  
- **Atualização de empresa** (PUT)  
- **Exclusão de empresa** (DELETE)  

### Notas Fiscais (`InvoiceController`)
- **Cadastro de notas fiscais** (POST)  
- **Remoção de notas fiscais** (DELETE)  

### Carrinhos (`CartController`)
- **Criação de carrinho** (POST)   
- **Checkout do carrinho** (GET)  

## 👨🏻‍💻 Abordagens Técnicas

### Clean Architecture
Adotei a **Clean Architecture** como arquitetura do projeto, visando garantir maior organização e manutenibilidade a longo prazo. As vantagens incluem:

- **Separação de responsabilidades**: Cada camada opera de forma independente, permitindo que mudanças em uma não afetem as demais, promovendo modularidade e segurança.
- **Facilidade de teste**: A divisão clara das camadas facilita a realização de testes isolados, tanto para as regras de negócio quanto para a infraestrutura, garantindo maior confiabilidade.
- **Flexibilidade**: A arquitetura permite trocas simples de implementações, como a substituição de banco de dados ou a integração com novos serviços, sem comprometer a lógica central.

### Repository Pattern
Implementei o **Repository Pattern** para gerenciar as operações com o banco de dados. As vantagens são:

- **Abstração no acesso a dados**: O código de acesso ao banco de dados é desacoplado da lógica de negócio, facilitando mudanças no sistema de armazenamento.
 
### Injeção de Dependência e Inversão de Controle (IoC)
Adotei os padrões de **Injeção de Dependência** e **Inversão de Controle (IoC)**, que proporcionam:

- **Desacoplamento de componentes**: As dependências são injetadas dinamicamente, permitindo fácil substituição sem grandes alterações no código.
- **Facilidade na manutenção e teste**: A injeção de dependências permite simular serviços e repositórios, melhorando a eficiência dos testes e a agilidade na manutenção.

### Code First
Utilizei a abordagem **Code First** com o **Entity Framework**, gerando o banco de dados a partir dos modelos de domínio. O uso de **migrations** permite o controle versionado da evolução do banco de dados, facilitando a sincronização entre diferentes ambientes.

### Validators
Para garantir a validação eficiente dos dados de entrada, implementei a biblioteca **FluentValidation**. Essa abordagem permite criar regras de validação de maneira fluida e expressiva, facilitando a leitura e manutenção do código.



## ✔️ Técnicas e Tecnologias utilizadas

- ``.Net 8``
- ``Enity Framework``
- ``Migrations``
- ``DDD``
- ``Value Objects``
- ``Clean Architecture``
- ``Injeção de dependência``
- ``Inversão de controle``
- ``Repository Pattern``
- ``Code-First``
- ``FluentValidation``
- ``Pipelines``
