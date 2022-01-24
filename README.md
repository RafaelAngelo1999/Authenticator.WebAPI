# Authenticator.WebAPI
Aplicação de autenticação JWT utilizando DDD.

![](assets/application.png)

## Rotas

- Autenticar e obter token 
- Rotas para validar autenticação de acordo com a "Role"

## Arquitetura do projeto

Foi desenvolvido uma API de autenticação utilizando JWT para validação de role, tempo de token, e token do usuario

## Modelo de retorno para obter dados e token

```{
  "id": "96f1c7ca-17cb-4284-87dc-b0d513048c49",
  "username": "Rafael",
  "email": "rafaelangelowow@gmail.com",
  "role": "Manager",
  "token": {
    "key": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiUmFmYWVsIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE2NDI5OTQ1MjAsImV4cCI6MTY0Mjk5NjMyMCwiaWF0IjoxNjQyOTk0NTIwfQ.ODWusj5ht7KoLC3fGpu4-lRNyb0uAA4fh9JH90iDhKk",
    "vencimento": "2022-01-24T03:52:00.2083397Z"
  }
}
```

## Modelo de retorno para tokens de role invalida

```
Code	Details
401
Undocumented
Error: response status is 401

Response headers
 content-length: 0 
 date: Mon,24 Jan 2022 03:22:48 GMT 
 server: Kestrel 
 www-authenticate: Bearer 
```

## Modelo de retorno para tokens corretamente

```
Code	Details
200	
Response body

Response headers
 content-type: text/plain; charset=utf-8 
 date: Mon,24 Jan 2022 03:24:08 GMT 
 server: Kestrel 
```
