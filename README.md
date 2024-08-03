Sistema de Agendamento de Consultas.
Este sistema foi desenvolvido utilizando a plataforma .NET e permite a gestão de médicos e pacientes, bem como o agendamento de consultas. Abaixo, você encontrará uma descrição detalhada dos requisitos funcionais e dos endpoints da API disponíveis para interação via Swagger.

Requisitos Funcionais:

Cadastro de Usuário (Médico)
Descrição: O médico pode se cadastrar preenchendo os seguintes campos obrigatórios:
Nome
CPF
Número CRM
E-mail
Senha

Autenticação de Usuário (Médico)
Descrição: O médico pode fazer login utilizando o e-mail e a senha. Um token de autorização será retornado.
Cadastro/Edição de Horários Disponíveis (Médico)
Descrição: O médico pode cadastrar e editar seus horários disponíveis para agendamento de consultas.

Cadastro de Usuário (Paciente)
Descrição: O paciente pode se cadastrar preenchendo os seguintes campos:
Nome
CPF
E-mail
Senha

Autenticação de Usuário (Paciente)
Descrição: O paciente pode fazer login utilizando o e-mail e a senha. Um token de autorização será retornado.

Busca por Médicos (Paciente)
Descrição: O paciente pode visualizar a lista de médicos disponíveis.

Agendamento de Consultas (Paciente)
Descrição: Após selecionar um médico, o paciente pode visualizar a agenda do médico com os horários disponíveis. Ao agendar uma consulta, um e-mail será enviado ao médico com as informações do agendamento.


Endpoints da API:

Cadastro do Médico:
Endpoint: POST /api/healthMed/Medico/cadastrar
Descrição: Registra um novo médico no sistema.
Parâmetros:
{
  "nome": "string",
  "documento": "string",
  "crm": "string",
  "email": "string",
  "senha": "string"
}

![image](https://github.com/user-attachments/assets/c527619a-4cac-4747-b40e-a29d44fd5f56)

Autenticação do Médico:
Endpoint: GET /api/healthMed/Login/medico
Descrição: Autentica um médico e retorna um token de autorização.
Parâmetros:
Email: E-mail do médico
Senha: Senha do médico
Exemplo de Requisição:
GET /api/healthMed/Login/medico?Email=teste%40&Senha=1234

![image](https://github.com/user-attachments/assets/25443665-a656-4e7d-83c5-29d309d64699)

Cadastro de Horários Disponíveis do Medico:
Endpoint: POST /api/healthMed/Medico/cadastrar-agenda
Descrição: Permite ao médico cadastrar novos horários disponíveis.
Parâmetros:
{
  "dataHoraDisponivel": "dd/MM/yyyy HH:mm"
}

![image](https://github.com/user-attachments/assets/33c0d215-1ad2-431e-882f-c4f5dde7dc4b)

Listar Agenda Medico
Endpoint: GET /api/healthMed/Medico/listar-agenda
Descrição: Retorna a agenda do médico logado, incluindo IDs das agendas para edição.
![image](https://github.com/user-attachments/assets/17339bd9-aa36-4621-9eaa-48641212a81f)

Edição dos Horários Cadastrado do Medico:
Endpoint: POST: api/healthMed/Medico/editar-agenda
Descrição: Permite ao médico editar um horário disponível existente.
Parametros: 
{
"idAgenda": "ef1eef01-7b35-4a5f-a043-20988218161c",
"novaDataHoraDisponivel": "20/01/2025 15:30"
}

![image](https://github.com/user-attachments/assets/c211b6d2-2bda-41f6-a64a-b914812621d4)

Cadastro do Paciente:
Endpoint: POST /api/healthMed/Paciente/cadastrar
Descrição: Registra um novo paciente no sistema.
Parâmetros:
Copiar código
{
  "nome": "string",
  "documento": "string",
  "email": "string",
  "senha": "string"
}



![image](https://github.com/user-attachments/assets/4c8ca25c-c5a2-46c5-8312-993cb9af7608)

Autenticação do Paciente:
Endpoint: GET /api/healthMed/Login/paciente
Descrição: Autentica um paciente e retorna um token de autorização.
Parâmetros:
Email: E-mail do paciente
Senha: Senha do paciente
Exemplo de Requisição:
GET /api/healthMed/Login/paciente?Email=teste%40&Senha=teste%40

![image](https://github.com/user-attachments/assets/03a392e0-8755-432e-8d69-f499ee4c2625)

Busca os Médicos e suas Agendas:

Endpoint: GET /api/healthMed/Paciente/buscar-agenda-medico
Descrição: Permite ao paciente visualizar a lista de médicos disponíveis e suas agendas. O ID da agenda selecionada é necessário para o agendamento.

![image](https://github.com/user-attachments/assets/0ebe1e06-fefe-47cf-b503-927b72df2adf)

Agendamento de Consultas:
Endpoint: POST /api/healthMed/Paciente/agendar
Descrição: Permite ao paciente agendar uma consulta com um médico. Um e-mail será enviado ao médico com as informações do agendamento.
Parâmetros:
idAgenda: Identificador único do horário disponível obtido na busca de médicos
Exemplo de Requisição:
POST /api/healthMed/Paciente/agendar?idAgenda=2095b9e9-d4de-43b9-94a1-f18f913bb30a

![image](https://github.com/user-attachments/assets/1b4882ca-a6be-4259-86a8-8271bc8deeb0)


![image](https://github.com/user-attachments/assets/4cc92a04-d879-47ba-966c-de119669a69b)
