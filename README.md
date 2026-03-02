# hackerApi

Pre Requisitos tener instalado el siguiente software 
- SDK Net Core 8
- Git Cli
- Visual Studio
- Postman o cualquier cliente

1 - Clonar el repositorio en un folder local
git clone https://github.com/pablorenmar/hackerApi.git

2- Configurar el archivo appsettings para el ambiente con la url de la api considerar la estrucura del json
"Api": {
    "HackerApi": {
      "BaseUrl": "https://hacker-news.firebaseio.com/v0/"
    }
  }

3 - Compilar y Ejecutar el proyecto

Informacion general
Ejemplo de request api

curl -X 'GET' \
  'https://localhost:7277/api/stories/best/10' \
  -H 'accept: */*'

Documentation tecnica
  - Se utilizo Inyeccion de dependecia para la invocacion de la api de hacker
  - Se agrego cache al servicio de obtener el detalle de la historia por Id de 3 minutos
  - El projecto tiene una arquitectura de dominio 
