___

# SpotCoordinate Generator for Revit 2023

![Revit](https://img.shields.io/badge/Revit-2023-blue) ![C#](https://img.shields.io/badge/C%23-17.0-green) 

## 📌 Project Description

This project is a plugin for Autodesk Revit developed in C# that allows you to create **automatic Spot Dimensions** on piles (`FamilyInstance`) in plan or section views. The user can select piles individually, multiple at once, or all piles present in the current view.

The script calculates the top face of each pile, determines its central point, and creates a `SpotCoordinate` with a predefined offset.

## 🚀 Project Status

- **Status:** In development
- **Version:** 1.0.0
- **Last Update:** April 15, 2025

## ✨ Key Features

- **Flexible Selection:** The user can choose between individual, multiple, or automatic selection of all piles in the active view.
- **Automatic Top Face Detection:** Identifies the horizontal top face of each pile.
- **Spot Dimensions Creation:** Generates dimensions with a predefined offset.
- **View Validation:** Only works on `ViewPlan` or `ViewSection` view types.

## ⚙️ Prerequisites

To use this plugin, you will need the following:

- Autodesk Revit 2023 (or another compatible version).
- .NET Framework 4.8 or higher.
- Basic knowledge of C# programming and the Revit API.

## 🛠 Installation Instructions (Steps 3 and 4 Coming Soon)

***1. Clone this repository to your local machine:***
   bash
   git clone https://github.com/your-username/SpotCoordinateGenerator.git
***2. Open the project in Visual Studio.***

*Coming Soon*
3. Build the project to generate the .dll file.
4. Copy to: C:\Users\[YourUser]\AppData\Roaming\Autodesk\Revit\Addins\[RevitVersion]\

## 🖥 Usage Instructions

1. Open Revit:
Launch Autodesk Revit and open a project with piles (FamilyInstance).

2. Run the Command:
Run the command from the "Add-ins" tab.

3. Select Option:
Choose how you want to select the piles:
- Individual: Select a single pile.
- Multiple: Select several piles.
- All in View: Automatically select all visible piles in the current view.

4. Automatic Dimension Generation:
The plugin will automatically generate the dimensions following these internal steps:
- Retrieves the top face of each pile (if it's flat and horizontal).
- Calculates the central point of that face.
- Creates a SpotCoordinate with a predefined offset (XYZ(2,2,0)).

5. Errors and Warnings:
If a pile does not have a valid top face, it will be skipped, and a message will indicate how many piles were ignored.

## 🗂 Project Structure

PlanoPilotes/
├── Commands/
│   └── SpotCoordinate_PickObject.cs       # Main entry point of the plugin
├── Helpers/
│   └── SpotDimensionHelper.cs            # Auxiliary functions for geometry and dimension creation
├── UI/
│   ├── Form1.cs                           # Graphical interface for option selection
│   └── Form1.Designer.cs                  # Design of the graphical interface

**File Details**

***Commands/SpotCoordinate_PickObject.cs***

- Class: SpotCoordinate_PickObject
- Implements: IExternalCommand
- Functionality:
    - Displays the Form1 form.
    - Allows element selection according to the user's choice.
    - Validates that the active view is ViewPlan or ViewSection.
    - Calls SpotDimensionHelper methods to obtain the face, the central point, and create the dimension.

***Helpers/SpotDimensionHelper.cs***
- Class: SpotDimensionHelper (static)
- Methods:
    - GetTopFace: Returns the horizontal top face of a solid.
    - GetFaceCenter: Evaluates the center of the Face using its BoundingBoxUV.
    - CreateSpotDimension: Creates a SpotCoordinate using the Revit API with XYZ offsets.
    
***UI/Form1.cs and Form1.Designer.cs***

- Class: Form1 (partial)
- Functionality:
    - Contains the interface with buttons for individual, multiple, or automatic selection.
    - Exposes the property: OpcionElegida, of enum type OpcionSeleccion { Individual, Multiple, VistaActual, Ninguna }.

## 🤝 Contributions
Contributions are welcome! If you want to improve this project, follow these steps:

1. Fork the repository.
2. Create a new branch (git checkout -b feature/new-feature).
3. Commit your changes (git commit -m "Add new feature").
4. Push your changes (git push origin feature/new-feature).
5. Open a Pull Request.

## 📜 License
This work is licensed under a Creative Commons Attribution-NonCommercial 4.0 International License.


## 📧 Contact
If you have questions or suggestions, feel free to contact me:

**Email:** [juanarias.git@gmail.com - jariasandrade@gmail.com]  
**Whatsapp:** +(57) 3108113350  
**GitHub:** @JuanAriasAndrade
 
 # Creador  de Cotas de Coordenadas para Revit 2023

![Revit](https://img.shields.io/badge/Revit-2023-blue) ![C#](https://img.shields.io/badge/C%23-17.0-green) 

## 📌 Descripción del Proyecto

Este proyecto es un plugin para Autodesk Revit desarrollado en C# que permite crear **Spot Dimensions automáticas** sobre pilotes (`FamilyInstance`) en vistas de plano o sección. El usuario puede seleccionar pilotes individualmente, múltiples o todos los presentes en la vista actual.

El script calcula la cara superior de cada pilote, determina su punto central y crea un `SpotCoordinate` con un desplazamiento predeterminado.

## 🚀 Estado del Proyecto

- **Estado:** En desarrollo
- **Versión:** 1.0.0
- **Última actualización:** 15 de Abril de 2025

## ✨ Características Principales

- **Selección flexible:** El usuario puede elegir entre selección individual, múltiple o automática de todos los pilotes en la vista activa.
- **Detección automática de caras superiores:** Identifica la cara horizontal superior de cada pilote.
- **Creación de Spot Dimensions:** Genera cotas con un offset predefinido.
- **Validación de vistas:** Solo funciona en vistas de tipo `ViewPlan` o `ViewSection`.

## ⚙️ Requisitos Previos

Para utilizar este plugin, necesitarás lo siguiente:

- Autodesk Revit 2023 (u otra versión compatible).
- .NET Framework 4.8 o superior.
- Conocimientos básicos de programación en C# y la API de Revit.

## 🛠 Instrucciones de Instalación (Proximamente punto 3 y 4 )

***1. Clona este repositorio en tu máquina local: ***
   bash
   git clone https://github.com/tu-usuario/SpotCoordinateGenerator.git
***2. Abre el proyecto en Visual Studio.***

*Proximamente*
3. Compila el proyecto para generar el archivo .dll.
4. C:\Users\[TuUsuario]\AppData\Roaming\Autodesk\Revit\Addins\[VersiónRevit]\

## 🖥 Instrucciones de Uso
1. Abrir Revit:
Abre Autodesk Revit y carga un proyecto con pilotes (FamilyInstance).

2. Ejecutar el Comando:
Ejecuta el comando desde la pestaña "Add-ins".

3. Seleccionar Opción:
Selecciona cómo deseas elegir los pilotes:
- Individual: Selecciona un solo pilote.
- Múltiple: Selecciona varios pilotes.
- Todos en vista: Selecciona automáticamente todos los pilotes visibles en la vista actual.

4. Generación Automática de Cotas:
El plugin generará las cotas automáticamente siguiendo estos pasos internos:
- Obtiene la cara superior de cada pilote (si es plana y horizontal).
- Calcula el punto central de esa cara.
- Crea un SpotCoordinate con un offset predeterminado (XYZ(2,2,0)).

5. Errores y Advertencias:
Si algún pilote no tiene una cara superior válida, se omitirá y se mostrará un mensaje indicando cuántos pilotes fueron ignorados.

## 🗂 Estructura del Proyecto

PlanoPilotes/
├── Commands/
│   └── SpotCoordinate_PickObject.cs       # Entrada principal del plugin
├── Helpers/
│   └── SpotDimensionHelper.cs            # Funciones auxiliares para geometría y creación de cotas
├── UI/
│   ├── Form1.cs                           # Interfaz gráfica para selección de opciones
│   └── Form1.Designer.cs                  # Diseño de la interfaz gráfica

**Detalles de los Archivos**

***Commands/SpotCoordinate_PickObject.cs***

- Clase: SpotCoordinate_PickObject
- Implementa: IExternalCommand
- Funcionalidad:
	- Muestra el formulario Form1.
	- Permite selección de elementos según opción del usuario.
	- Valida que la vista activa sea ViewPlan o ViewSection.
	- Llama a métodos de SpotDimensionHelper para obtener cara y punto central, y crear la cota.
	
***Helpers/SpotDimensionHelper.cs***
- Clase: SpotDimensionHelper (static)
- Métodos:
	- ObtenerCaraSuperior: Devuelve la cara horizontal superior de un sólido.
	- ObtenerCentroCara: Evalúa el centro de la Face usando su BoundingBoxUV.
	- CrearSpotDimension: Crea un SpotCoordinate usando la API de Revit con desplazamientos en XYZ.
	
***UI/Form1.cs y Form1.Designer.cs***

- Clase: Form1 (partial)
- Funcionalidad:
	- Contiene la interfaz con botones para selección individual, múltiple o automática.
	- Expone la propiedad: OpcionElegida, del tipo enum OpcionSeleccion { Individual, Multiple, VistaActual, Ninguna }.

## 🤝 Contribuciones
¡Las contribuciones son bienvenidas! Si deseas mejorar este proyecto, sigue estos pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (git checkout -b feature/nueva-funcionalidad).
3. Haz commit de tus cambios (git commit -m "Añade nueva funcionalidad").
4. Sube tus cambios (git push origin feature/nueva-funcionalidad).
5. Abre un Pull Request.

## 📜 Licencia
*Este script está disponible solo para fines educativos, de investigación o de experimentación personal.*
*Su uso con fines comerciales está estrictamente prohibido bajo los términos de la licencia CC BY-NC 4.0.*

## 📧 Contacto
Si tienes preguntas o sugerencias, no dudes en contactarme:

**Email:** [juanarias.git@gmail.com - jariasandrade@gmail.com ]
**Whatsapp:** +(57) 3108113350
**GitHub:** @JuanAriasAndrade
