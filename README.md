#  Sistema de Recursos Humanos - Veterinaria KOA

Sistema completo de gestión de Recursos Humanos desarrollado en **ASP.NET Core MVC** y **SQL Server**.

---

##  Descripción

Este sistema permite administrar de forma integral a los empleados de una veterinaria, incluyendo gestión de pagos, productividad, horas extras, vacaciones y control de información laboral.

---

##  Funcionalidades principales

###  Gestión de empleados
- Crear empleados
- Editar información
- Despedir empleados
- Estado activo/inactivo

###  Gestión de pagos
- Pago mensual, quincenal y semanal
- Cálculo automático de salario según tipo de pago
- Cálculo de:
  - IGSS
  - ISR
- Control de días trabajados y ausencias
- Descuento de séptimo día

###  Horas extras
- Registro de horas extras
- Cálculo automático del valor de hora extra
- Integración directa al salario

###  Productividad
- Cálculo automático de productividad (%)
- Clasificación:
  - Alta
  - Media
  - Baja
- Gráficas dinámicas (Chart.js)

###  Vacaciones
- Solicitud de vacaciones por empleado
- Cálculo automático de días hábiles
- Control de períodos pendientes
- Historial de solicitudes
- Aprobación por administrador

### Usuario trabajador
- Visualización de sueldos
- Visualización de productividad
- Solicitud de vacaciones
- Historial personal

---

##  Lógica implementada

- Cálculo por días hábiles (excluye fines de semana)
- Validación de pagos por mes según tipo:
  - Mensual → 1 pago
  - Quincenal → 2 pagos
  - Semanal → 4 pagos
- Cálculo proporcional del salario
- Cálculo de horas extras basado en salario diario
- Sistema de roles (Admin / Empleado)

---

## Tecnologías utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- Chart.js
- C#

---

##  Módulos del sistema

- Dashboard
- Empleados
- Pagos
- Flujo de pagos
- Productividad
- Vacaciones
- Portal del trabajador

---

 ## Estado del proyecto

Funcional  
Base de datos integrada  
Sistema completo de RRHH  

---




