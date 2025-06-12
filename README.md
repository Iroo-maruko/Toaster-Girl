# 🕹️ Toasty-Girl: Enemy

> This document summarizes my individual contributions to the Unity-based 2D action game **Toaster-Girl**, focusing on enemy behavior and UI systems.

> I worked as part of the *Enemy Team* and was specifically responsible for implementing the flying enemy "Canbee" and the enemy health bar system.






## 📌 Project Overview

- **Project Name:** Toaster-Girl
- **Genre:** 2.5D Action Platformer
- **Development Tools:** Unity (C#), Unity Animator, UI (Canvas)
- **Team:** Enemy Team
- **My Role:** Junior Developer

Toaster-Girl은 2.5D 기반 게임이며 배경과 요소들은 3D로 구성되어있지만 움직임이 2D로 제한되어있는 게임을 2.5D 게임이라고 한다.


## 🧩 Summary of My Contributions

| Feature | Description | Script Files |
|--------|-------------|--------------|
| 🪫 Enemy Health Bar UI | Health bar displayed above each enemy in real time | `EnemyHealthBar.cs`, `Health.cs` |
| 🐝 Enemy "Canbee" | A flying enemy that tracks and attacks the player | `CanbeeController.cs`, `CanbeeAttack.cs` |


drop
sprint 1
<img width="485" alt="image" src="https://github.com/user-attachments/assets/b4dfa13e-25d7-43a7-ad45-b114ad24a00b" />

design that sara made
<img width="571" alt="image" src="https://github.com/user-attachments/assets/fa26d119-0a73-4c86-ad0d-4cd737e22a8d" />

detail design and bee hive
<img width="540" alt="image" src="https://github.com/user-attachments/assets/543e2cb7-c4b8-4c95-a7d5-fa2cbbbfb3e1" />

detect


https://github.com/user-attachments/assets/0ea20e42-da7f-4759-a32a-db759684f76e




detect and spray
(detect, follow, spray)
![detect and follow](https://github.com/user-attachments/assets/550585b3-f758-4d69-98fb-2a8e0fb657b6)

<img width="469" alt="followplayer spray" src="https://github.com/user-attachments/assets/b4358486-6fb4-46b7-8736-a4df4e6b2cf8" />
https://github.com/user-attachments/assets/d1246ca7-0885-4ec6-b83c-f1e7f88acd80


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



13/3 (sprint 1)
<img width="1675" alt="image" src="https://github.com/user-attachments/assets/ac992861-330b-46ab-b47f-e95df32deefb" />
<img width="1675" alt="image" src="https://github.com/user-attachments/assets/f607f29e-4995-4100-894c-f62da0a08152" />

27/3 (sprint 2)
<img width="1674" alt="image" src="https://github.com/user-attachments/assets/790e3744-8e34-471a-abd4-6ba0bf2a52f6" />

10/4 (sprint 3)
<img width="1673" alt="image" src="https://github.com/user-attachments/assets/ec793a4b-c55d-4125-ac2f-3f59bab2094d" />

24/4 (sprint 4)
<img width="1680" alt="image" src="https://github.com/user-attachments/assets/aa38523b-74db-4021-8c2c-b217cf4287f8" />

15/5 (sprint 5)
<img width="1685" alt="image" src="https://github.com/user-attachments/assets/c98b5758-d82d-4f30-83e5-637192782a32" />
<img width="1681" alt="image" src="https://github.com/user-attachments/assets/64613e03-f4f9-4fbf-af0f-5228f2796cfc" />

5/6 (sprint 6)
<img width="1698" alt="image" src="https://github.com/user-attachments/assets/d1c474fe-9a66-409d-8bb2-4059cb2d9b9f" />
