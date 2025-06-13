# 🎩 Toaster-Girl: Enemy
![image](https://github.com/user-attachments/assets/2197b6e6-2c48-4550-8375-276b6dea8d7f)

This document describes the design, development process, and technical implementation of my contributions to the Unity-based 2D action game **Toaster-Girl**. 

My focus was on three key features: **the flying enemy "Canbee", the enemy health bar UI, and the item drop logic**. It also includes sprint progress and feedback from teammates.


----

## 🌐 Student Info
- Name: Iroo Ki
- Student ID: 497662
- Course: Project Game Design and Development (GDD Minor)


## 🎮 Game Overview

>  Project Name: Toaster-Girl

 A 3D platformer in a world where everyone lives their life in the afterlife as the object relating to their death. The story follows Toastergirl, who is out to set on an adventure as the looking thread of the evil Testoasterone overshadows the world.

>  Genre: 2.5D Action Platformer

Toaster-Girl is a 2.5D side-scrolling action platformer set in a deceptively cheerful world.

The game features bright and colorful 3D models in a lighthearted art style, designed to mask the darker reality that most characters have met tragic, object-related fates. 

While the environment and characters are rendered in 3D, the gameplay is restricted to a 2D plane, creating a classic 2.5D gameplay experience.

> Engine: Unity
> 
> Language: C#
> 
> Team: Enemy Team
> 
> Role: Junior Developer



## 🧰 Summary of My Contributions

| Feature | Description | Script Files |
|--------|-------------|--------------|
| 🏹 Item Drop | Random item spawning upon enemy defeat | `ItemDrop.cs`, `EnemyDeathHandler.cs` |
| 🪫 Enemy Health Bar UI | Health bar displayed above each enemy in real time | `EnemyHealthBar.cs`, `Health.cs` |
| 🐝 Enemy "Canbee" | A flying enemy that tracks and attacks the player | `CanbeeController.cs`, `CanbeeAttack.cs` |


# 

### 🏹 Item Drop System

#### - Feature Description
: When an enemy dies, it may drop an item that the player can collect.

#### - Key Logic

```
// Enemy_die.cs
void Die()
{
    if (dropItem != null)
    {
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    Debug.Log("Enemy has died!");

    gameObject.SetActive(false);
}
```
#### - Implementation

https://github.com/user-attachments/assets/3b0633bf-d661-42f9-b4c8-548582e67548

**As shown in the video, when the enemy's health reaches zero, the enemy is deactivated using `SetActive(false)`, and an item is spawned.**

_Initially, I used `Destroy(gameObject)` to remove the enemy object, but this caused unexpected issues such as prematurely stopping linked Particle Systems, Animations, and throwing `NullReferenceException` due to broken script references.
To resolve these problems, I switched to deactivating the object instead of destroying it, which allowed for a smoother and safer flow of logic._

# 

<img width="350" alt="image" src="https://github.com/user-attachments/assets/50ed892e-df7b-4bc5-a182-31a117eaffe0" />  <img width="350" alt="image" src="https://github.com/user-attachments/assets/9b68fb6f-27e5-421f-930e-bf3255c6a3e5" />

Following the setup and script implementation illustrated in the images above, the functionality was modularized and applied to a Prefab, allowing it to be reused efficiently throughout the project.


#### - Detail

To visually emphasize it as an item, a semi-transparent material was applied, and a gentle floating animation was implemented using a sine wave function, as shown in the code below.

<img width="560" alt="image" src="https://github.com/user-attachments/assets/23d3dca3-9136-4c27-9ff1-257dcec0768f" />

#

_The floating behavior was created as shown in the script below:_
```
/item_move.cs
void Update()
{
    // Floating movement using Sin function
    float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
}
```



# 

### 🪫 Enemy Health Bar UI System 
<img width="178" alt="image" src="https://github.com/user-attachments/assets/5653cfa8-258e-406a-b13d-3ff7225bec9b" />

#### - Feature Description

Each enemy features a world-space health bar that updates in real time and is always oriented toward the camera for maximum readability.
The health bar is positioned relative to the enemy but implemented independently, ensuring it maintains a consistent orientation regardless of the enemy's movement or rotation.

#### - Key Logic

_The `EnemyHP` component receives a health percentage value and visually updates the UI using Unity’s `Image.fillAmount` property. This modular design allows real-time health feedback across multiple enemies with minimal overhead._
```
//EnemyHP.cs
public void TakeDamage(int damage)
{
    currentHealth -= damage;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    UpdateHealthUI();
    Debug.Log($"Enemy HP: {currentHealth}");

    if (currentHealth <= 0)
    {
        Die();
    }
}

```

#

_A centralized HealthBarManager handles the creation, positioning, and cleanup of world-space health bars for each enemy. The bars are registered upon spawn, continuously follow the enemy in screen space, and are removed when the enemy dies._

```
//HealthBarManager.cs

// Position the health bar above the enemy's head
public void RegisterEnemy(Enemy enemy)
{
    if (healthBarPrefab == null || canvasTransform == null)
    {
        return;
    }


    GameObject newHealthBarObj = Instantiate(healthBarPrefab, canvasTransform);
    
    if (newHealthBarObj == null)
    {
        return;
    }

    HealthBar healthBar = newHealthBarObj.GetComponent<HealthBar>();

    if (healthBar != null)
    {
        healthBars[enemy] = healthBar;
        enemy.AssignHealthBar(healthBar); 
        
    }
}

// Instantiate the health bar and link it to the enemy
private void UpdateHealthBarPosition(Enemy enemy)
{
    if (healthBars.ContainsKey(enemy))
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 2f, 0));

        if (screenPos.z < 0) 
        {
            return;
        }

        healthBars[enemy].transform.position = screenPos;
    }
}

```
#### - Implementation


https://github.com/user-attachments/assets/dc307b68-77e8-4ff3-9198-6a2c5eaa5ada


When an enemy spawns, the `HealthBarManager` dynamically instantiates a health bar and links it to the corresponding enemy.
The health bar is positioned above the enemy’s head and is updated every frame to align with the enemy’s world position in screen space.
This implementation ensures that players can easily monitor each enemy's health in real time.

Since the player's attack system has not been implemented yet, **collisions between the player and the enemy were temporarily treated as simulated attacks** for testing purposes.
As shown in the video, the enemy's health decreases on contact, and the health bar updates accordingly in real time.

#

#### - Details

In the initial implementation, I focused solely on functionality and overlooked the fact that the item was a child of the enemy object—causing it to move along with the enemy unintentionally.
Since the item system was not meant to be tied to a single enemy, it needed to function independently across all instances.
Through Eric’s feedback, I was able to identify the issue and progressively improve the structure.


<img width="1024" alt="image" src="https://github.com/user-attachments/assets/be61279c-8dee-4a7b-92c0-2580249a2be7" />

https://github.com/user-attachments/assets/f33190e7-a54b-4507-bcdd-b8209feb7675

---

<img width="1087" alt="image" src="https://github.com/user-attachments/assets/30231e29-0801-4817-a04b-fbc4efdce101" />

https://github.com/user-attachments/assets/463f56d5-316a-49de-ad0a-fbae0e4af8e7


# 

### 🐝 Canbee: Flying Enemy
<img width="270" alt="canbee" src="https://github.com/user-attachments/assets/c5340c1b-2e36-44a9-b525-4f3f84e7cfe6" /> <img width="400" alt="beehive" src="https://github.com/user-attachments/assets/2d15f4de-6522-425b-b9fb-e002cdf5bd50" />

#### - Feature Description


https://github.com/user-attachments/assets/154176cc-cca2-4316-aa4c-a40650863e51


**Canbee** is a flying enemy that detects and chases the player within a specified range. It flips direction based on the player's position and initiates an attack when close enough.
True to its name, Canbee is a hybrid of a spray can and a bee. It has a spray can body featuring bee-like stripes and a cap on top, resembling the lid of a can.
Canbees typically appear in swarms originating from a beehive, enhancing their threat through group behavior.
When attacked by a beehive, there is a 50/50 chance that the player will either take damage or be temporarily slowed, simulating the unpredictability of swarm-based attacks.

#

#### - Key Logic
FlyingEnemyMovement.cs
```

// Implements natural hovering motion similar to a bee
Vector3 GetBeeMotion()
{
    float t = Time.time + timeOffset;
    float x = Mathf.Cos(t * hoverSpeed) * hoverAmount * 0.3f;
    float y = Mathf.Sin(t * hoverSpeed * 1.5f) * hoverAmount;
    return new Vector3(x, y, 0);
}
void HoverMotion()
{
    Vector3 motion = GetBeeMotion();
    transform.position = new Vector3(
        transform.position.x + motion.x * Time.deltaTime,
        hoverCenterHeight + motion.y
    );
}


// Detects and starts chasing the player when they are below
void CheckPlayerBelow()
{
    float horizontalDistance = Vector2.Distance(
        new Vector2(beePos.x, beePos.z),
        new Vector2(playerPos.x, playerPos.z)
    );

    float verticalDistance = beePos.y - playerPos.y;

    if (verticalDistance > 0 && verticalDistance <= detectionHeight &&
        horizontalDistance <= detectionRadiusXZ)
    {
        isFollowingPlayer = true;
    }
    else
    {
        isFollowingPlayer = false;
    }
}

```
#

CanbeeSprayAttack.cs
```
void Update()
{
    if (targetPlayer == null)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }
    }

    if (targetPlayer == null)
        return;

    float distance = Vector3.Distance(transform.position, targetPlayer.position);
    attackTimer -= Time.deltaTime;

    if (distance <= detectionRange && attackTimer <= 0f)
    {
        _BeeAni.SetBool("Spray", true);
        attackTimer = attackCooldown;
    }
}

public void Attack()
{
    if (SprayDamage.Instance._Spray == null)
    {
        SprayDamage.Instance._Spray = this;
        Debug.Log("Adding reference");
    }

    sprayParticle.Play();
    AudioManager.Instance.PlaySound(Spray, transform.position);
}
```

#


#### - Implementation

1️⃣ Initial Detection Logic – Prototype Stage
Before the character design was finalized, we implemented a basic detection logic to test interactions.
When the player moved below or came within a certain radius, the Canbee would detect the presence and print a debug log.

https://github.com/user-attachments/assets/1c0e4c0a-68ed-4a12-b26d-c2d81e0563a1

2️⃣ Movement vs. Stationary Behavior – Design Decision Phase
Once the core design of Canbee was more established, we discussed whether the enemy should remain stationary and spray at a fixed position, or actively follow the player.
After internal discussions and feedback during a team meeting, we decided that dynamic following behavior would lead to richer and more immersive gameplay.

![detect and follow](https://github.com/user-attachments/assets/0974e4bf-3047-428c-baf3-d45977c17e3c)

3️⃣ Final Polish – Visual Integration
The final phase focused on polishing visuals. After integrating textures, particle effects, and animation, Canbee’s behavior was fully synchronized with its flying movement and spray attack.
The enemy now detects, follows, and attacks in a visually coherent and satisfying manner.

https://github.com/user-attachments/assets/63a8eb80-186d-4b01-aa43-ffdcdd253c8c


This iterative process—from simple detection logs to design deliberation and full visual integration—highlights how the Canbee enemy evolved from concept to a gameplay-ready AI with real-time tracking, behavior logic, and particle-based attacks.

#


#### - Details

Initially, the concept imagined each Canbee as an individual unit placed across the level.
However, the design later shifted to a beehive-based system, where multiple Canbees spawn and act from a single origin point.

This led to a visual issue: when all Canbees began following the player, they converged to the exact same position, causing overlapping graphics and visual artifacts.

To resolve this, we applied slight positional offsets to each Canbee at the moment they detect the player. These offsets are randomized once per enemy to prevent visual jittering and create the appearance of a dynamic swarm formation.

```
Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z) + followOffset;
transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);

```
✅ Key points:
- Prevents all enemies from stacking at the same coordinates
- followOffset is randomized only once at detection → smooth movement
- Visually resembles a dispersed swarm, enhancing realism and gameplay feedback

#

During a team meeting, we talked about giving Canbee more than just a simple damage attack.
Because the spray effect looked strong, we decided it would be better to make it slow down the player instead of doing big damage.
This made Canbee more interesting, not just an enemy that reduces health, but one that also makes it harder for the player to move.
```
//SprayDamage.cs

public void SprayEffect()
{
    if (_Spray._DealDamage)
    {
        int damageAmount = 0;
        float stunDuration = 0.3f;
        _Player.TakeDamage(damageAmount, stunDuration);
        Debug.Log("Player took spray damage.");
    }

    else
    {
        _Player.TempStatChange(
            getStat: () => _Player.GetStats().walkSpeed,
            setStat: val => _Player.GetStats().walkSpeed = val,
            debuffAmount: 7f,
            duration: 5f
        );
        Debug.Log("Player slowed by spray.");
    }
   _Spray = null;
}
```
✅ Design Goals:
- Create more diverse player challenges without relying solely on damage
- Introduce status effects to slow movement and increase tension during chase
- Encourage the player to avoid attacks even if the threat is not purely lethal



---


### 🧑‍💼 Team Feedback

1️⃣ Senior Programmer
<img width="1069" alt="image" src="https://github.com/user-attachments/assets/5d1d7d0f-8f37-4e83-9701-a3688a5170af" />
<img width="1068" alt="image" src="https://github.com/user-attachments/assets/735cde6b-442d-4fe3-8963-8f587759c018" />

2️⃣ Art lead
<img width="1086" alt="image" src="https://github.com/user-attachments/assets/b2c0906f-ba25-4925-9bf1-fcb014ed4b2c" />

3️⃣ Senior Programmer
<img width="1065" alt="image" src="https://github.com/user-attachments/assets/42d6ccc3-bbef-4fa5-b68c-2615332f580e" />

#


### 🔧 Key Learnings

During this project, I learned how to work more effectively as a team member. I regularly shared updates during sprint meetings, gave and received feedback, and adjusted my tasks when needed to help the team stay on track. It helped me understand the importance of good communication and planning in team projects.
One of the most interesting parts for me was working on the behavior of Canbee, a flying enemy. I explored different ways it could move and attack, and after discussing with my teammates, I decided to make it follow the player. I also had to fix a problem where all the Canbees were going to the same spot, so I added small random offsets to make them spread out like a real swarm. This process helped me compare different solutions and choose the one that worked best for the game.
To build this, I used things I learned in class like Unity’s movement functions, particle effects, animations, and even some code patterns like Singleton. I was happy to see how those concepts actually worked in a real project.
Also, instead of just making the enemy deal damage, I tried a new idea where the spray slows the player down. I thought this made the gameplay more interesting and matched well with the visual effect of the spray. I got this idea from our team meeting, and it was fun to experiment with something new.
Through all this, I also realized that I really enjoy designing enemy logic and making it feel alive in the game. I’ve set personal goals to improve my skills in animation syncing and gameplay scripting, and I learned a lot by reflecting on what worked and what didn’t during the process.

#

### 📂 Relevant File 
All related images, videos, and scripts are included in this repository.
You can explore and verify everything directly here.
