# scytral2
Sistemas Cuantitativos de Trading Algorítmico (2)

Esta sección se subdivide en 3 partes.

* Evaluación de sistemas de trading (Tema 4)
* Diseño de carteras de ETFs (Tema 5)
* Evaluación de carteras de sistemas. (Tema 6)

# Evaluación de sistemas
El trabajo realizado se encuentra en la carpeta trabajo hecho, consta de dos documentos
* Evaluación In Sample: Sobre el sistema ya dado BOUL2022 se realiza una evaluación In Sample del sistema en la que se evaluan
  los time frame, horarios y mecanismos de soporte del sistema para tratar de determinar las zonas robustas del análisis paramétrico y determinar el parámetro más óptimo para
  la estrategia. Así mismo se evalua la sensibilidad de la estrategia ante pequeños cambios de la misma en la zona robusta (imprescindible para evitar problemas de overfitting) y se estudia el rendimiento en función de distintos    ratios como el ratio de Calmar, beneficio medio/operación etc..
  
* Evaluación Out Sample: Una vez obtenido el parámetro óptimo de la estrategia tras el análisis durante el periodo In Sample se realizan el paso final de la evaluación antes de poner la estrategia en funcionamiento, sobre diversos periodos Out of Sample utilizando un método Robust Walk Forward en el cual se estudiarán atendiendo a las variaciones en los distintos ratios el rendimiento fuera de los periodos sobre los cuales se han realizado los backtest y las optimizaciones, para comprobar la viabilidad y la supervivencia de la misma en periodos posteriores.

# Diseño de carteras de ETFs
El trabajo realizado se encuentra en la carpeta trabajo hecho

* Cartera ETFs estático (Definitivo): Se realiza un proceso de documentación acerca de la creación y evaluación del portfolio para una cartera de modelo estático. Esto constará de una analisis de las características generales así como la composición de la cartera y un analisis detallado de los activos, el correspondiente analisis del beneficio acumilado así como de los resultados generales como los principales ratios, un análisis pormenorizado de los drawdowns y la rentabilidad y en términos de la rentabilidad riesgos y correlaciones entre activos.

# Evaluación de carteras de sistemas
El trabajo realizado se encuentra en la carpeta practicas/trabajo hecho.

* Construcción y evaluación del portfolio T6: Dados un conjunto de sistemas aplicados sobre ciertos futuros, y partiendo de un capital inicial de 100000$ se creará una cartera en la que se ponderará el peso de cada uno de ellos en función de los rendimientos y los drawdowns del portfolio global, mediante el análisis del Test Profile de la misma. Esto se realizará tanto para un modelo de cartera estático como uno dinámico en la que el balanceo se determinará tras realizar evaluaciones mediante un proceso robust walk forward.

