using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCollision : MonoBehaviour
{
    public GameManager gameManager;
    public float collisionCooldown = 3.0f; // Cooldown time in seconds
    private bool canTakeDamage = true; 
    private SpaceShipController spaceShipController;
    void Start()
    {
        spaceShipController = GetComponentInParent<SpaceShipController>();
    }

    // void OnCollisionEnter(Collision collision)
    // pour les obstacles
    void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with a wall or obstacle
        Debug.Log("COLLISION");
        if (other.CompareTag("Obstacle") && canTakeDamage)
        {   
            Debug.Log("EFFECTIVE DAMAGE");
            TakeDamage();
        }
    }

    void TakeDamage(){
        canTakeDamage = false; // Prevent further damage
        gameManager.LoseSpaceshipLife(); // Reduce life
        Debug.Log($"Ship hit! Remaining life: {gameManager.GetSpaceshipLife()}");

        // Damage feedback
        spaceShipController.StopShip();

        // Start cooldown before the ship can take damage again
        Invoke(nameof(ResetDamageCooldown), collisionCooldown);
    }

    void ResetDamageCooldown()
    {
        canTakeDamage = true; // Allow damage again
    }
}
