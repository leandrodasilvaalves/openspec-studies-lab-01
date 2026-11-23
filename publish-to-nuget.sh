#!/usr/bin/env bash
set -euo pipefail

# Publica o pacote da ferramenta `fakedatacli` no NuGet.org.
#
# Pré-requisitos:
#   - Conta no nuget.org com uma chave de API de escopo "Push new packages".
#   - Chave exportada na variável de ambiente NUGET_API_KEY.
#
# Uso:
#   NUGET_API_KEY="chave" ./publish-to-nuget.sh

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

if [[ -z "${NUGET_API_KEY:-}" ]]; then
    echo "Erro: variável de ambiente NUGET_API_KEY não definida." >&2
    exit 1
fi

echo ">> dotnet pack src/FakeDataCli"
dotnet pack src/FakeDataCli

NUPKG="$(ls -t src/FakeDataCli/bin/Release/fakedatacli.*.nupkg | head -n 1)"
echo ">> dotnet nuget push $NUPKG"
dotnet nuget push "$NUPKG" --source https://api.nuget.org/v3/index.json --api-key "$NUGET_API_KEY"

echo "Pronto: $NUPKG publicado."
