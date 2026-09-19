# Desayuno Asíncrono

---

## Objetivo
Comprender la importancia del **diseño** en la preparación de un desayuno asíncrono y aprender a optimizar tiempos de ejecución.

---

## Descripción
Un desarrollador quiere automatizar la preparación de su desayuno. Cada acción tiene un tiempo de ejecución conocido. El problema es que el usuario tiene **poco tiempo por la mañana** y no puede esperar eternamente.

Tu trabajo es implementar diferentes enfoques de ejecución y descubrir cuál es el adecuado, **analizando por qué unos funcionan y otros no**.
> *"No se trata de correr más rápido, sino de saber qué carreras correr en paralelo."*

---

## Las 7 Acciones del Desayuno
| # | Acción | Tiempo | Descripción |
|---|--------|--------|-------------|
| 1 | Hacer café | 200ms | Encender la cafetera y esperar |
| 2 | Calentar sartén | 200ms | Poner el fuego y esperar a que esté caliente |
| 3 | Freír huevos | 300ms | Necesita la sartén caliente (acción 2) |
| 4 | Freír bacon | 300ms | Necesita la sartén caliente (acción 2) |
| 5 | Tostar pan | 200ms | Meter el pan en la tostadora |
| 6 | Untar mantequilla | 100ms | Necesita el pan tostado (acción 5) |
| 7 | Hacer zumo | 200ms | Exprimir las naranjas |

---

## Restricción
El usuario tiene un **tiempo límite de 500ms**. Si el desayuno no está listo a tiempo:
> ☕ **"¡El café se ha enfriado! Los huevos y tostadas con café frío no tienen gracia..."**

---

## Tareas a Realizar
1. Ejecución **secuencial**

Tabla de Tiempos
| # | Acción | Tiempo |
|---|--------|--------|
| 1 | Hacer café | 200ms |
| 2 | Calentar sartén | 200ms |
| 3 | Freír huevos | 300ms |
| 4 | Freír bacon | 300ms |
| 5 | Tostar pan | 200ms |
| 6 | Untar mantequilla | 100ms |
| 7 | Hacer zumo | 200ms |
| Total estimado | | ~1500ms |
| Resultado| No se puede tomar |

2. Ejecución con **`async/await`**

Tabla de Tiempos
| # | Acción | Tiempo |
|---|--------|--------|
| 1 | Hacer café | 200ms |
| 2 | Calentar sartén | 200ms |
| 3 | Freír huevos | 300ms |
| 4 | Freír bacon | 300ms |
| 5 | Tostar pan | 200ms |
| 6 | Untar mantequilla | 100ms |
| 7 | Hacer zumo | 200ms |
| Total estimado | | ~1500ms |
| Resultado| No se puede tomar |

3. Ejecución con el **mejor rendimiento posible**

Tabla de Tiempos
| # | Acción | Tiempo |
|---|--------|--------|
| 1 | Hacer café + Calentar la sartén + Tostar pan + Hacer zumo | 200ms |
| 2 | Freír huevos + Freir bacon + Untar mermelada | 300ms |
| Total estimado | | ~500ms |
| Resultado| Se puede tomar dependiendo del rendimiento del sistema |

Las tres soluciones anteriores con **timeout de 500ms** (para que el café no se enfríe)

4. Ejecución con **`async/await`** y **CancellationToken**

Tabla de Tiempos
| # | Acción | Tiempo |
|---|--------|--------|
| 1 | Hacer café | 200ms |
| 2 | Calentar sartén | 200ms |
| 3 | Freír huevos | 300ms |
| 4 | Freír bacon | 300ms |
| 5 | Tostar pan | 200ms |
| 6 | Untar mantequilla | 100ms |
| 7 | Hacer zumo | 200ms |
| Total estimado | | ~1500ms |
| Finalización | | 500ms |
| Resultado| No se puede tomar |

5. Ejecución con el **mejor rendimiento posible** y **CancellationToken**

Tabla de Tiempos
| # | Acción | Tiempo |
|---|--------|--------|
| 1 | Hacer café + Calentar la sartén + Tostar pan + Hacer zumo | 200ms |
| 2 | Freír huevos + Freir bacon + Untar mermelada | 300ms |
| Total estimado | | ~500ms |
| Resultado| Se puede tomar dependiendo del rendimiento del sistema |

Compara los tiempos de ejecución de las 5 soluciones y reflexiona sobre las diferencias.
> La solución Síncrona y la AsíncronaMal tardan prácticamente lo mismo debido a que todas las tareas se esperan y ejecutan una trás de otra. La AsíncronaPro reduce el tiempo a ~500ms porque aprovecha la independencia entre tareas y ejecuta varias al mismo tiempo. Lo que se busca con las versiones con CancellationToken, es parar el proceso a los 500ms, haya o no terminado la solución, puesto que se determina que a los 500ms, el café se queda frío, por lo que no podemos tomarnos el desayuno.

---

## Entrega
### 1. Repositorio GitHub
Sube tu proyecto a GitHub. El `README.md` debe incluir:
- Tabla de tiempos de cada enfoque
- Respuestas a estas preguntas:
  1. ¿Qué diferencias has observado entre las 5 soluciones?
  > Síncrono: una única tarea detrás de otra; el programa permanece bloqueado.
  
  > AsíncronoMal: utiliza Task, pero sigue siendo secuencial porque cada await espera antes de iniciar la siguiente operación.
  
  > AsíncronoPro: organiza las tareas independientes para que trabajen en paralelo con Task.WhenAll.
  
  > AsíncronoMal + CancellationToken: igual que AsíncronoMal pero con cacelación por tiempo.
  
  > AsíncronoPro + CancellationToken: igual que AsíncronoPro pero con cacelación por tiempo.
  2. ¿Qué acciones se pueden ejecutar a la vez y cuáles no? ¿Por qué?
  > | Se pueden ejecutar a la vez | Porque... |
  > |---|---|
  > | Preparar café | No depende de ninguna otra tarea. |
  > | Calentar sartén | No depende de ninguna otra tarea. |
  > | Tostar pan | No depende de ninguna otra tarea. |
  > | Preparar zumo | No depende de ninguna otra tarea. |
  
  > | Se pueden ejecutar a la vez pero después de las anteriores | Porque... |
  > |---|---|
  > | Untar mermelada | Necesita que el pan ya esté tostado. |
  > | Freír huevo | Requiere que la sartén esté caliente. |
  > | Freír bacon | Requiere que la sartén esté caliente. |
  
  > El huevo y el bacon se pueden hacer puesto que asumimos que contamos con una sartén lo suficientemente grande como para poder hacer las dos cosas a la vez
  3. ¿Qué ha pasado con cada solución cuando introduces el timeout?
  > Síncrono: no admite cancelación porque utiliza llamadas bloqueantes (Thread.Sleep).
  > AsíncronoMal: la cancelación ocurre a los 500ms, durante FreirHuevo, por lo que deja el resto sin completar, puesto que funciona como el Síncrono, con llamadas bloqueantes.
  > AsíncronoPro: todas las tareas empiezan antes, pero las que aún no han terminado (FreirHuevo + FreirBacon + UnterMermelada) reciben el CancellationToken y lanzan OperationCanceledException cuando el tiempo expira.
  4. ¿El enfoque con mejor rendimiento es también el más seguro? ¿Por qué?
  > No siempre.
  > La solución asíncrona pro es más rápida porque permite realizar varias tareas al mismo tiempo. Sin embargo, también es un poco más complicada y hay que tener cuidado con el orden de las tareas para evitar errores.
  > En este ejercicio funciona bien porque las tareas no dependen unas de otras, excepto en los casos en los que sí hemos establecido un orden, como tostar el pan antes de untar la mermelada. En otros programas, hacer muchas tareas a la vez podría provocar problemas.
  5. ¿Merece la pena complicarse con paralelismo o con mecanismos de control de tiempo? Justifica tu respuesta.
  > El uso de paralelismo es beneficioso cuando hay operaciones que pueden ejecutarse de manera independiente, ya que en tales situaciones, el tiempo de ejecución se reduce de 1500 ms a 500 ms.
  
  > Los mecanismos de timeout también resultan ser prácticos, ya que impiden que una aplicación se quede esperando de forma indefinida una operación que sea lenta. Aunque esto añade complejidad, contribuye a que el programa sea más sólido y que responda de manera más efectiva ante bloqueos o retrasos.

### 2. Aula Virtual
Entrega en **Aula Virtual**:
- Enlace al repositorio GitHub
- Fichero PDF con las respuestas a las preguntas