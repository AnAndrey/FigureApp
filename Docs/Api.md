# Local running Rest API for FigureApp

## General API Information
* The base endpoint is: **https://localhost:5001**
* All endpoints return a JSON object.

## HTTP Return Codes

* HTTP `4XX` return codes are used for requests with invalid parameters and internal errors;
  the issue is on the sender's side or something went wrong on the back-end side.
* HTTP `200` return code is used for successful operations.

## Errors
* Any endpoint can return an ERROR

Sample error response below:
```json
{
    "errors": "Invalid figure type 'Square'."
}
```
Invalid JSON error response:
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "|d774f28e-48040c48ff9d2064.",
    "errors": {
        "$.params": [
            "The JSON object contains a trailing comma at the end which is not supported in this mode. Change the reader options. Path: $.params | LineNumber: 4 | BytePositionInLine: 4."
        ]
    }
}
```

## Supported geometrical figures

Type | Params
------------ | ------------
Circle (string) | Radius (double, >0)
Triangle (string) | SideA, SideB, SideC (double, >0)

## Store figure

```
POST /figure
```

* Request example

```json
{
    "type": "Triangle",
    "params":{
        "SideA" : 2,
        "SideB" : 2,
        "SideC" : 2.2
    }
}
```

* Response example

```json
{
    "type": "Triangle",
    "id": 1
}
```
Where `id` is `int`.

## Get figure area

```
GET /figure/{id}
```
* Request query

```
GET /figure/1
```

* Response example

```json
{
    "area": 1.8373622397339073,
    "id": 1,
    "type": "Triangle"
}
```
Where `area` is `double`.
