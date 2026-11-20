# Proposal

## Why

O repositório já possui a solução .NET 10 com o pacote Bogus instalado, mas `Program.cs` ainda é o template `Hello, World!`. Sem um contrato aprovado, os nomes dos comandos, as flags de formatação e a validade dos documentos brasileiros ficariam como decisões implícitas. Esta mudança define o comportamento observável da CLI antes de qualquer código, seguindo o ciclo especificar → revisar → implementar → validar do laboratório.

## What Changes

- Gerar dados de pessoa (`first_name`, `last_name`, `full_name`) e contato (`phone`, `email`) com Bogus no locale `pt_BR`.
- Gerar CPF e CNPJ com dígitos verificadores matematicamente válidos, com saída pontuada por padrão e opção de omitir a pontuação.
- Gerar GUID com traços e letras minúsculas por padrão, com opções independentes para remover traços e usar maiúsculas.
- Expor as operações como comandos slash em inglês: `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid` e `/help`.
- Usar flags de formatação também em inglês: `--no-punctuation`, `--no-dashes` e `--uppercase`.
- Substituir a saída de exemplo do programa pelo despacho dos comandos, com mensagem de uso e código de saída adequados.
- Cobrir os critérios de aceitação com testes automatizados no projeto xUnit existente.

**Decisão registrada:** `cpf` e `cnpj` são os únicos termos em português da interface, mantidos por serem siglas dos documentos brasileiros. Comandos, flags e nomes de campos são todos em inglês: `first_name`, `last_name`, `full_name`, `phone` e `email`.

## Capabilities

### New Capabilities

- `person-contact-data`: geração de dados falsos de pessoa e contato no locale `pt_BR` (`first_name`, `last_name`, `full_name`, `phone` e `email`).
- `brazilian-documents`: geração de CPF e CNPJ válidos e da opção de pontuação da saída.
- `identifier-generation`: geração de GUID e das opções de traços e caixa da saída.
- `cli-command-interface`: superfície observável da CLI — comandos slash, flags, mensagem de uso e forma da saída.

### Modified Capabilities

Nenhuma. O projeto ainda não possui specs publicadas (`openspec list --specs` não retornou resultados).

## Impact

- **Código:** `src/FakeDataCli/Program.cs` deixa de ser o template padrão; novos tipos para geração, formatação e despacho de comandos em `src/FakeDataCli/`.
- **Testes:** `tests/FakeDataCli.Tests/UnitTest1.cs` é substituído por testes de dígitos verificadores, flags de formatação e despacho da CLI.
- **Dependências:** nenhuma nova. `Bogus` 35.6.5, xUnit 2.9.3 e `Microsoft.NET.Test.Sdk` 17.14.1 já estão referenciados; `TargetFramework` permanece `net10.0`.
- **Compatibilidade:** sem impacto em consumidores — a CLI ainda não tem comportamento publicado. Não há mudanças quebráveis (**BREAKING**).
