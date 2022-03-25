# Desenvolvedor bem vindo ao GlobalManage!

Olá **Desenvolvedor**. existe algumas passos antes de começa a desenvolver, primeiro vamos entender um pouco sobre o projeto.

	**Global Manage é um projeto para unidades de escolas, um ERP completo com todos os cadastrados necessario para empresa e também atendimento ao aluno, também possivel realizar pagamento nele utilizando APIs externas de E-commerce, TEF e geração de boleto**.


# Vamos falar de projetos!

**global-manage-front**, um projeto totalmente desenvolvido em angular com material design, ele faz a comunição com back-end do ERP (global-manage-api-web) e também comunica com a API do TEF (global-manage-api-tef) que é instalada local na maquina do usuário (atendente).
**global-manage-api-web**, projeto desenvolvido utilizando .NET Core 3.1, responsável pela a comunicação do com front-end.
**global-manage-api-tef**, projeto desenvolvido utilizando .NET Core 3.1, responsável pela a comunicação com o TEF da linx, no ato da implantação, essa API devera ser instalada na maquina do usuário, ela é responsável pela a comunicação com a maquina de cartão.

## Baixa automatica de boleto Itaú

Dentro do projeto **global-manage-api-web** existe uma pasta chamada serviço, é uma aplicação a parte de "Windows Services", ela deve ser instalada no mesmo servidor onde esta o STCP do Itaú responsável por baixa os arquivos retorno, essa aplicação realiza o envio do arquivo para api atualizar os pagamento de acordo com o "nosso numero" gerado pelo o banco.

Na pasta raiz desse projeto "Windows Services" existe um arquivo chamado "itau_download_ftp.bat", ele deve ser executado no servidor da aplicação para chamar o "STCP Cliente Itaú".

## Pronto, bom trabalho.

