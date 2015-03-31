# Arquitectura de Referencia .Net Backend 

El proyecto contiene un ejemplo de una posible arquitecturas de referencia. La misma ataña únicamente al backend, proponiendo un modelo de servicios REST, con una capa de dominio y otra de persistencia de datos. 

## Índice
* [Estructura del Proyecto](#/## Estructura del proyecto)
* [Requerimientos](#/## Requerimientos)
* [Licencia] (#/## Licencia)



## Estructura del proyecto

* **Soporte**: Este proyecto, provee tal cual su nombre lo dice, diferentes clases de soporte para el resto de la aplicación: IoC, Logging, Configuración, etc.
* **Modelo**: Aquí se ubican todas las clases de dominio y las interfaces pertinentes a las mismas (IRepositorio por ejemplo)
* **DataAccess_NH**: En esta librería se ubican todas las clases pertinentes al acceso a datos utilizando NHibernate como framework ORM, con Firehawk para el mapeo de clases. Aquí es donde se implementan las interfaces de repositorios. Se propone NHibernate ya que el mismo provee dialectos para diferentes motores de bases de datos en contraposición a EntityFramework que únicamente los provee para SQL Server y Oracle.
* **Servicios**: En este proyecto se implementan todos los servicios que la aplicación va a exponer. Está basado en OWIN, con WebAPI 2, utilizando autorización a traves de JWT. OWIN provee la posibilidad de hostear los servicios ya sea en IIS como self hosted, evitando todo el acople de clases de ASP.Net

## Requerimientos

* **Visual Studio 2013 o superior** en cualquiera de sus versiones (Express, Ultimate, Professional, Community). La versión express se puede descargar desde [aquí](https://www.visualstudio.com/)
* **Nuget** (www.nuget.org) para el manejo de dependencias. En la mayoría de las versiones de VS ya se encuentra integrado, sino se deberá incluir la extensión
* **SQL Server 2012 o superior** Se puede descargar la versión Express en el siguiente [link](http://www.microsoft.com/en-us/server-cloud/products/sql-server-editions/sql-server-express.aspx). 

##Licencia

The MIT License (MIT)

Copyright (c) 2015 Ashwin Hegde

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
