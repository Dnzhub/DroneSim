
# 🛰️ Drone Simulation

A Unity simulation of autonomous drones collecting resources in outer space for two competing factions. The project includes drone AI, resource management, UI controls, and simulation parameters.

---

## ✅ What Is Implemented

- 🔄 **Drone State Machine**: Each drone uses a modular state machine with individual state classes (`Search`, `Collect`, `Return`) for clean and scalable behavior.
- 🧠 **Resource Management**: Randomly spawning resources that avoid obstacles and are claimable by drones to avoid conflict.
- 🛸 **Drone Logic**:
  - Detect nearest unclaimed resource
  - Fly to it
  - Collect after 2s, then return to base
  - Visual effect for delivery
  - Repeats cycle
- 🧭 **Obstacle Avoidance**: Simple separation/avoidance logic to prevent drone collisions.
- 🏳️ **Two Competing Factions**: Drones and bases are color-coded by faction; score tracked per team.
- 🎛️ **UI Controls** (via Unity UI Toolkit):
  - Sliders: number of drones per faction (1–5), drone speed (1-10)
  - Input field: resource spawn frequency
  - Toggle: render drone paths
  - Score display
- ✏️ **Line Renderer Paths**: Optional runtime path rendering per drone.
- 🎥 **Free Flying Camera**: Player-controlled camera to freely inspect the 3D simulation environment.

---

## 🧩 Architecture Overview

The code is built with clean, modular **component-based architecture** using **lazy manager caching** instead of global singletons.

### 🧠 Core Components

| Component        | Responsibility |
|------------------|----------------|
| `GameManager`     | Central simulation flow; caches references to all managers |
| `DroneManager`    | Spawns and manages drones per faction |
| `ResourceManager` | Spawns resources and tracks active/available ones |
| `UIManager`       | Controls all UI interactions, sliders, and data display |
| `BaseController`  | Place holder for accessing bases |
| `DroneController` | Per-drone logic; owns state machine, movement, and effects |
| `IDroneState`     | Interface for drone states |
| `DroneSearchingState`, etc. | Concrete state implementations |
| `FreeFlyCamera`   | Player-controlled camera with mouse and keyboard |

### 🔄 State Machine

Each `DroneController` has a state machine that holds:
- Cached state instances
- Transitions between states (`Searching → Collect → Return → Searching`)
- State update logic is isolated per class for clarity and testability.

### 🔗 Communication

- Managers are referenced through `GameManager.Instance.[XManager]` using lazy initialization for flexibility.
- UI changes (like resource delivery) are triggered by `GameManager → UIManager`.

---

## 🛠️ Tools & Approaches Used

- **Unity 2022 LTS**
- **UI Toolkit** (`UIDocument`) for runtime parameter configuration
- **LineRenderer** for path visualization
- **NavMesh or custom logic** for movement 
- **Dotween** for random resource movement
- **Separation of concerns**: Clear division between drone logic, base logic, and managers
- **Lazy Singleton Access**: Managers are cached via GameManager but not hard singletons
- **Scriptable architecture**: State machine components per class for clarity and reuse
- **Editor-Friendly**: Sliders, toggles, and prefab usage supports easy configuration

---

## 💡 Future Improvements (Maybe)

- 📍 Minimap
- 🔄 Pause/resume
- 🎯 Click-to-follow drone camera
- 🧪 More advanced obstacle avoidance (Boid-style)

---

## 📂 How to Run

1. Open project in Unity 2022.3+ LTS
2. Press Play
3. Use sliders to adjust simulation settings
4. Move around using the FreeFly camera (`WASD`, `QE`, mouse look)
5. Toggle path rendering to debug drone paths

---

### © All rights reserved to **Deniz Yılmaz**