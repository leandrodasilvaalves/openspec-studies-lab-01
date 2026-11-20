# Tasks

## 1. Geração de contato para pessoa

- [x] 1.1 Criar `ContactGenerator.GenerateForPerson(PersonData person)` em `src/FakeDataCli/Generators/ContactGenerator.cs`, gerando telefone via `Phone.PhoneNumber()` e e-mail no formato `<person.FirstName><4 dígitos>@<domínio Bogus>`, e verificar com teste xUnit que o e-mail corresponde ao padrão esperado
- [x] 1.2 Garantir que `ContactGenerator.Generate()` permaneça inalterado e verificar com teste xUnit que `/contact` continua produzindo e-mail padrão

## 2. Despacho e saída

- [x] 2.1 Adicionar `--contact` às flags aceitas por `/person` em `CliDispatcher` e verificar que qualquer outra flag adicional ainda retorna código 2
- [x] 2.2 Atualizar `WritePerson` para imprimir `phone` e `email` quando `--contact` estiver presente, usando o `PersonData` gerado, e verificar com `dotnet run --project src/FakeDataCli -- /person --contact` que as cinco linhas aparecem na ordem correta
- [x] 2.3 Atualizar a linha de uso de `/person` para `/person [--uppercase] [--contact]` e verificar que `/help` lista `--contact`
- [x] 2.4 Verificar com teste xUnit que `/person` sem `--contact` continua gerando apenas três linhas

## 3. Verificação

- [x] 3.1 Executar `dotnet test` na solução e verificar que todos os testes passam
- [x] 3.2 Executar smoke de `/person`, `/person --contact` e `/person --uppercase --contact` e verificar saída e códigos de saída

## Workflow follow-up

- Arquivar o change após validação e sincronizar as specs principais.
- Verificar o resultado arquivado.
