# Proyecto Final: .NET /Web Services/JAVA EE

### Local env:
- Swagger: http://localhost:5199/swagger/index.html
- Afiliados: http://localhost:5199/api/consulta/afiliado
- Proveedor: http://localhost:5199/api/consulta/proveedor

### Body
- Afiliados
`{
  "codigoPaciente": 1,
  "fechaNacimiento": "1990-05-14"
}`

- Proveedor
`{
  "nitProveedor": "NIT002",
  "codigoPaciente": 2,
  "fechaNacimiento": "1985-08-22T00:00:00Z",
  "fechaCobertura": "2022-05-01T00:00:00Z"
}`

Servicio WEB (Arquitectura .NET): El sistema deberá de contar con un servicio WEB que permita la consulta de datos en línea para cualquier proveedor de servicios o cualquier afiliado del seguro. 

El servicio WEB deberá contar con dos métodos web:
1. Consulta Proveedor: Para poder verificar si el paciente (afiliado al seguro médico) se encuentra activo.
El sistema deberá solicitar los siguientes parámetros:
- NIT del Proveedor de Servicios
- Código del Paciente (Afiliado)
- Fecha de Nacimiento
- Fecha Cobertura

El sistema deberá validar que el NIT del proveedor se encuentre registrado en la Base de datos de proveedores de la red del seguro.

El sistema deberá validar que el paciente tenga un pago de prima para la fecha de cobertura. Como resultado de la consulta el sistema devolverá un numero de autorización en caso el paciente se encuentre activo en la fecha enviada o debe devolver la cadena “Sin Cobertura”. El servicio deberá almacenar la transacción de consulta de cobertura y el código de autorización retornado.

<img width="661" alt="image" src="https://github.com/user-attachments/assets/ba8ec095-6515-4f8e-86a2-40d266476a50">


2. Consulta de Afiliado: Para poder consultar si el afiliado al seguro médico se encuentra activo. El sistema deberá solicitar como parámetros de entrada el Código del Paciente y la fecha de nacimiento, y deberá retornar “Activo” o “Sin Cobertura”. (La fecha de cobertura para determinar si el afiliado se encuentra activo es la fecha actual)


### Imágenes de ejemplo
![image](https://github.com/user-attachments/assets/0c99c242-6c2d-46c1-92e4-af665fc90efc)

![image](https://github.com/user-attachments/assets/07a71d35-bf94-48bc-957c-0ad927af8aa3)

![image](https://github.com/user-attachments/assets/d952ef87-a748-4f2e-81a8-c6ec2ba3446a)

![image](https://github.com/user-attachments/assets/0b1b18f4-8bec-45f2-b75b-a733af2b8140)

### Curl de ejemplo
#### Con cobertura
`curl --request POST \
  --url http://localhost:5199/api/consulta/afiliado \
  --header 'Content-Type: application/json' \
  --header 'User-Agent: insomnia/10.1.1' \
  --data '{
  "codigoPaciente": 1,
  "fechaNacimiento": "1990-05-14"
}
'`
#### Sin Cobertura
`curl --request POST \
  --url http://localhost:5199/api/consulta/afiliado \
  --header 'Content-Type: application/json' \
  --header 'User-Agent: insomnia/10.1.1' \
  --data '{
  "codigoPaciente": 1,
  "fechaNacimiento": "1990-05-15"
}
'`




