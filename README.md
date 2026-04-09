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
