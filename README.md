[![.NET](https://github.com/ternista/questionnaire-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ternista/questionnaire-api/actions/workflows/dotnet.yml)

# Questionnaire Api

## API Documentation

Swagger documentation has been added to Api project. To explore it, run the `Questionnaires.Api` project and open `http://localhost:5000/swagger/index.html` in your browser.

## Authentication

This api has a mocked JWT-based authentication, call `POST authentication/token` with one of the test accounts to get bearer token.
Token contains mocked `DepartmentId`, `CompanyId` and `UserId` claims. Due to the time limitations those values are hardcoded.

Test accounts:
bob@gmail.com
roos@gmail.com
sarah@gmail.com

## Storage and data seeding

Project uses in-memory EF implementation to read and write data. When application started, storage is populated with a test data from `questionnaire.json` file.

## Getting questions with answer options

Endpoint `GET ​/questionnaire​/{questionnaireId}​/questions` provides paginated question list for a questionnaire with answer options. The results are sorted by section order and by question order within a section. All texts are returned for a selected language (`locale` parameter).
Client can control number of returned questions by specifying `offset` and `limit` parameters.

## Submitting one or several answers

Endpoint `POST ​/questionnaire​/{questionnaireId}​/responses` accept one or multiple answers for a questionnaire. It can be used by adding new as well as adjusting existing answers.

After answers are recorded, department aggregated response summary are recalculated. Due to the time limitations that is processed synchronously by mocked `IEventDispatcher` implementation. But the intention was to process those changes asynchronously with the use of message broker.

## Getting response summary

Endpoint `GET /questionnaire/{questionnaireId}/responses/summary` returns aggregations of min, max and average score for each question grouped by department. Aggregations are pre-calculated upon submission of the answer.

## Requests validation and error handling

Api doesn't have proper response validation or error handling. If invested more time, I would create request validation middleware, for example using `FluentValidation` framework and middleware for handling business and validation exceptions and returning proper api response. 
