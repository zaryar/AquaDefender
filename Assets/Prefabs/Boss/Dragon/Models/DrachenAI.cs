using UnityEngine;
using System;

public class DragonAI : MonoBehaviour
{
    private Transform player; // Der Spieler
    private float circleRadius = 10.0f; // Radius des Kreises
    private float circleSpeed = 2.0f; // Geschwindigkeit des Kreisens
    private float circleAngle = 0.0f; // Aktueller Winkel für das Kreisen
    private float flightSpeed = 12.0f; // Geschwindigkeit des Fliegens
    private float flightAltitude = 10.0f; // Zielhöhe des Fluges
    private float groundSpeed = 2.0f; // Geschwindigkeit des Drachens am Boden
    public GameObject iceShardProjectile; // Eisprojektil Prefab
    public GameObject waterJetProjectile; // Wasserstrahl Prefab
    public GameObject waterBombPrefab; // Prefab der Wasserkugel
    public float waterBombSpeed = 10f; // Geschwindigkeit der Wasserkugel
    public int health = 100; // Gesundheit des Drachens
    private int maxHealth = 100;
    private float attackCooldown = 2f; // Zeit zwischen Angriffen
    private int attacksDuringFlight = 0;
    private const int MaxAttacksDuringFlight = 2;
    private bool isPreparingWaterBomb = false;
    public static event Action OnBossDeath;

    private float stopDistance = 1.0f; // Radius, innerhalb dessen der Drache nicht näher zum Spieler geht
    private Animator animator;

    private bool isFlying = true;
    private bool isLanding = false;
    private bool isGrounded = false;



    private Vector3 originalPosition;
    private bool returningToOriginalPosition = false;

    // Schwellenwerte für Flugauslöser
    private float flightHealthThresholds = 74f;

    private float waterBombAttackDelay = 5.0f; // Verzögerung von 5 Sekunden
    private float waterBombAttackTimer = 0.0f; // Timer für den Wasserbombenangriff

    private float waterBlastCooldown = 0.05f; // Cooldown-Zeit in Sekunden
    private float waterBlastTimer = 0f; // Timer für den Cooldown

    private float damageCooldown = 1f; // 2 Sekunden Cooldown
    private float lastDamageTime;

    private Rigidbody drachenRigidbody;


    private void Start()
    {
        maxHealth = health;
        flightHealthThresholds = (maxHealth / 4)*3 - 1;
        drachenRigidbody = GetComponent<Rigidbody>();
        drachenRigidbody.isKinematic = true; // Starte mit Physik aktiviert

        animator = GetComponent<Animator>();

        // Finde das Spieler-GameObject über den Tag und weise seine Transform - Komponente zu
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null) 
        { 
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
    }

    private void Update() 
    {
        if (!CheckPlayerVisibility())
        {
            HandleMovement();
        }
        else if (isFlying)
        {
            CircleAroundPlayer();
        }
        else
        {
            idleAnimation();
        }
        
        

    }

    private void biteAnimation()
    {
        animator.SetBool("isFly", false);
        animator.SetBool("isBite", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isLand", false);
    }
    private void landAnimation()
    {
        animator.SetBool("isFly", false);
        animator.SetBool("isBite", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isLand", true);
    }
    private void idleAnimation()
    {
        animator.SetBool("isFly", false);
        animator.SetBool("isBite", false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isLand", false);
    }
    private void flyAnimation()
    {
        animator.SetBool("isFly", true);
        animator.SetBool("isBite", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isLand", false);
    }

    private void HandleMovement()
    {

        
        Debug.Log("teashold " + flightHealthThresholds+ "  Health "+ health);
        if (health < flightHealthThresholds && isGrounded)
        {
            flightHealthThresholds = flightHealthThresholds - maxHealth / 4 ;
            StartFlying();
            
            //return; // Beende die Methode hier, um zu verhindern, dass andere Bewegungslogiken ausgeführt werden
        }

        if (isFlying)
        {
            Fly();
            flyAnimation();
        }
        else if (!isGrounded)
        {
            StartLanding();
            landAnimation();
        }
        else
        {
            PerformWaterBlastAttack();
            RotateOnGround();
            if (IsPlayerInRange(3f))
            {
                biteAnimation();
                MoveAway();

                if (Time.time - lastDamageTime >= damageCooldown)
                {
                    if (IsPlayerInRange(3f))
                    {
                        player.GetComponent<Health>().TakeDamage(5);
                    }
                    if (IsPlayerInFrontOfDragon(player))
                    {
                        player.GetComponent<Health>().TakeDamage(15);
                    }

                    lastDamageTime = Time.time;
                }
            }
            else
            {
                idleAnimation();
            }
        }
    }

    private void StartFlying()
    {
        isFlying = true;
        isGrounded = false;
        isLanding = false;
        drachenRigidbody.isKinematic = true;

        flyAnimation();
    }


    private void RotateOnGround()
    {
        // Zielposition ist die Position des Spielers
        Vector3 targetPosition = player.position;

        // Berechne die Richtung zum Spieler, ignoriere dabei die Y-Komponente
        Vector3 direction = (new Vector3(targetPosition.x, transform.position.y, targetPosition.z) - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion yRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0); // Behalte nur die Y-Rotation bei

            transform.rotation = Quaternion.RotateTowards(transform.rotation, yRotation, 50 * Time.deltaTime);
        }
    }
    private void MoveAway()
    {
        // Zielposition ist die Position des Spielers
        Vector3 targetPosition = player.position;

        // Berechne die Distanz zwischen Drache und Spieler
        float distance = Vector3.Distance(transform.position, targetPosition);

        float step = groundSpeed * Time.deltaTime;

        // Berechne die Richtung vom Spieler weg
        Vector3 awayFromPlayer = transform.position - targetPosition;

        // Bewege den Drachen in diese Richtung
        transform.position += awayFromPlayer.normalized * step;
    }


    private void MoveOnGround()
    {
        // Zielposition ist die Position des Spielers
        Vector3 targetPosition = player.position;

        // Berechne die Distanz zwischen Drache und Spieler
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Bewege den Drachen nur, wenn die Distanz größer als der Stop-Radius ist
        if (distance > stopDistance)
        {
            float step = groundSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            
        }
    }

    private void StartLanding()
    {
        isLanding = true;
        drachenRigidbody.isKinematic = false;

        // Bewegen Sie den Drachen sanft nach unten
        Vector3 landingPosition = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        float step = flightSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, landingPosition, step);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe Kollision mit dem Boden
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isLanding = false; 
        }
        
        
    }
    bool IsPlayerInFrontOfDragon(Transform playerTransform)
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // Überprüfe, ob der Spieler innerhalb eines bestimmten Winkels vor dem Drachen steht
        if (angle < 45) // 45 Grad Winkel, kann nach Bedarf angepasst werden
        {
            return true;
        }
        return false;
    }

    private void Fly()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, flightAltitude, transform.position.z);
        FlyTowards(targetPosition);

        if (isPreparingWaterBomb)
        {
            MoveDirectlyAbovePlayer();
        }
        else
        {
            CircleAroundPlayer();
        }


        HandleWaterBombAttackTimer();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;

        // Aktualisiere die Gesundheitsanzeige
        UpdateHealthBar(amount);

        if (health <= 0)
        {
            // Führe Aktionen aus, wenn der Drache besiegt ist
            Die();
        }
    }
    public HealthBar3D healthBar;


    private void UpdateHealthBar(int dmg)
    {   

        // Angenommen, deine HealthBar3D-Instanz ist zugänglich
        // Du musst vielleicht einen Weg finden, darauf zu verweisen
        
        if (healthBar != null)
        {
            healthBar.update_healthbar(maxHealth, dmg);
        }
        Debug.Log("Drache ist besiegt!"+ health);
    }

    private void Die()
    {
        // Führe Aktionen aus, wenn der Drache stirbt (z. B. Animationen, Zerstörung des Objekts usw.)
        Destroy(gameObject);
        OnBossDeath?.Invoke();
        Debug.Log("Drache ist besiegt!");
    }


    private void PerformWaterBombAttack()
    {
        isPreparingWaterBomb = true;
    }

    private void MoveDirectlyAbovePlayer()
    {
        // Zielposition direkt über dem Spieler
        Vector3 targetPosition = new Vector3(player.position.x, flightAltitude, player.position.z);

        // Bewegen Sie sich auf die Zielposition zu
        FlyTowards(targetPosition);

        // Überprüfen Sie, ob der Drache über dem Spieler ist
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                             new Vector3(player.position.x, 0, player.position.z)) < 0.3f) // 1.0f ist die Toleranz für die Position
        {
            DropWaterBomb();
        }
    }
    private void DropWaterBomb()
    {
        GameObject waterBomb = Instantiate(waterBombPrefab, transform.position, Quaternion.identity);
        isPreparingWaterBomb = false;
        returningToOriginalPosition = true; // Beginne mit der Rückkehr zur ursprünglichen Position
                                            
    }

    private void HandleWaterBombAttackTimer()
    {
        // Timer-Logik für Wasserbombenangriff
        if (waterBombAttackTimer >= waterBombAttackDelay && attacksDuringFlight < MaxAttacksDuringFlight)
        {
            PerformWaterBombAttack();
            attacksDuringFlight++;
            waterBombAttackTimer = 0.0f; // Timer zurücksetzen
        }
        else if (attacksDuringFlight >= MaxAttacksDuringFlight)
        {
            EndFlying();
        }
        else
        {
            waterBombAttackTimer += Time.deltaTime; // Timer aktualisieren
        }
    }

    private void EndFlying()
    {
        isFlying = false;
        attacksDuringFlight = 0;
        Debug.Log("Ende Fliegt");
    }

    private void CircleAroundPlayer()
    {
        circleAngle += circleSpeed * Time.deltaTime;
        Vector3 circleOffset = new Vector3(Mathf.Sin(circleAngle) * circleRadius, 0, Mathf.Cos(circleAngle) * circleRadius);
        Vector3 targetPosition = new Vector3(player.position.x + circleOffset.x, flightAltitude, player.position.z + circleOffset.z);

        FlyTowards(targetPosition);
    }



    private void FlyTowards(Vector3 targetPosition)
    {
        float step = flightSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Berechnen Sie die Richtung, in die der Drache schauen soll, aber ignorieren Sie die Y-Komponente
        Vector3 direction = (targetPosition - transform.position);
        direction.y = 0; // Ignoriere die Y-Komponente, um nur die horizontale Rotation zu berücksichtigen

        // Stellen Sie sicher, dass die Richtung nicht Null ist
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            float rotationSpeed = 12.0f; // Anpassbar für eine sanftere Rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }


    private bool IsPlayerInRange(float range)
    {
        // Überprüfe die Distanz zum Spieler
        return Vector3.Distance(transform.position, player.position) < range; // Beispiel-Distanz: 10 Einheiten
    }




    private void PerformWaterBlastAttack()
    {
        if (waterBlastTimer > 0)
        {
            waterBlastTimer -= Time.deltaTime;
        }
        if (IsPlayerInRange(10f) && waterBlastTimer <= 0 && !IsPlayerInRange(4f))
        {
            // Offset vorne vom Drachen
            Vector3 spawnPositionOffset = transform.forward * 2.3f; // Zum Beispiel 2 Einheiten nach vorne

            // Spawn-Position ist die aktuelle Position des Drachens plus den Offset
            Vector3 spawnPosition = transform.position + spawnPositionOffset; 
             
            GameObject waterJet = Instantiate(waterJetProjectile, spawnPosition, Quaternion.identity);
            WaterJetProjectile waterJetScript = waterJet.GetComponent<WaterJetProjectile>();

            if (waterJetScript != null)
            {
                waterJetScript.InitializeProjectile(player.transform.position);
            }

            waterBlastTimer = waterBlastCooldown; // Reset des Timers
        }
        else if (!IsPlayerInRange(10f) || (!IsPlayerInRange(3f) && IsPlayerInRange(4f)))
        {
            MoveOnGround();
        }
    }

    private bool CheckPlayerVisibility()
    {
        if (player != null)
        {
            PlayerMovementController playerController = player.GetComponent<PlayerMovementController>();
            if (playerController != null && playerController.invisible)
            {
                return true;
            }
        }
        return false;
    }




    private void PerformIceShardAttack()
    {
        // Beispiel: Erstelle Eissplitter-Projektile und schieße sie auf den Spieler
        if (IsPlayerInRange(20f))
        {
            GameObject iceShard = Instantiate(iceShardProjectile, transform.position, Quaternion.LookRotation(player.position - transform.position));
            // Fügen Sie hier zusätzliche Logik hinzu, um das Projektil zu bewegen oder zu steuern
        }
    }

    private void HandleAttacks()
    {
        // Angriffslogik
        if (attackCooldown <= 0f)
        {
            if (IsPlayerInRange(10f))
            {
                // Wählen Sie einen Angriff basierend auf Ihrem Spiellogik
                PerformIceShardAttack(); // oder PerformWaterBlastAttack();
            }
            attackCooldown = 2f; // Reset des Cooldowns
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
