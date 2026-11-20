# Design

## Context

Ver `proposal.md` — Why. Hoje `ContactGenerator` monta o e-mail diretamente a partir de `person.FirstName` (que herda acentos e, com `--uppercase`, maiúsculas) ou de `_faker.Internet.Email()`, sem nenhuma normalização. O ponto de composição é único, o que permite centralizar a regra sem alterar o contrato da CLI.

## Goals / Non-Goals

**Goals:**

- Garantir a propriedade "e-mail sem diacríticos e com caixa definida" em toda saída de contato, inclusive para consumidores diretos dos geradores (testes).
- Manter o formato `<first_name><4 dígitos>@<domínio>` e a coerência entre e-mail e `first_name` exibido.
- Não adicionar dependências nem alterar comandos, flags, rótulos ou códigos de saída.

**Non-Goals:**

- Validar unicidade, entregabilidade ou existência real do domínio.
- Transliterar caracteres que não decompõem via Unicode (por exemplo `ø`, `ß`); o locale `pt_BR` não os produz.
- Alterar a caixa de `first_name`, `last_name` ou `full_name` (apenas o e-mail é normalizado).

## Decisions

### Normalização dentro de `ContactGenerator`

`ContactGenerator.Generate()` e `GenerateForPerson(...)` passam a devolver o e-mail já normalizado por um método privado `NormalizeEmail(string email, bool uppercase)`.

- **Por quê:** o e-mail é uma propriedade do dado de contato; qualquer consumidor do gerador deve receber o mesmo contrato. Normalizar no `CliDispatcher` deixaria os geradores capazes de emitir e-mails inválidos.
- **Alternativa descartada:** normalizar apenas na escrita da saída da CLI — duplicaria a regra e deixaria os testes de gerador cobrindo um comportamento diferente do real.

### Remoção de diacríticos via decomposição Unicode

`email.Normalize(NormalizationForm.FormD)`, descartar caracteres cuja categoria seja `UnicodeCategory.NonSpacingMark`, e recompor com `NormalizationForm.FormC` (`System.Text` e `System.Globalization`, já no runtime).

- **Por quê:** cobre todos os acentos do pt_BR (`á`, `ã`, `ç`, `é`, `í`, `ó`, `ú`, `â`, `ê`, `ô`) sem tabela manual e sem dependência nova.
- **Alternativa descartada:** dicionário de substituições — frágil e incompleto.

### Caixa aplicada depois da remoção de acentos

A ordem é: montar o e-mail bruto (com o `first_name` já na caixa pedida por `--uppercase`) → remover diacríticos → aplicar `ToLowerInvariant`/`ToUpperInvariant` ao endereço completo (parte local e domínio).

- **Por quê:** aplicar a caixa por último garante que o resultado inteiro obedeça à regra, inclusive o domínio, e que o prefixo seja exatamente o `first_name` exibido sem diacríticos naquela caixa.
- **`/contact`:** sem flag de caixa, chama `Generate()`, que normaliza sempre para minúsculas; `--uppercase` continua não aceito por esse comando.
- **`/person --contact`:** `CliDispatcher.WritePerson` passa `uppercase` (já lido da flag) para `GenerateForPerson(person, uppercase)`.

### Assinaturas

- `Generate()` — sem parâmetros; e-mail sempre minúsculo e sem diacríticos.
- `GenerateForPerson(PersonData person, bool uppercase = false)` — parâmetro opcional preserva compatibilidade com chamadas existentes.

## Risks / Trade-offs

- [Domínio em maiúsculas em `/person --uppercase --contact`] → Decisão explícita do contrato ("e-mail todo em maiúsculo"); domínios são case-insensitive, então o endereço permanece utilizável. Documentado nos cenários da spec.
- [Normalização altera o prefixo em relação ao `first_name` exibido quando há acentos] → Esperado e coberto pela spec: a comparação passa a ser feita após remover diacríticos.
- [Caracteres que não decompõem (`ø`, `ß`)] → Fora do locale `pt_BR`; se aparecerem, permanecem no e-mail. Sem impacto prático no escopo.
- [Testes dependentes de aleatoriedade] → Usar `PersonData` fixo com acentos para casos determinísticos e asserções de propriedade (e-mail igual à própria versão normalizada) nos laços existentes de `/contact`.
