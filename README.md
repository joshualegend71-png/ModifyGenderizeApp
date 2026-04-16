# Gender Classification API

## Overview

This is a simple ASP.NET Core API that classifies a name by gender using the Genderize API.
The response is processed to include confidence logic and a standardized format.

---

## Endpoint

### GET `/api/classify`

#### Query Parameter:

* `name` (string, required)

#### Example Request:

```
GET /api/classify?name=John
```

---

## Success Response

```json
{
  "status": "success",
  "data": {
    "name": "John",
    "gender": "male",
    "probability": 0.99,
    "sample_size": 1234,
    "is_confident": true,
    "processed_at": "2026-04-16T12:00:00Z"
  }
}
```

---

## Error Responses

### 400 - Missing or Empty Name

```json
{
  "status": "error",
  "message": "Name is required"
}
```

### 422 - Invalid Name Type

```json
{
  "status": "error",
  "message": "Name must be a string"
}
```

### No Prediction Available

```json
{
  "status": "error",
  "message": "No prediction available for the provided name"
}
```

### 502 - External API Error

```json
{
  "status": "error",
  "message": "External service error"
}
```

---

## Processing Rules

* `count` is renamed to `sample_size`
* `is_confident` is `true` when:

  * probability ≥ 0.7 AND
  * sample_size ≥ 100
* `processed_at` is generated per request (UTC, ISO 8601)

---

## Setup Instructions

1. Clone the repository
2. Open in Visual Studio
3. Run the project
4. Test using Postman or browser

---

## CORS

CORS is enabled with:

```
Access-Control-Allow-Origin: *
```

---

## Notes

* Built with ASP.NET Core
* Uses HttpClientFactory for external API calls
* Designed to handle concurrent requests efficiently
