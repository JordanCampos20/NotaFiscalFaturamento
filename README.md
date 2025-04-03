dotnet ef migrations add "First_Migration" -p NotaFiscalFaturamento.Infrastructure -s NotaFiscalFaturamento.API

dotnet ef database update -p NotaFiscalFaturamento.Infrastructure -s NotaFiscalFaturamento.API