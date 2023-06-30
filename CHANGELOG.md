# Change Log

Lista de cambios en el proyecto.

## 6/29/2023

### Added

- Se agregaron animaciones de movimiento, animaciones de salto y sonidos SFX para el jugador.
- El jugador puede controlar la cámara usando un mouse o joystick.
- El jugador puede saltar.
- Se agrego un modelo fbx de prueba para el jugador

### Changed

- Se cambió la configuración del proyecto para usar el Input Manager (old) e Input System Package (New).
- Los enemigos utilizan el prefab "Enemy" creado en la carpeta Prefabs. Se separo el mesh y el skeleton para una mejor organización.
- Modelos .fbx movidos a la carpeta Models.
- Texturas movidas a carpeta Textures.

### Fixed

- Script Enemy.cs - Se optimizó el calculo de distancias usando SqrMagnitude. Se optimizó el uso de Animator.SetBool() para utilizar el id de los parámetros.
- Se corrigió el nombre del script BarraVida. Se agregó una corrutina que se ejecuta cuando la vida del jugador llega a cero.

### Removed 
- Se movieron los GameObjects obsoletos de la escena principal al GameObject "NotUsed (Old)" hasta comprobar que el proyecto funcione correctamente.