using UnityEngine;

public class NewMonoBehaviourScript : Enemy
{
    
    Vector2 moveVec;
    Rigidbody2D rb;

    private float moveTimer = 1.25f; //timer that controls how long enemy moves (I think)
    private float moveTimerLower = -2.25f; //timer that controls stationary time (the timer logic might be backwards...)
    private float x_vel = 0.0f;
    private float y_vel = 0.0f;
    //public float speed = 1.0f;2
    //public float enemyDetectionRange = 2.0f; //radius which notices player
    public float enemyLoseDetectionRange = 4.0f; //radius which loses track of player

      void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void Patrol() {
        //idle behavior


        //simple random movement where robot just kinda wanders
        //needs to be upgraded entirely or be better...
        //being better would entail not walking into walls...
        //either with other kai's pathfinding or a simple hitbox that stops-
        //movement when close enough to a wall.

        if(moveTimer <= 0.0f){  
            x_vel = 0.0f;
            y_vel = 0.0f;

            if (moveTimer <= moveTimerLower){

            //choose random x and y velocity with a minimum
            x_vel = Random.Range(-speed, speed);
            if (x_vel < 0.0f){
                if (x_vel > -speed/4){
                x_vel = -speed/4;
                }
            } else {
                if (x_vel < speed/4){
                    x_vel = speed/4;
                }
            }
            y_vel = Random.Range(-speed, speed);
            if (y_vel < 0.0f){
                if (y_vel > -speed/4){
                y_vel = -speed/4;
                }
            } else {
                if (y_vel < speed/4){
                    y_vel = speed/4;
                }
            }
                moveTimer = Random.Range(.75f, 4.0f);
                moveTimerLower = Random.Range(-.75f, -4.0f);
            }
        }
        rb.linearVelocity = new Vector2(x_vel, y_vel);//moveVec;
        moveTimer -= Time.deltaTime;
    }

    private void Update() {
        base.Update();
        if(currentState != EnemyState.Idle){ //IMPORTANT!!!!!!!! THIS DOES NOT CONSIDER STUNNED OR DEAD YET
            if (target != null && Vector3.Distance(transform.position, target.position) >= enemyLoseDetectionRange) {
                currentState = EnemyState.Idle; // Transition to attacking state
            }
        }
    }

    protected override void DetectTarget() {
        

        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRange) {
            currentState = EnemyState.Attacking; // Transition to attacking state
        }
    }

    protected override void PerformAttack() {


        // Choose an attack and perform it
        string selectedAttack = ChooseAttack();
        if (selectedAttack != null) {
            Debug.Log($"{enemyName} used {selectedAttack}!"); //its super effective (joking)
            //TODO set enemy logic here! (for attacks!)
            attackCooldowns[selectedAttack] = Time.time + GetCooldownForAttack(selectedAttack);
        }
        else {
            currentState = EnemyState.Retreating; // Retreat if no attack available
        }
    }

    protected override string ChooseAttack() {
        // Find an available attack based on cooldown
        foreach (var attack in attacks) {
            if (Time.time >= attackCooldowns[attack]) {
                //TODO set enemy logic here! (chose different attack patterns)
                return attack;
            }
        }
        return null;
    }
   protected override float GetCooldownForAttack(string attack) {
        // Return cooldown duration for a specific attack
        switch (attack) {
        //TODO set enemy logic here! (for cooldowns!)
            case "basic": return 2f;
            case "skill": return 3f;
            default: return 1f;
        }
    }

    protected override void Dodge() {
        //TODO set enemy logic here! (for dodging!)
    }

    protected override void Retreat() { 
        //since robots dont have fear, rather than retreating, the robot will slow down as it charges
        //its next attack.

        Vector3 normalizedGoal = (target.position-transform.position).normalized;
        Debug.Log(normalizedGoal);
        x_vel = normalizedGoal.x;
        y_vel = normalizedGoal.y;
        rb.linearVelocity = new Vector2(x_vel, y_vel);//moveVec;
        //TODO set enemy logic here! (for retreat!)
        //retreat does not apply to mechanical / digital enemies, only living ones (robots can be programmed without fear!)
    }

    protected override void HandleStun() {
        // Logic for handling stun duration
        stunDuration -= Time.deltaTime;
        if (stunDuration <= 0)
        {
            currentState = EnemyState.Idle;
        }
    }

    protected override void HandleDeath() {
        // Logic for enemy death
        Destroy(gameObject);
    }

    protected override void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            currentState = EnemyState.Dead;
        }
        else {
            currentState = EnemyState.Stunned;
            stunDuration = 2f; // Example stun duration, change per enemy type (less for robots, more for bandit)
        }
    }
}
