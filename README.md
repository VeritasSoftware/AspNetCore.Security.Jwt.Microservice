# AspNetCore.Security.Jwt.Microservice
Asp Net Core JWT Bearer Token Security Micro service

## Demo API secured by **AspNetCore.Security.Jwt** package.

[**Package AspNetCore.Security.Jwt**](https://github.com/VeritasSoftware/AspNetCore.Security.Jwt)

API Swagger UI

![API Swagger UI](https://github.com/VeritasSoftware/AspNetCore.Security.Jwt.Microservice/blob/master/Demo.jpg)

Add below to your appsettings.json or Secret Manager (Manage User Secrets - right click API project).

```C#
{
  "SecuritySettings": {
    "Secret": "a secret that needs to be at least 16 characters long",
    "Issuer": "your app",
    "Audience": "the client of your app",
    "IdType": "Name",
    "TokenExpiryInHours": 1.2,
    "AppId": "your facebook app id here",
    "AppSecret": "your facebook app secret here"
  }
  .
  .
}
```

After obtaining the JWT bearer token (by calling **/token** or **/facebook** endpoints), enter the token using the **Authorize button** as:

Authorization: Bearer \<token\>

Then you can call the secure Movies Controller as shown below.

![Secure API Call](https://github.com/VeritasSoftware/AspNetCore.Security.Jwt.Microservice/blob/master/SecureAPICall.jpg)
