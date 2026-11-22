# Design

## Context

O projeto consiste em `src/FakeDataCli` (console Exe, `net10.0`, Bogus 35.6.5 com locale `pt_BR`); `Program.cs` delega a `CliDispatcher.Run(args, Console.Out, Console.Error)`. Sem metadados de empacotamento: não há `PackAsTool`, `Version` nem `ToolCommandName`. As operações são comandos slash (`/person`, `/contact`, `/cpf`, `/cnpj`, `/guid`, `/help`) com labels em inglês e códigos de saída 0 (sucesso) e 2 (argumentos inválidos).

## Goals / Non-Goals

**Goals:**
- O pacote gerado pelo `dotnet pack` de `src/FakeDataCli` ser instalável como ferramenta .NET com comando `faker` (instalação com `dotnet tool install -g fakedatacli --source src/FakeDataCli/bin/Release`).
- A aplicação gera dados quando invocada como `faker /{comando}` após instalação global.
- O contrato observável permanece o da `cli-command-interface`: comandos aceitos, flags, saída, códigos de saída.

**Non-Goals:**
- Alterar `Program.cs` ou `CliDispatcher` — o dispatcher não ganha ramos novos (a invocação é única: `faker /{comando}`).
- Publicar o pacote no NuGet.org.
- Mudanças nos geradores ou no formato da saída de dados.
- Suportar outros locales ou novos comandos além dos contratos existentes.

## Decisions

1. **Nome da ferramenta: `faker`** (`ToolCommandName=faker`; `PackageId=fakedatacli` — id único, pois `faker` já é um pacote publicado no NuGet.org sem relação com esta ferramenta). Alternativa considerada: `PackageId=faker` — descartada por colidir com o pacote existente; o nome da ferramenta é o comando da tool, não o id do pacote.
2. **Aplicação invocada como `faker /{comando}`**: o comando `faker` cai na PATH do dotnet (`~/.dotnet/tools`) após a instalação global; nossa aplicação não muda — é o mesmo Exe que aceita os comandos slash. A forma `dotnet faker` não é resolvida pela CLI do dotnet: uma tool tem um único comando, e a CLI do dotnet busca o executável `dotnet-faker` na PATH — invocamos a tool como `faker` (verificado: `faker /person --uppercase`, `faker /cpf --no-punctuation` e `faker` sem argumentos).
3. **Metadados do pacote**: `Version=1.0.0`, `Description`, `Authors`, `Title`, `Summary` no csproj, já que o projeto não define `Version`; a versão inicial pode ser ajustada futuramente.
4. **Sem dependências novas**: Bogus continua a única referência de runtime (constraint de `AGENTS.md`).
5. **`dotnet pack` produz o pacote da tool** — o SDK gera a entrada de tool (`Command Name="faker"` em `DotnetToolSettings.xml`) automaticamente com `PackAsTool=true`; sem configuração extra.
6. **A invocação é única por configuração de ferramenta**: uma tool tem um comando; com `ToolCommandName=faker`, o comando é `faker` — invocamos a aplicação como `faker /{comando}` (verificado).

## Risks / Trade-offs

- **Conflito de nome de pacote**: `faker` já existe no NuGet.org como pacote não-tool; mitigação: `PackageId=fakedatacli` (único) e `ToolCommandName=faker` (nome de comando).
- **Forma `dotnet faker` não resolvida**: a ferramenta tem um único comando (`faker`) e a CLI do dotnet não resolve `dotnet faker` para tools globais sem um executável `dotnet-faker` na PATH — invocamos a tool como `faker /{comando}` (verificado).
- **Estado global**: instalação global altera a máquina do usuário — exigência explícita do usuário para a invocação global.
- **Equivalência de saída**: "equivalência" refere-se à estrutura, rótulos e códigos de saída — dados aleatórios nunca são idênticos entre invocações.

## Migration Plan

- **Desenvolvimento**: `dotnet run --project src/FakeDataCli -- /person` continua funcionando — `OutputType` permanece `Exe` e os códigos de saída não mudam.
- **Instalação do pacote**: `dotnet pack src/FakeDataCli` → `dotnet tool install -g fakedatacli --source src/FakeDataCli/bin/Release`.
- **Rollback**: `dotnet tool uninstall -g fakedatacli` não afeta o código nem o `dotnet run`.
- **Atualização**: `dotnet tool update -g fakedatacli` para novas versões do pacote.
