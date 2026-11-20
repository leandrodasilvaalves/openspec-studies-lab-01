# Design

## Context

`/person` atualmente aceita `--uppercase` e gera apenas nome; `/contact` gera telefone e e-mail com o `Faker` pt_BR compartilhado. A nova flag `--contact` deve compor a saída sem alterar `/contact` isolado. Ver `proposal.md` para motivação e os deltas em `specs/` para o contrato observável.

## Goals / Non-Goals

**Goals:**
- Aceitar `--contact` em `/person` e imprimir `phone` e `email` após os campos de pessoa.
- Gerar e-mail no formato `<first_name><4 dígitos>@<domínio Bogus>`.
- Preservar integralmente `/person` sem a flag e `/contact`.

**Non-Goals:**
- Não alterar o e-mail gerado pelo comando `/contact`.
- Não alterar a geração de telefone nem o locale.
- Não alterar o formato de saída dos documentos ou GUID.

## Decisions

- **Flag única `--contact` em `/person`**: consistente com o pedido; `--uppercase` continua independente e pode ser combinada. Alternativa (subcomando `/person-contact`) rejeitada por fugir do padrão de flags da CLI.
- **Novo método em `ContactGenerator` (`GenerateForPerson`)**: recebe o `PersonData` já gerado e produz `phone` + e-mail customizado. Alternativa (alterar `Generate()`) rejeitada porque mudaria o comportamento de `/contact`.
- **Prefixo do e-mail a partir do `first_name` exibido**: quando `--uppercase` está ativo, o `PersonData` já contém o nome em caixa alta, então o e-mail reflete a saída. Alternativa (sempre minúsculo) rejeitada por criar divergência entre `first_name` e e-mail.
- **Domínio aleatório via Bogus (`Internet.DomainName()`)**: atende à escolha de domínio aleatório fornecido pelo Bogus. Alternativa (lista fixa de provedores) rejeitada pelo usuário.
- **4 dígitos via `Randomizer.Number(0, 9999)` com formatação `D4`**: garante exatamente 4 caracteres, incluindo zeros à esquerda.
- **Saída combinada com rótulos existentes**: `first_name`, `last_name`, `full_name`, `phone`, `email`, um por linha, mantendo `ParseLabeled` dos testes.

## Risks / Trade-offs

- [Risco] Nomes com acentos no prefixo do e-mail → Mitigação:manter exatamente o `first_name` exibido e cobrir com teste de regex tolerante a Unicode.
- [Risco] Colisão entre flags não suportadas → Mitigação: manter lista `PersonFlags` com `--uppercase` e `--contact`; rejeitar qualquer outra flag.
- [Trade-off] Novo método público no gerador aumenta a API, mas não quebra chamadas existentes.

## Migration Plan

1. Adicionar `--contact` às flags aceitas por `/person`.
2. Criar `ContactGenerator.GenerateForPerson` e usá-lo apenas no caminho de `/person --contact`.
3. Atualizar saída combinada, uso e help.
4. Adicionar testes e executar `dotnet test`.

## Open Questions

Nenhuma.
