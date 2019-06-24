## Sobre o Projeto
Este projeto foi desenvolvido com o intuíto de facilitar o processo de criação de novas API's para projetos pequenos de estudo.

## Sobre a arquitetura
O Projeto foi desenvolvido utilizando a arquitetura de Microserviços, afim de facilitar a implementação de novas features e agilizar o processo de correção de bugs. Para realizar requisições foi usada a arquitetura REST.

## Configurações iniciais necessárias
Para começar a desenvolver neste Scaffold, é necessário somente algumas coisas:
- Alterar o nome da solution principal (NomeAPI.sln) para o nome da futura api;
- Alterar a parte onde diz "Nome da API" na classe de Startup da api para o nome da api desejada, bem como o nome do documento na invocação do serviço;
- Adicionar um arquivo de conexoes.json válido em Comunicacao/.

## conexoes.json
É o arquivo não controlado responsável pelo armazenamento das connection strings ao banco de dados, para criar um novo arquivo, o mesmo deve respeitar as normas propostas na classe Comunicacao.Configuracao.Conexao, seguindo o seguinte modelo:

```
[
	{
		"Nome": "Insira aqui o nome do banco de dados",
		"StringConexao": "Insira aqui a connection string ao banco de dados"
	}
]
```

# Adicionando um novo micro serviço

## Criando o Dominio
Na camada de domínio, crie uma nova pasta com as classes de domínio que representarão a sua aplicação.
