# LawAssistant-Backend
Решение содержит API и библиотеки системы анализа степени соответствия коллективного договора действующему закодательству

## Проекты в решении
- **LawAssistant.Api** - ASP.NET Core Web API проект
- **LawAssistant.Application** - библиотека бизнес-логики
- **LawAssistant.Domain** - библиотека доменных сущностей
- **LawAssistant.Infrastructure** - реализация логики работы с внешними системами
- **LawAssistant.SemanticComparison** - FastAPI-приложение для семантического сопоставления

## Быстрый запуск
### Запуск API
``` bash
dotnet run --project LawAssistant.Api
```
### Запуск модуля семантического сопоставления
``` bash
uvicorn main:app
```
## Конфигурация
### Конфигурация API
В `LawAssistant.Api/appsettings.json` требуется добавить настройки подключения к БД, S3-хранилищу и модулю семантического сопоставления, а также параметры JWT-токенов:
``` json
{
  "DbConfiguration": {
    "PostreSqlConnectionString": "..."
  },
  "S3Configuration": {
    "Url": "...",
    "Login": "login", 
    "Password": "password",
    "DocumentsBucketName": "docs",
    "UseSsl": false
  },
  "SemanticModuleConfiguration": {
    "URL": "..."
  },
  "JwtConfiguration": {
    "ExpirationTimeInMinutes": 120,
    "SecretKey": "secret_key"
  }
}
```