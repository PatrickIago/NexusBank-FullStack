# 🏦 NexusBank - Full Stack System

O **NexusBank** é uma plataforma bancária de alta performance desenvolvida para gerenciar transações financeiras e oferecer suporte administrativo avançado. O projeto utiliza uma arquitetura robusta em .NET no backend e uma interface imersiva em Angular no frontend.

---

## 🚀 Tecnologias Utilizadas

O ecossistema do projeto foi construído com as melhores práticas de mercado:

* **Backend:** .NET 8 (Web API) seguindo princípios de **DDD (Domain Driven Design)**.
* **Frontend:** Angular 12 com interface baseada em **Glassmorphism**.
* **Persistência:** SQL Server com Entity Framework Core.
* **Containerização:** Docker e Docker Compose para orquestração de serviços.
* **Relatórios:** QuestPDF para comprovantes e CsvHelper para exportação de dados.

---

## 🐳 Infraestrutura com Docker

O projeto está totalmente "dockerizado", permitindo subir todo o ambiente (Banco de Dados + API + Frontend) com apenas um comando:

```bash
docker-compose up -d

📦 Estrutura da API (Endpoints)A API do NexusBank é dividida em áreas estratégicas para garantir a segurança e a rastreabilidade das operações.RecursoMétodoRotaDescriçãoTransferênciaPOST/api/Transferencias[CREATE] Realiza um novo Pix (Valida saldo e destino).GET/api/Transferencias[READ ALL] Lista todas as transações do banco.SuporteGET/api/Suporte/saldo[READ] Consulta posição consolidada por documento.GET/api/Suporte/transacoes[READ] Obtém histórico completo de um cliente.GET/api/Suporte/comprovante-pdf[FILE] Gera comprovante de transação em PDF.GET/api/Suporte/exportar-usuarios[FILE] Exporta base regional de usuários em CSV.UsuárioPOST/api/Usuario[CREATE] Cadastra um novo cliente no sistema.GET/api/Usuario/{id}[READ ONE] Retorna dados detalhados do perfil.PUT/api/Usuario/{id}[UPDATE] Atualiza informações cadastrais.EndereçoPOST/api/Endereco[CREATE] Vincula endereço ao perfil do usuário.
