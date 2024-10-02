# Структура микросервиса

Структура стандартного микросервиса состоит из 14 проектов.

## Структура папки src

В папке src располагается основная логика микросервиса

| Название проекта        | Описание |
|-------------------------|----------|
| Application             |          |
| Application.Contracts   |          |
| Domain                  |          |
| Domain.Shared           |          |
| EntityFrameworkCore     |          |
| Graphql                 |          |
| HttpApi                 |          |
| HttpApi.Client          |          |
| HttpApi.Host            |          |

## Структура папки test

В папке test располагается тесты сервиса

| Название проекта          | Описание |
|---------------------------|----------|
| Application.Tests         |          |
| Domain.Tests              |          |
| EntityFrameworkCore.Tests |          |
| Graphql.Tests             |          |
| TestBase                  |          |

## Пример

Если ваш микросервис называется **administration**, то структура микросервиса будет выглядеть так:
- src
  - Rubrum.AdministrationService.Application
  - Rubrum.AdministrationService.Application.Contracts
  - Rubrum.AdministrationService.Domain
  - Rubrum.AdministrationService.Domain.Shared
  - Rubrum.AdministrationService.EntityFrameworkCore
  - Rubrum.AdministrationService.Graphql
  - Rubrum.AdministrationService.HttpApi
  - Rubrum.AdministrationService.HttpApi.Client
  - Rubrum.AdministrationService.HttpApi.Host
- test
  - Rubrum.AdministrationService.Application.Tests
  - Rubrum.AdministrationService.Domain.Tests
  - Rubrum.AdministrationService.EntityFrameworkCore.Tests
  - Rubrum.AdministrationService.Graphql.Tests
  - Rubrum.AdministrationService.TestBase



