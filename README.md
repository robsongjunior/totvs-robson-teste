# Teste para desenvolvedor júnior <h1>
## Solicitações do teste<h2>:heavy_check_mark:
  
###### Cadastro de Usuários<h6>:heavy_check_mark:
Crie uma aplicação que exponha uma API RESTful de criação de usuários e login.
Todos os endpoints devem aceitar e responder somente JSON, inclusive ao responder mensagens de erro.
Todas as mensagens de erro devem ter o formato:
    {"mensagem": "mensagem de erro"}
###### Cadastro <h6> :heavy_check_mark:
•	Criar endpoints para listagem e inserção de usuário e perfil de usuário;
•	A criação de usuário deverá receber um usuário com os campos "nome", "email", "senha", mais uma lista dos objetos "perfis" escolhidos;
•	Os endpoints devem responder o código de status HTTP apropriado;
•	Na criação de usuário, em caso de sucesso, retorne o usuário, mais os campos:
o	id: id do usuário (pode ser o próprio gerado pelo banco, porém seria interessante se fosse um GUID);
o	created: data da criação do usuário;
o	modified: data da última atualização do usuário;
o	last_login: data do último login (no caso da criação, será a mesma que a criação);
o	profiles: lista de objetos perfil relacionados ao usuário;
•	Caso o e-mail já exista, deverá retornar erro com a mensagem "E-mail já existente".
###### Login <h6> :heavy_check_mark:
•	Este endpoint irá receber um objeto com e-mail e senha;
•	Caso o e-mail e a senha correspondam a um usuário existente, retornar igual ao endpoint de criação do usuário;
•	Caso o e-mail não exista, retornar erro com status apropriado mais a mensagem "Usuário e/ou senha inválidos";
•	Caso o e-mail exista, mas a senha não bata, retornar o status apropriado 401 mais a mensagem "Usuário e/ou senha inválidos".
###### Requisitos <h6> :heavy_check_mark:
•	.Net 5 C#;
•	Banco de dados em memória ou Postgres;
•	Persistência com Entity e consultas com Dapper;
•	Entregar um repositório público (github ou bitbucket) com o código fonte;
•	Organizar o projeto em camadas e aplicar boas práticas de desenvolvimento de software.
###### Requisitos desejáveis <h6> :heavy_check_mark:
•	Testes unitários;
•	Criptogafia não reversível (hash) na senha e no token;
•	Utilização de injeção de dependência e aplicação de design patterns;
•	Criar um Dockerfile para executarmos a aplicação via Docker.
  
## Versões <h2>
  
  1. Postgres 13.2 · 2021-02-11
  
  2. .Net 5
  
  3. Visual Studio 2019
  
## Execução <h2>
  
  1. Com o SGBD instalado, crie obanco de dados, usuário e senha de acordo com a ConnectionStrings no appsettings.Development.json, e aplique ao usuário criado a permissão de acesso ao banco.
    
  2. Execute o comando update-database via console.
  
  3. Execute a aplicação via Docker ou IIS.
  
