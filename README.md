Sistema de Agendamento de Consultas
Bem-vindo ao sistema de agendamento de consultas! 
Este sistema foi desenvolvido utilizando a plataforma .NET e permite a gestão de médicos e pacientes, bem como o agendamento de consultas. 
Abaixo estão descritos os requisitos funcionais e os endpoints disponíveis para interação com a API via Swagger.

Requisitos Funcionais:

Cadastro do Usuário (Médico)
O médico deve poder se cadastrar preenchendo os seguintes campos obrigatórios: Nome, CPF, Número CRM, E-mail e Senha.

![image](https://github.com/user-attachments/assets/c527619a-4cac-4747-b40e-a29d44fd5f56)

Autenticação do Usuário (Médico)
O sistema deve permitir que o médico faça login utilizando o E-mail e Senha. 
Onde será Retornado o token para autorização.

![image](https://github.com/user-attachments/assets/25443665-a656-4e7d-83c5-29d309d64699)

Cadastro Horários Disponíveis (Médico)
O médico deve poder cadastrar seus horários disponíveis para o agendamento de consultas.

![image](https://github.com/user-attachments/assets/33c0d215-1ad2-431e-882f-c4f5dde7dc4b)

Cadastro do Usuário (Paciente)
O paciente deve poder se cadastrar preenchendo os campos: Nome, CPF, Email e Senha.

![image](https://github.com/user-attachments/assets/4c8ca25c-c5a2-46c5-8312-993cb9af7608)

Autenticação do Usuário (Paciente)
O sistema deve permitir que o paciente faça login utilizando o E-mail e Senha. Onde será retornado o token de autorizações.
![image](https://github.com/user-attachments/assets/03a392e0-8755-432e-8d69-f499ee4c2625)

Busca por Médicos (Paciente)
O paciente deve poder visualizar a lista de médicos disponíveis. Deve obeter o id da consulta desejada para ser usada no agendamento.

![image](https://github.com/user-attachments/assets/0ebe1e06-fefe-47cf-b503-927b72df2adf)

Agendamento de Consultas (Paciente)
Após selecionar um médico, o paciente deve poder visualizar a agenda do médico com os horários disponíveis e efetuar o agendamento informando o id da agenda.

![image](https://github.com/user-attachments/assets/1b4882ca-a6be-4259-86a8-8271bc8deeb0)


Endpoints da API

Autenticação do Médico:
Endpoint: GET /api/healthMed/Login/medico
Parâmetros:
Email: E-mail do médico
Senha: Senha do médico
Exemplo de Requisição:
GET /api/healthMed/Login/medico?Email=teste%40&Senha=1234

Autenticação do Paciente:
Endpoint: GET /api/healthMed/Login/paciente
Parâmetros:
Email: E-mail do paciente
Senha: Senha do paciente
Exemplo de Requisição:
GET /api/healthMed/Login/paciente?Email=teste%40&Senha=teste%40


Cadastro do Médico:
Endpoint: POST /api/healthMed/Medico/cadastrar
Parâmetros:
{
  "nome": "string",
  "documento": "string",
  "crm": "string",
  "email": "string",
  "senha": "string"
}

Cadastro do Paciente:
Endpoint: POST /api/healthMed/Paciente/cadastrar
Parâmetros:
Copiar código
{
  "nome": "string",
  "documento": "string",
  "email": "string",
  "senha": "string"
}


Cadastro/Edição de Horários Disponíveis:
Endpoint: POST /api/healthMed/Medico/cadastrar-agenda
Parâmetros:
{
  "dataHoraDisponivel": "dd/MM/yyyy HH:mm"
}


Busca por Médicos:

Endpoint: GET /api/healthMed/Paciente/buscar-medico
Descrição: Permite ao paciente visualizar a lista de médicos disponíveis.


Agendamento de Consultas:
Endpoint: POST /api/healthMed/Paciente/agendar
Parâmetros:
idAgenda: Identificador único do horário disponível
Exemplo de Requisição:
POST /api/healthMed/Paciente/agendar?idAgenda=2095b9e9-d4de-43b9-94a1-f18f913bb30a
