# Generate certificate

In powershell run:

- `$pwd="Test1234@"`
- `$pfxFilePath="D:\RiderProjects\MonitoringAndLogging.AZ204\cert\selfsigncert.pfx"`
- `openssl req -x509 -sha256 -nodes -days 365 -newkey rsa:2048 -keyout privateKey.key -out selfsigncert.crt -subj /CN=localhost`
- `openssl pkcs12 -export -out $pfxFilePath -inkey privateKey.key -in selfsigncert.crt -password pass:$pwd`
- `openssl pkcs12 -in selfsigncert.pfx -out selfsigncert.pem -nodes`

# Add Certificate Azure Portal

![add_cert_azure_portal](./../img/01_azure_portal_add_cert.PNG)

# Add Certificate Postman

![add_cert_postman](./../img/02_add_postman_certificate.PNG)

# Configure JWT validation policy

- Choose a signing key, for example GUID: `0b9137730e06443183888e4eda727bc5`
- Sign some test token with it, using http://jwtbuilder.jamiekurtz.com

![generate_token](./../img/04_jwt_generator.PNG)

- Update sign key inside APIM as per screenshot

![update_apim](./../img/03_apim_jwt_sign_key.PNG)

- Copy token to the https://jwt.io
- Validate its signature
- Check `secret base64 encoded`

![jwt_io](./../img/05_jwt_token_secret_base64_encoded.PNG)

- Copy-paste token from jwt.io to the postman as Bearer Auth Header and send request

![postman](./../img/06_postman_response.PNG)
