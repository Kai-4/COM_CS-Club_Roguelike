
/*
PLAYER CLASS
(rev 1)

I've adapted the enemy class for to be the player class, please let me know if this is effective, and if it isnt, I can more a new, more
original one. Just keep me up to date!


-Henry
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeration for player states
public enum playerState {
    Idle,
    Attacking,
    Dodging,
    Stunned,
    Dead
}

public class player : MonoBehaviour {

    //nickname for protaganist 
    public string playerNickname;
    // Basic Properties
    public float level;
    public float health;
    public float maxHealth;
    public float speed;
    public float detectionRange;

    //skills 
    public List<string> skills;

    // State Management
    public playerState currentState;

    // List of possible attacks
    public List<string> attacks;

    // Cooldowns and timers
    private Dictionary<string, float> attackCooldowns;
    private float stunDuration;

    // Reference to player or target
    private Transform target;

    private void Start() {
        // Init player state
        currentState = playerState.Idle;
        attackCooldowns = new Dictionary<string, float>();
        foreach (var attack in attacks) {
            attackCooldowns[attack] = 0f; // Set initial cooldowns to 0
        }
    }

    private void Update() {
        // Update state logic
        switch (currentState) {
        //differet states for enemies - updates animations as well as logic
            case playerState.Idle:
                //patrols set route, looking for player within set radius (N/A for bosses)
                Patrol();
                DetectTarget();
                break;
            case playerState.Attacking:
                //can be parried
                PerformAttack();
                break;
                //cant be hit while dodging, also has cooldown
            case playerState.Dodging:
                Dodge();
                break;
            case playerState.Retreating:
                Retreat();
                break;
            case playerState.Stunned:
                HandleStun();
                break;
                //delete player when dead!!! (or memory leak!)
            case playerState.Dead:
                HandleDeath();
                break;
        }
    }

    private void Patrol() {
        //idle behavior

        //TODO set player logic here!
    }

    private void DetectTarget() {
        //TODO set player logic here!

        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRange) {
            currentState = playerState.Attacking; // Transition to attacking state
        }
    }

    private void PerformAttack() {
        // Choose an attack and perform it
        string selectedAttack = ChooseAttack();
        if (selectedAttack != null) {
            Debug.Log($"{playerNickname} used {selectedAttack}!"); //its super effective (lmfao)
            //TODO set player logic here! (for attacks!)
            attackCooldowns[selectedAttack] = Time.time + GetCooldownForAttack(selectedAttack);
        }
        else {
            return;
        }
    }

    private void useSkill() {
        string selectedSkill = ChooseSkill();
        if (selectedSkill != null) {
            Debug.Log($"{playerNickname} used {selectedSkill}!"); //need more added here, not sure how skill implementaion is going to work so update as needed. 
            //TODO set skill logic here!
        }
        //not sure how to do this skill part, update if needed . -HB
        else {
            return;
        }
    }

    private string ChooseAttack() {
        // Find an available attack based on cooldown
        foreach (var attack in attacks) {
            if (Time.time >= attackCooldowns[attack]) {
                //TODO set player logic here! (chose different attack patterns)
                return attack;
            }
        }
        return null;
    }

    private float GetCooldownForAttack(string attack) {
        // Return cooldown duration for a specific attack
        switch (attack) {
        //TODO set player logic here! (for cooldowns!)
            case "basic": return 2f;
            case "skill": return 3f;
            default: return 1f;
        }
    }

    private void Dodge() {
        //TODO set player logic here! (for dodging!)
    }

    private void HandleStun() {
        // Logic for handling stun duration
        stunDuration -= Time.deltaTime;
        if (stunDuration <= 0)
        {
            currentState = playerState.Idle;
        }
    }

    private void HandleDeath() {
        // Logic for player death
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            currentState = playerState.Dead;
        }
        else {
            currentState = playerState.Stunned;
            stunDuration = 2f; // Example stun duration, change per player type (less for robots, more for bandit)
        }
    }
}
