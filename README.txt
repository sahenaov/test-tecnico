#Test

El proyecto funciona con el request de rutas múltiples, el cual permite determinar los posibles caminos a un mismo destino conociendo también el origen. Provee el numero de vuelos y el precio total.

# Condiciones de uso:

Actualmente, funciona de manera backend, las pruebas se realizan desde el swagger y allí se introducen los datos solicitados. Se está en proceso de realización de una interfaz mas amigable y con las condiciones solicitadas en el test.

En el proyecto se establecieron 3 etapas para un uso adecuado. 
- En FLightData se ofrece el URL en el que se hara la conexion y se forman las excepciones pertinentes.
- FlightBusiness hace las conexiones con las clases Journey, Flight y Transport y determina la cantidad de rutas (con y sin escala) dependiendo de los lugares de origen y destino. Ofrece el valor de precio por vuelo y el valor total de precio, ademas del numero de paradas por viaje.
- JourneyController obtiene la informacion que provista anteriormente y devuelve la respuesta.


Adicionalmente

Se observa en la entrega que los valores de la clase transporte estan Null.

# Se presenta el archivo que contiene el backend y el que contiene el frontend.

La problematica surge al conectar el NetCore con Angular debido a CORS, por lo cual es necesario usar ng serve --proxy-config proxy.conf.json para acceder al servidor. Para el correcto funcionamiento, tanto el NetCore como el Angular deben estar ejecutándose.

El localhost para observar la interfaz es http://localhost:4200/
