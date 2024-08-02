Sistema de Agendamento de Consultas
Bem-vindo ao sistema de agendamento de consultas! 
Este sistema foi desenvolvido utilizando a plataforma .NET e permite a gestão de médicos e pacientes, bem como o agendamento de consultas. 
Abaixo estão descritos os requisitos funcionais e os endpoints disponíveis para interação com a API via Swagger.

Requisitos Funcionais:

Cadastro do Usuário (Médico)
O médico deve poder se cadastrar preenchendo os seguintes campos obrigatórios: Nome, CPF, Número CRM, E-mail e Senha.

Autenticação do Usuário (Médico)
O sistema deve permitir que o médico faça login utilizando o E-mail e Senha.

Cadastro/Edição de Horários Disponíveis (Médico)
O médico deve poder cadastrar e editar seus horários disponíveis para o agendamento de consultas.

Cadastro do Usuário (Paciente)
O paciente deve poder se cadastrar preenchendo os campos: Nome, CPF, Email e Senha.

Autenticação do Usuário (Paciente)
O sistema deve permitir que o paciente faça login utilizando o E-mail e Senha.

Busca por Médicos (Paciente)
O paciente deve poder visualizar a lista de médicos disponíveis.

Agendamento de Consultas (Paciente)
Após selecionar um médico, o paciente deve poder visualizar a agenda do médico com os horários disponíveis e efetuar o agendamento.

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