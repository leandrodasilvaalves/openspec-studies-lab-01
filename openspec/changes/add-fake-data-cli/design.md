# Design

## Context

O repositório já contém `src/FakeDataCli` (console `net10.0`, `Bogus` 35.6.5) e `tests/FakeDataCli.Tests` (xUnit 2.9.3), ambos ainda no template inicial. `Program.cs` apenas imprime `Hello, World!`. A motivação e o escopo estão em `proposal.md`; o contrato observável está nas quatro specs do delta. Não há specs publicadas nem consumidores existentes da CLI.

## Goals / Non-Goals

**Goals:**

- Manter a superfície da CLI em inglês, com `/help` como comando de ajuda.
- Manter zero dependências novas além das já referenciadas.
- Tornar a geração de CPF/CNPJ verificável por um validador independente do gerador.
- Permitir que cada comando e cada flag sejam testados isoladamente.

**Non-Goals:**

- Não introduzir framework de linha de comando, parser de argumentos de terceiros ou configuração externa.
- Não gerar atributos adicionais de pessoa/contato além dos cinco campos pedidos (`first_name`, `last_name`, `full_name`, `phone`, `email`).
- Não criar comandos interativos, modo REPL ou persistência de dados.
- Manter `cpf` e `cnpj` como os únicos termos em português da interface; todos os demais nomes de campos são em inglês (`first_name`, `last_name`, `full_name`, `phone`, `email`).

## Decisions

**1. Parsing manual de argumentos em um despachador próprio.**
`Program.cs` permanece com top-level statements e delega para um despachador que valida o primeiro argumento contra a lista de comandos e interpreta flags nomeadas. Alternativa considerada: pacote `System.CommandLine`, rejeitada porque exigiria nova dependência e porque as regras de flags por comando são simples o suficiente para serem explícitas no código.

**2. Uma única instância de `Faker` com locale `pt_BR`.**
Os geradores de pessoa e contato compartilham um `Faker("pt_BR")`, evitando configuração duplicada e garantindo consistência de locale. Alternativa considerada: um `Faker` por comando, descartada por não trazer benefício observável.

**3. CPF/CNPJ calculados localmente, sem depender de gerador do Bogus.**
São sorteados os dígitos base e calculados os dois verificadores com os pesos oficiais (módulo 11). Sequências com todos os dígitos iguais são rejeitadas e sorteadas novamente. Alternativa considerada: usar um gerador pronto de documento da biblioteca, descartada porque a biblioteca não oferece garantia explícita dos verificadores para ambos os documentos nesta versão, e o critério de aceitação exige validade oficial.

**4. Validação independente nos testes.**
Os testes usam um validador próprio, separado do código de geração, para confirmar os dígitos verificadores de CPF e CNPJ. Assim, um erro de cálculo no gerador não é mascarado por reutilizar a mesma rotina.

**5. Formatação como funções puras.**
A pontuação de CPF/CNPJ, a remoção de traços e a caixa do GUID são aplicadas por funções que recebem o valor e as flags e retornam uma string. Alternativa considerada: formatação dentro de cada gerador, descartada por dificultar testes isolados das flags. O GUID usa os formatos `D` (com traços) e `N` (sem traços) e conversão de caixa invariante.

**6. Convenção de saída e códigos de saída.**
Dados e ajuda vão para stdout; erros de comando/flag vão para stderr, acompanhados do uso correto. Sucesso retorna `0`; argumento inválido retorna `2`. Comandos com múltiplos campos imprimem `label: value` por linha, com rótulos `first_name`, `last_name`, `full_name`, `phone` e `email`; comandos de campo único imprimem somente o valor.

**7. Nomenclatura da interface.**
Comandos, flags e nomes de campos em inglês (`/person`, `/contact`, `/guid`, `--no-punctuation`, `--no-dashes`, `--uppercase`, `first_name`, `last_name`, `full_name`, `phone`, `email`), com `/help` no lugar de `--help`. `cpf` e `cnpj` permanecem por serem siglas de documentos brasileiros sem tradução.

## Risks / Trade-offs

- **[Risco] CPF/CNPJ com sequência repetida podem passar no cálculo dos verificadores mas serem inválidos para fins oficiais** → Mitigação: rejeitar bases com todos os dígitos iguais antes de calcular os verificadores e cobrir isso em teste.
- **[Risco] Validador dos testes replicar o mesmo erro do gerador** → Mitigação: implementar a validação nos testes de forma independente e incluir casos conhecidos com dígitos verificadores já estabelecidos.
- **[Risco] Formato de `phone` do Bogus pt_BR variar entre versões** → Mitigação: validar a presença de DDD e separador, em vez de fixar uma máscara única.
- **[Risco] Flags combinadas ou repetidas produzirem saída ambígua** → Mitigação: tratar cada flag como booleana independente e interpretar repetição como um único efeito.
- **[Trade-off] Parser manual não oferece sugestões automáticas de comandos** → Aceito para evitar nova dependência; a mensagem de uso lista todas as opções.

## Migration Plan

Não há migração de consumidores: a CLI ainda não tem comportamento publicado. A implementação substitui o `Hello, World!` de `Program.cs` e o teste vazio `UnitTest1.cs`. Em caso de necessidade de reversão, basta restaurar esses dois arquivos ao estado de template.
