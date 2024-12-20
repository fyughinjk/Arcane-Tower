using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Reference to the projectile prefab
    public GameObject projectilePrefab;

    // Offset for spawning the projectile
    public Vector2 projectileSpawnOffset;

    // Projectile speed
    public float projectileSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the fire button (default "Fire1" is left mouse button or Ctrl key)
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 button pressed. Attempting to shoot a projectile.");
            ShootProjectile(); // Call the method to shoot a projectile
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null)
        {
            // Calculate the position to spawn the projectile using the player's position and offset
            Vector2 spawnPosition = (Vector2)transform.position + projectileSpawnOffset;
            Debug.Log("Spawning projectile at position: " + spawnPosition);

            // Instantiate the projectile at the calculated position with no rotation
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Projectile instantiated.");

            // Attempt to get the Rigidbody2D component from the instantiated projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Assign velocity to the projectile so it moves horizontally
                // The direction is determined by the player's local scale (e.g., facing left or right)
                rb.velocity = new Vector2(projectileSpeed * transform.localScale.x, 0);
                Debug.Log("Projectile velocity set to: " + rb.velocity);
            }
            else
            {
                Debug.LogError("Rigidbody2D component not found on projectile prefab.");
            }
        }
        else
        {
            // Log an error if the projectile prefab is not assigned in the Inspector
            Debug.LogError("Projectile prefab is not assigned.");
        }
    }
}
