/*

ENEMY CLASS
(rev 1)

Original implementation of the enemy class. 

-Henry

*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeration for enemy states
public enum EnemyState {
    Idle,
    Attacking,
    Dodging,
    Retreating,
    Stunned,
    Dead
}

public class Enemy : MonoBehaviour {
    // Basic Properties
    public string enemyName;
    public float health;
    public float maxHealth;
    public float speed;
    public float attackRange;
    public float detectionRange;

    // State Management
    public EnemyState currentState;

    // List of possible attacks
    public List<string> attacks;

    // Cooldowns and timers
    private Dictionary<string, float> attackCooldowns;
    private float stunDuration;

    // Reference to player or target
    private Transform target;

    private void Start() {
        // Init enemy state
        currentState = EnemyState.Idle;
        attackCooldowns = new Dictionary<string, float>();
        foreach (var attack in attacks) {
            attackCooldowns[attack] = 0f; // Set initial cooldowns to 0
        }
    }

    private void Update() {
        // Update state logic
        switch (currentState) {
        //differet states for enemies - updates animations as well as logic
            case EnemyState.Idle:
                //patrols set route, looking for player within set radius (N/A for bosses)
                Patrol();
                DetectTarget();
                break;
            case EnemyState.Attacking:
                //can be parried
                PerformAttack();
                break;
                //cant be hit while dodging, also has cooldown
            case EnemyState.Dodging:
                Dodge();
                break;
            case EnemyState.Retreating:
                Retreat();
                break;
            case EnemyState.Stunned:
                HandleStun();
                break;
                //delete enemy when dead!!! (or memory leak!)
            case EnemyState.Dead:
                HandleDeath();
                break;
        }
    }

    private void Patrol() {
        //idle behavior

        //TODO set enemy logic here!
    }

    private void DetectTarget() {
        //TODO set enemy logic here!

        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRange) {
            currentState = EnemyState.Attacking; // Transition to attacking state
        }
    }

    private void PerformAttack() {
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

    private string ChooseAttack() {
        // Find an available attack based on cooldown
        foreach (var attack in attacks) {
            if (Time.time >= attackCooldowns[attack]) {
                //TODO set enemy logic here! (chose different attack patterns)
                return attack;
            }
        }
        return null;
    }

    private float GetCooldownForAttack(string attack) {
        // Return cooldown duration for a specific attack
        switch (attack) {
        //TODO set enemy logic here! (for cooldowns!)
            case "basic": return 2f;
            case "skill": return 3f;
            default: return 1f;
        }
    }

    private void Dodge() {
        //TODO set enemy logic here! (for dodging!)
    }

    private void Retreat() { 

        //TODO set enemy logic here! (for retreat!)
        //retreat does not apply to mechanical / digital enemies, only living ones (robots can be programmed without fear!)
    }

    private void HandleStun() {
        // Logic for handling stun duration
        stunDuration -= Time.deltaTime;
        if (stunDuration <= 0)
        {
            currentState = EnemyState.Idle;
        }
    }

    private void HandleDeath() {
        // Logic for enemy death
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
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
