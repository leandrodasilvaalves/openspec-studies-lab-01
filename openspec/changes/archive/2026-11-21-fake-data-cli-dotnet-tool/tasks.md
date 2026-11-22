# Tasks

## 1. Empacotamento e metadados da ferramenta

- [x] 1.1 Adicionar ao `src/FakeDataCli/FakeDataCli.csproj` os metadados de tool: `PackAsTool=true`, `ToolCommandName=faker`, `PackageId=fakedatacli`, `Version=1.0.0` e `Description`/`Authors`/`Title`/`Summary` — verificar com `dotnet build` (saída 0) e `dotnet run --project src/FakeDataCli -- /person` que o contrato de `cli-command-interface` segue intacto

## 2. Pacote publicável

- [x] 2.1 Empacotar com `dotnet pack src/FakeDataCli` — verificar que `src/FakeDataCli/bin/Release/fakedatacli.1.0.0.nupkg` é gerado e contém `tools/net10.0/any/DotnetToolSettings.xml` com `Command Name="faker"` (packageType `DotnetTool`, id `fakedatacli` v1.0.0)

## 3. Invocações da ferramenta `faker`

- [x] 3.1 Instalar globalmente: `dotnet tool install -g fakedatacli --source src/FakeDataCli/bin/Release` — verificar com `dotnet tool list -g` que a ferramenta com comando `faker` aparece
- [x] 3.2 Verificar `faker /person --uppercase` — sai 0 e imprime `first_name`, `last_name`, `full_name` com caixa alta
- [x] 3.3 Verificar `faker /cpf --no-punctuation` — sai 0 e imprime apenas os dígitos, seguindo o contrato de `cli-command-interface`
- [x] 3.4 Verificar `faker /person --contact` — sai 0 e imprime `first_name`, `last_name`, `full_name`, `phone` e `email`
- [x] 3.5 Verificar `faker` sem argumentos — sai 0 apresentando a mensagem de uso (contrato de `cli-command-interface`)

## 4. Documentação

- [x] 4.1 Atualizar `AGENTS.md`: documentar a ferramenta `faker` — instalação via `dotnet tool install`, a forma de invocação (`faker /{comando}`) e o change `openspec/changes/fake-data-cli-dotnet-tool/` — verificar relendo o arquivo

## 5. Validação total

- [x] 5.1 Rodar `dotnet test` — todos os testes passam, confirmando que nenhum contrato de `cli-command-interface`, `person-contact-data`, `brazilian-documents` ou `identifier-generation` foi quebrado
