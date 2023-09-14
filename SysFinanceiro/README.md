# Documentação Sitema Fina-Invest

Este guia descreve os passos necessários para configurar e iniciar o projeto XYZ em seu ambiente de desenvolvimento. Certifique-se de seguir essas etapas cuidadosamente para iniciar o projeto com sucesso.

## Clonar o Repositório

Clone o repositório do projeto para o seu ambiente local

## Criar o Banco de Dados

1. **Definir o Projeto Inicial:**

   Certifique-se de definir o projeto "apis" como o projeto inicial.

2. **Acessar o Console do Gerenciador de Pacotes:**

   Abra o Console do Gerenciador de Pacotes (Package Manager Console) no Visual Studio.

3. **Definir o Projeto Padrão para Infraestrutura:**

   No console, defina o projeto padrão para "Infraestrutura\Infra" 
   usando o seguinte comando:
		Add-Migration NomeDa migration -Context ContextBase
		Update-database -Context ContextBase

## Criar usuario

Inicia a PI vai na documentação procura adicionar usuário e adiciona seu usuário, com e-mail, senha ou CPF 

Após isso vai no visual Code abre o projeto fronte ente rode o comando NG S para subir o ângulo entra com o login com o e-mail senha você cadastrou através da PI

## Funcionalidade do sistema 

1. **Dashboard:** Mostra só o início do sistema
2. **Sistema:** Serve para cadastrar novos sistemas onde vai dar novas permissões de usuários para outros sistemas Ex: sistema financeiro casa, sistema financeiro serviço, só precisa preencher o nome
3. **Categoria:** Serve para criar categorias do sistema, tipo categorias pode ser por departamentos e etcs, precisa do nome da categoria e qual sistema vai escolher
4. **Despesa:** Serve para criar pequenas despesas com os seguintes dados, nome, valor, data de vencimento, qual categoria foi escolhida e se está pago ou não e salva no banco de dados
5. **CDB;** Este módulo é de simular investimentos onde não tem interação com o banco, somente com a PI sem persistência de dados, você coloca o valor e a quantidade de meses e clica no calcular e ele te dá o retorno dessas informações
6. **Sair:** Opção de sair do sistema

## Informações Técnicas

1. **Banco de Dados:** SQL Server.
2. **Back-End:** C# 7.0
3. **Front-End:** Angular 16.1.2
4. **Arquitetura:** DDD (Domain-Driven Design)
5. **Autenticação:** JWT (JSON Web Token)
6. **Teste Unitário:** Xunit para as classes de serviços.

