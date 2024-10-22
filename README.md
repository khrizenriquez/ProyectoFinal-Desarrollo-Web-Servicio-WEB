# Proyecto Final: .NET /Web Services/JAVA EE

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
