using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int spaceshipLife = 100;
    private float distanceTraveled = 0;

    void Start()
    {
        
    }

    public void LoseSpaceshipLife()
    {
        spaceshipLife -= 33;
    }

    public int GetSpaceshipLife()
    {
        return spaceshipLife;
    }

    public int getDistanceTraveled()
    {
        return (int)distanceTraveled;
    }

    void Update()
    {
         // Calculate distance based on the corridor's speed
        float speed = SpeedManager.Instance.CurrentSpeed;
        distanceTraveled += speed * Time.deltaTime;

          // Check for Game Over
        if (spaceshipLife <= 0) {
            Debug.Log("Game Over!");
            Time.timeScale = 0; // Pause the game
            // TODO Trigger any additional Game Over UI or logic
        }
    }
}
