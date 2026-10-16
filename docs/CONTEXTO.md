# CONTEXTO COMPARTIDO - Kepler-7: Protocolo Crisis

## El proyecto
TP final de la materia: videojuego de escritorio en **C# Windows Forms** (**.NET 8**: `Kepler7.Core` en `net8.0`, `Kepler7.Game` en `net8.0-windows`).
Equipo: Barbara (B), Martin (M), Lautaro (L), Axel (A). Entrega: **viernes 20/11/2026**.

**Prohibido:** motores/frameworks gráficos (Unity, Godot, MonoGame, SFML, Raylib, etc.) y **cualquier paquete NuGet de terceros**. Solo lo que trae .NET (System.*, System.Drawing, System.Windows.Forms, System.Text.Json, System.Media, System.Threading, etc.).

## Idea del juego
Táctico por turnos con gestión de estación. La estación orbital Kepler-7 sufre una crisis y el jugador (el sistema de control central) debe reactivar subsistemas, armar naves, clonar tripulantes, comunicarse con la Tierra y combatir amenazas por sectores. Cada patrón de diseño es una función crítica de la estación.

## Solución
- `Kepler7.Core` (Class Library): TODA la lógica y los 12 patrones. **Sin referencia a System.Windows.Forms** ni a controles. Evitar `MessageBox`, `Control`, `Color` de WinForms.
- `Kepler7.Game` (Windows Forms App): SOLO interfaz y coordinación de interacciones. No contiene reglas del juego.
- `Kepler7.Tests` (opcional): solo si la cátedra confirma qué framework de tests se puede usar.

Regla de oro: la lógica de Core devuelve **datos** (strings de log, listas, enums, resultados). La UI los muestra. Así cada patrón se "ve" en pantalla.

## Los 12 patrones y su mecánica
| Patrón | Familia | Mecánica / pantalla | Responsable |
|---|---|---|---|
| Builder | Creacional | Hangar: armar naves (Exploradora, Combate, Carga) | B |
| Prototype | Creacional | Laboratorio: clonar tripulantes (deep clone) | M |
| Bridge | Estructural | Comunicaciones: tipo de mensaje x cifrado | A |
| Facade | Estructural | Control Central: IniciarTurno / Emergencia / Apagar | B |
| Flyweight | Estructural | Censo de 10.000 tripulantes, 5 especies | L |
| Proxy | Estructural | Bóveda con control de rol y log de denegados | L |
| Chain of Responsibility | Comportamiento | Cadena de defensas en combate | M |
| Command | Comportamiento | Acciones del jugador con deshacer/rehacer | B |
| Visitor | Comportamiento | Analizador de inventario | L |
| State | Comportamiento | Estados del personaje | M |
| Template Method | Comportamiento | Turno de unidades | A |
| Strategy | Comportamiento | IA enemiga intercambiable | A |

## Convenciones del equipo (obligatorias)
1. **Nombres exactos del enunciado** (el profesor los va a buscar): `Nave`, `INaveBuilder`, `IngenieroJefe`, `RecetaExploradora`, `RecetaCombate`, `RecetaCarga`, `IPrototipo` con `Clonar()`, `Mensaje`, `ICifrado`, `MensajeTexto`, `MensajeAlerta`, `MensajeEmergencia`, `SinCifrado`, `CifradoAES`, `CifradoCuantico`, `ControlCentral` (`IniciarTurno()`, `EntrarEnEmergencia()`, `ApagarEstacion()`), `IEspecie`, `Tripulante`, `FabricaEspecies` (`Dictionary<string,IEspecie>`), `IBoveda` (`LeerArchivo(id)`, `EscribirArchivo(id, contenido)`), `BovedaDatos`, `ProxyBoveda`, `IManejadorDanio` (`Manejar(Danio)`), `Esquiva`, `Bloqueo`, `Armadura`, `Escudo`, `Vida`, `IComando` (`Ejecutar()`, `Deshacer()`), `MoverComando`, `AtacarComando`, `CurarComando`, `ConstruirComando`, `GestorComandos`, `IVisitor`, `IItem` (`Aceptar(IVisitor)`), `CalculadorPrecio`, `ContadorArmas`, `AplicadorVeneno`, `IEstadoPersonaje` (`Atacar()`, `Mover()`, `RecibirDanio()`), `Normal`, `Envenenado`, `Aturdido`, `Berserk`, `TurnoUnidad` (`EjecutarTurno()` NO virtual), `TurnoSoldado`, `TurnoMedico`, `TurnoFrancotirador`, `TurnoComandante`, `IEstrategiaIA` (`DecidirAccion(Enemigo, Contexto)`), `IAAgresiva`, `IADefensiva`, `IAAleatoria`, `IACalculadora`.
2. **Conflictos de nombres** (resolver con namespaces):
   - `Tripulante` de Flyweight (Lautaro) vs tripulante del Prototype (Martin) -> el de Prototype se llama `TripulanteClonable`.
   - `Armadura` eslabón de Chain (Martin) vs `Armadura` ítem del inventario (Lautaro) -> namespaces distintos: `Kepler7.Core.Patrones.Comportamiento.Chain` y `Kepler7.Core.Inventario`.
3. Namespaces: `Kepler7.Core.Patrones.<Familia>.<Patron>` (Familia = Creacionales / Estructurales / Comportamiento). Modelo común en `Kepler7.Core.Modelo`.
4. Comentarios XML (`///`) en español en toda clase pública, indicando qué rol cumple en el patrón (ej: `/// Rol Builder: Director`).
5. Cada patrón expone un **log o resultado** legible (`IReadOnlyList<string>` o un objeto de resultado) para que la UI lo muestre.
6. Aleatoriedad inyectable (`Random` recibido por constructor) para poder testear.
7. Rutas de archivos inyectadas por constructor; el valor real es `%AppData%\Kepler7\` (nunca la carpeta del .exe).
8. Commits chicos y con sentido: `feat(chain): agrega eslabon Escudo`, `docs(uml): diagrama de State`. Ramas: `feature/<tema>`. Se mergea por Pull Request revisado por otro integrante.
9. UML: se escribe en **PlantUML** o **Mermaid** (texto) en `docs/uml/` y se exporta a PNG.
10. Cada patrón tiene un archivo `docs/informe/<patron>.md` (máx. media página): por qué se eligió, qué mecánica resuelve, dificultad encontrada.
