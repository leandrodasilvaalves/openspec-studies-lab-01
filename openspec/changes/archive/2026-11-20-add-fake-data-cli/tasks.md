# Tasks

## 1. Despacho de comandos e ajuda

- [x] 1.1 Criar o despachador de argumentos em `src/FakeDataCli` com a lista de comandos `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid` e `/help`; verificar com `dotnet run --project src/FakeDataCli -- /help` que a saída lista comandos e flags e termina com código 0
- [x] 1.2 Implementar a rejeição de comando desconhecido e de flag não suportada, com mensagem de erro e uso no `stderr` e código de saída 2; verificar com testes xUnit que `/unknown` e `/person --uppercase` retornam código diferente de zero e mensagem de uso
- [x] 1.3 Substituir o `Hello, World!` em `Program.cs` pelo despachador, tratando a ausência de argumentos como ajuda; verificar com `dotnet run --project src/FakeDataCli` que a saída é idêntica à de `/help`

## 2. Dados de pessoa e contato

- [x] 2.1 Criar a instância compartilhada de `Faker` com locale `pt_BR` e o gerador de pessoa (`first_name`, `last_name`, `full_name`); verificar com teste xUnit que os três campos são não vazios e que `full_name` é a composição dos outros dois
- [x] 2.2 Criar o gerador de contato (`phone`, `email`) usando o mesmo `Faker`; verificar com teste xUnit que `email` tem formato válido e que `phone` contém DDD de dois dígitos e separador
- [x] 2.3 Ligar os comandos `/person` e `/contact` ao despachador, imprimindo `label: value` por linha com os rótulos `first_name`, `last_name`, `full_name`, `phone` e `email`; verificar com `dotnet run --project src/FakeDataCli -- /person` e `/contact` que cada campo aparece em uma linha própria

## 3. Documentos brasileiros

- [x] 3.1 Implementar o cálculo dos dois dígitos verificadores de CPF e CNPJ com os pesos oficiais e rejeitar bases com todos os dígitos iguais; verificar com teste xUnit que casos conhecidos produzem os dígitos esperados e que sequências repetidas são descartadas
- [x] 3.2 Criar os geradores de CPF e CNPJ e ligá-los aos comandos `/cpf` e `/cnpj`; verificar com teste xUnit que centenas de valores gerados passam em um validador independente dos verificadores
- [x] 3.3 Implementar a formatação de saída pontuada por padrão e a remoção de pontuação pela flag `--no-punctuation`; verificar com testes xUnit que o formato padrão contém separadores, que a saída sem pontuação tem apenas dígitos e que ambos os formatos passam na validação independente
- [x] 3.4 Conferir os comandos de documento de ponta a ponta; verificar com `dotnet run --project src/FakeDataCli -- /cpf` e `/cnpj`, com e sem `--no-punctuation`, que apenas o valor é impresso

## 4. Identificadores

- [x] 4.1 Gerar GUID padrão em formato `D` com letras minúsculas e ligá-lo ao comando `/guid`; verificar com teste xUnit que a saída tem 8-4-4-4-12 hexadecimais e é aceita por `Guid.TryParse`
- [x] 4.2 Implementar as funções puras de formatação para `--no-dashes` (formato `N`) e `--uppercase` (caixa invariante); verificar com testes xUnit cada flag isoladamente e a combinação das duas
- [x] 4.3 Conferir o comando de ponta a ponta; verificar com `dotnet run --project src/FakeDataCli -- /guid --no-dashes --uppercase` que a saída tem 32 caracteres maiúsculos e nenhum hífen

## 5. Verificação de integração

- [x] 5.1 Executar `dotnet test` na solução e verificar que todos os testes passam sem falhas ou testes ignorados
- [x] 5.2 Executar uma rodada de smoke de todos os comandos (`/help`, `/person`, `/contact`, `/cpf`, `/cnpj`, `/guid` e combinações de flags) e verificar que nenhum comando imprime `Hello, World!` ou texto de depuração
- [x] 5.3 Conferir que `src/FakeDataCli/FakeDataCli.csproj` e `tests/FakeDataCli.Tests/FakeDataCli.Tests.csproj` não ganharam novas referências de pacote; verificar que `dotnet build` continua passando
