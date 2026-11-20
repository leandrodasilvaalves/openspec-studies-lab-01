# Design

## Context

A CLI já implementa `/guid --uppercase` com `IdentifierGenerator.Format(..., uppercase)` usando `ToUpperInvariant()`. O comando `/person` ainda não tem flag de caixa. A uniformização exige que `/person` use o mesmo nome de flag (`--uppercase`) e a mesma semântica de caixa invariante. Ver `proposal.md` para motivação.

## Goals / Non-Goals

**Goals:**
- Adicionar `--uppercase` a `/person`, aplicando caixa invariante a `first_name`, `last_name` e `full_name`.
- Expor a opção no `PersonGenerator` (parâmetro opcional) para testabilidade.
- Manter o comportamento padrão inalterado quando a flag está ausente.

**Non-Goals:**
- Não alterar o locale ou a geração base do Bogus.
- Não adicionar caixa alta a `/contact` ou a documentos.
- Não unificar flags de outros comandos além do nome (`--uppercase` já existe para `/guid`).

## Decisions

- **Flag uniformizada como `--uppercase` (não `--upper`)**: mantém consistência com `/guid` e evita proliferar nomes de flag. Alternativa considerada: `--upper` (rejeitada por inconsistência).
- **Caixa invariante via `ToUpperInvariant()`**: espelha `IdentifierGenerator.Format` e garante resultados estáveis independente do ambiente/cultura. Alternativa `ToUpper()` (cultura corrente) rejeitada por variabilidade em acentos.
- **Opção B — parâmetro no gerador**: `PersonGenerator.Generate(bool uppercase = false)` aplica a caixa dentro do gerador, preservando default e permitindo testes isolados. Alternativa (só no dispatcher) rejeitada por menor testabilidade.
- **Rótulos permanecem minúsculos**: apenas valores são uppercased, preservando o parser `label: value` e a spec de formato de saída.
- **`full_name` composto após a caixa**: `full_name = $"{firstName} {lastName}"` com ambos já uppercased (ou caixa aplicada ao conjunto), garantindo consistência.

## Risks / Trade-offs

- [Risco] Testes existentes usam `/person --uppercase` como exemplo de flag não suportada → Mitigação: atualizar o cenário para outra combinação inválida (ex.: `/person --no-punctuation`).
- [Risco] Acentos em nomes pt_BR podem ter mapeamento de caixa não óbvio → Mitigação: usar `ToUpperInvariant()` e cobrir com testes de execução real.
- [Trade-off] Adicionar parâmetro ao gerador altera a assinatura pública, mas default preserva compatibilidade.

## Migration Plan

1. Atualizar `PersonGenerator.Generate` com parâmetro `uppercase`.
2. Atualizar `CliDispatcher` para aceitar `--uppercase` em `/person` e propagar a flag.
3. Atualizar linha de uso e (se necessário) help.
4. Ajustar testes de flag não suportada.
5. Executar `dotnet test`.

## Open Questions

Nenhuma.
