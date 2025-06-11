# 🕹️ Toasty-Girl: Enemy

> This document summarizes my individual contributions to the Unity-based 2D action game **Toaster-Girl**, focusing on enemy behavior and UI systems.

> I worked as part of the *Enemy Team* and was specifically responsible for implementing the flying enemy "Canbee" and the enemy health bar system.






## 📌 Project Overview

- **Project Name:** Toaster-Girl
- **Genre:** 2D Action Platformer
- **Development Tools:** Unity (C#), Unity Animator, UI (Canvas)
- **Team:** Enemy Team
- **My Role:** Junior Developer


## 🧩 Summary of My Contributions

| Feature | Description | Script Files |
|--------|-------------|--------------|
| 🪫 Enemy Health Bar UI | Health bar displayed above each enemy in real time | `EnemyHealthBar.cs`, `Health.cs` |
| 🐝 Enemy "Canbee" | A flying enemy that tracks and attacks the player | `CanbeeController.cs`, `CanbeeAttack.cs` |




## 🪫 Enemy Health Bar UI System
### Feature Description
>Each enemy has a world-space health bar that updates in real time as it takes damage. The health bar always faces the camera for readability.

### Key Logic
```csharp
// EnemyHealthBar.cs
void Update()
{
    healthSlider.value = enemy.CurrentHealth / enemy.MaxHealth;
    transform.LookAt(transform.position + Camera.main.transform.forward);
}
```

```
// Health.cs
public void TakeDamage(float amount)
{
    currentHealth -= amount;
    if (currentHealth <= 0)
    {
        Die();
    }
    onHealthChanged?.Invoke();
}
```

### My Contributions
Designed the UI using world-space Canvas and integrated it with enemies

Implemented real-time health updates via event callbacks (onHealthChanged)

Added LookAt logic to make UI always face the camera

Optimized rendering performance for multiple enemies on screen

---


## 🐝 Canbee: Flying Enemy AI

### Feature Description
>"Canbee" is a flying enemy that detects and chases the player within a specified range. It flips direction based on player position and initiates an attack when close enough.

### Key Logic
```csharp
// CanbeeController.cs
void Update()
{
    if (Vector2.Distance(player.position, transform.position) < detectRange)
    {
        MoveTowardsPlayer();
        FlipDirection();
    }
}

void MoveTowardsPlayer()
{
    Vector2 dir = (player.position - transform.position).normalized;
    rb.velocity = dir * moveSpeed;
}
```

---
## 👥 Team Collaboration & Feedback
### Enemy Team Composition
Me: Canbee enemy logic, enemy health bar system
Member A: Ground enemy behavior, basic FSM structure
Member B: Boss enemy, phase-based skills and effects

### Feedback I Received
(from Member A): “The Canbee AI is stable and works well even in edge cases like large distances.”
(from Member B): “The health bar UI looks clean and doesn't affect performance even with multiple enemies.”


---
## 💡 Key Learnings
- I gained experience in combining AI logic with animation, physics, and UI in Unity.
- Learned to handle coordinate system differences between UI elements and world objects.
- Understood the importance of separating logic concerns (AI, damage, rendering) for maintainability.
- I plan to further explore FSM (Finite State Machine) patterns and ScriptableObject-based architecture for reusable enemy behaviors.

---


## 📁 Relevant File Paths

| Directory                             | Description                       |
| ------------------------------------- | --------------------------------- |
| `Assets/Scripts/Enemies/Canbee/`      | Canbee behavior scripts           |
| `Assets/Scripts/UI/EnemyHealthBar.cs` | Enemy health bar UI logic         |
| `Assets/Scripts/Enemy/Health.cs`      | Health management for all enemies |

