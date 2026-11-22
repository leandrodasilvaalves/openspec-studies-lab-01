# FakeDataCli

Gerador de dados fakes brasileiros com Bogus (locale `pt_BR`) — disponibilizado como ferramenta .NET (`fakedatacli`).

## Comandos CLI

- `/person [--uppercase] [--contact] [--cpf]` — `first_name`, `last_name`, `full_name` e campos opcionais.
- `/contact` — `phone` e `email`.
- `/cpf [--no-punctuation]` — CPF válido (pontuado por padrão).
- `/cnpj [--no-punctuation]` — CNPJ válido (pontuado por padrão).
- `/guid [--no-dashes] [--uppercase]` — GUID (com traços, minúsculas, por padrão).
- `/help` — uso.

Saídas em inglês; `cpf` e `cnpj` são siglas dos documentos brasileiros.

## Ferramenta .NET (`faker`)

- **Instalação (desenvolvimento):** `dotnet tool install -g fakedatacli --source src/FakeDataCli/bin/Release` — o nupkg é gerado por `dotnet pack src/FakeDataCli`.
- **Invocação:** `faker /{comando}` — comando da ferramenta (`ToolCommandName=faker`, PackageId `fakedatacli`), com o mesmo contrato de `cli-command-interface`.
- A forma `dotnet faker` não é resolvida pela CLI do dotnet: a tool tem um único comando, e a CLI busca o executável `dotnet-faker` na PATH — invocamos a tool como `faker`.

## Publicação no NuGet.org

`NUGET_API_KEY="..." ./publish-to-nuget.sh` — publica `src/FakeDataCli/bin/Release/fakedatacli.<versão>.nupkg` no NuGet.org. A chave precisa ser de escopo "Push new packages" da sua conta nuget.org.

## Desenvolvimento

- Build: `dotnet build`
- Testes: `dotnet test`
- Execução local: `dotnet run --project src/FakeDataCli -- /person`
