# Design

## Context

O `/person` já compõe a saída em `CliDispatcher.WritePerson`, imprimindo os campos de pessoa e, opcionalmente, contato via `--contact`. O CPF já é gerado e formatado por `BrazilianDocumentGenerator.GenerateCpfDigits`/`FormatCpf`, usados por `/cpf`. O `PersonData` não carrega documento, e o CPF não tem relação com os dados biográficos gerados. Veja `proposal.md` para a motivação.

## Goals / Non-Goals

**Goals:**

- Adicionar `--cpf` à lista de flags aceitas por `/person`.
- Imprimir `cpf` pontuado e válido ao final da saída de pessoa, sem duplicar a lógica de geração.
- Manter as flags existentes independentes e combináveis, com ordem de saída estável.

**Non-Goals:**

- Alterar `PersonData` para transportar CPF ou associar CPF a um nome específico.
- Permitir `--no-punctuation` em `/person` ou criar novas opções de formatação para o CPF de pessoa.
- Alterar `/contact`, `/cpf` ou a assinatura pública dos geradores.

## Decisions

**Gerar o CPF no dispatcher reutilizando `BrazilianDocumentGenerator`.** `WritePerson` passa a checar `--cpf` e, quando presente, chama `GenerateCpfDigits` e `FormatCpf`, imprimindo `cpf: <valor>`. Alternativa considerada: mover a geração para `PersonGenerator` e ampliar `PersonData`; isso acoplaria documento a dados biográficos sem necessidade e obrigaria a ajustar os testes do gerador. O dispatcher já orquestra a composição da saída e é o ponto natural para combinar geradores.

**`--cpf` adiciona apenas o CPF, sem implicar contato.** A leitura escolhida pelo usuário mantém as flags ortogonais: `--contact` controla `phone`/`email`, `--cpf` controla `cpf`. Alternativa descartada: fazer `--cpf` implicar os campos de contato, o que duplicaria a responsabilidade de `--contact` e surpreenderia quem combina as flags.

**Ordem fixa: pessoa, contato (se houver), CPF por último.** A ordem não depende da ordem dos argumentos. Isso mantém a saída determinística e coloca o documento ao final, como no pedido original ("contato mais o documento"). A implementação deve imprimir `cpf` depois do bloco de contato.

**CPF sempre pontuado em `/person`.** `--no-punctuation` continua na lista de flags de documento (`/cpf`, `/cnpj`) e, portanto, é rejeitada em `/person` pela validação existente de `RejectUnsupportedFlags` assim que `--cpf` é adicionado a `PersonFlags`. Alternativa descartada: reutilizar a flag de pontuação em `/person`, o que ampliaria o contrato sem pedido explícito e exigiria mais cenários.

**Atualizar uso/ajuda junto com a validação.** A linha de `/person` em `WriteUsage` passa a listar `--cpf`, mantendo `/help` e a invocação sem argumentos consistentes com as flags aceitas.

## Risks / Trade-offs

- **Saída com três flags (`--uppercase --contact --cpf`) cresce para seis linhas** → a ordem fixa e os testes de saída exata cobrem a combinação e evitam regressões.
- **Teste de CPF pode acoplar-se ao formato** → validar tanto o padrão pontuado quanto os dígitos verificadores com o `DocumentValidator` já existente nos testes.
- **Duplicação da geração de CPF em dois pontos** → aceitável porque ambos chamam as mesmas funções puras do gerador; não há regra de negócio duplicada.
