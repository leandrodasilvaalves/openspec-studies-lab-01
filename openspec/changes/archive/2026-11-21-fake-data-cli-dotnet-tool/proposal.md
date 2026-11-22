# Proposal

## Why

O projeto FakeDataCli é uma CLI console .NET 10 funcional (`/person`, `/contact`, `/cpf`, `/cnpj`, `/guid`, `/help`), mas atualmente só pode ser executado via `dotnet run` ou pelo binário local. Não há forma de invocá-la como ferramenta instalada: o usuário precisa estar dentro do projeto e lembrar de um caminho de binário, e não há pacote publicável.

## What Changes

- Empacotar `src/FakeDataCli` como ferramenta .NET: `PackAsTool=true` e `ToolCommandName=faker` no csproj, com metadados de tool (nome, descrição).
- Ativar a invocação `dotnet faker /{comando}` a partir do projeto — já existe porque o projeto é um `Exe`; o contrato de saída é o mesmo do binário.
- Após instalação global (`dotnet tool install -g`), tornar a tool chamável diretamente como `faker /{comando}` — também já ocorre por ser um `Exe` publicável como ferramenta.
- Ambos os modos de invocação (`dotnet faker /person` e `faker /person`) geram os dados com Bogus, sem qualquer alteração de comportamento dos comandos existentes.
- Atualizar `AGENTS.md` para documentar a nova capacidade e os modos de execução.

**Decisão registrada:** a ferramenta se chama `faker` (ToolCommandName). A linha de invocação `dotnet faker <comando>` e a forma global `faker <comando>` são equivalentes; ambas geram dados idênticos à invocação atual.

## Capabilities

### New Capabilities

- `dotnet-tool-packaging`: capacidade de empacotar a CLI como ferramenta .NET e executá-la tanto via `dotnet faker /{comando}` quanto via `faker /{comando}` (após `dotnet tool install -g`), com a geração de dados sendo o mesmo contrato observável existente.

### Modified Capabilities

Nenhuma. Os comandos, flags, mensagens de uso e códigos de saída da `cli-command-interface` não mudam; apenas a forma de invocação da aplicação é nova e ocorre sobre o mesmo contrato.

## Impact

- **Código:** `src/FakeDataCli/FakeDataCli.csproj` ganha propriedades de empacotamento (`PackAsTool`, `ToolCommandName`, `Version`, `Description`, `Authors`, `Title`, `Summary`); o `OutputType` continua `Exe` para suportar a invocação direta.
- **Docs:** `AGENTS.md` ganha seção de uso da ferramenta (`dotnet faker /person`, `faker /person`).
- **Compatibilidade:** sem impacto em consumidores existentes — o binário e os comandos permanecem os mesmos; não há breaking changes.
