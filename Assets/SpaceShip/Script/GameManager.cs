using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int spaceshipLife = 100;
    private float distanceTraveled = 0;
    
    private GameObject audioPlayerShip;
    private AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayerShip = GameObject.Find("AudioPlayerShip");
        audioPlayer = audioPlayerShip.GetComponent<AudioPlayer>();
    }

    public void LoseSpaceshipLife()
    {
        spaceshipLife -= 33;
        audioPlayer.PlayDamage();

        if (spaceshipLife == 1 )
        {
            Debug.Log("Spaceship life is 1");
            audioPlayer.PlayAlarm();
        }

        if (spaceshipLife <= 0) // Check for Game Over
        {
            spaceshipLife = 0;
            Debug.Log("Game Over!");
            Time.timeScale = 0; // Pause the game
            // TODO Trigger any additional Game Over UI or logic
        }
    }

    public int GetSpaceshipLife()
    {
        return spaceshipLife;
    }

    public int GetDistanceTraveled()
    {
        return (int)distanceTraveled;
    }

    void Update()
    {
         // Calculate distance based on the corridor's speed
        float speed = SpeedManager.Instance.CurrentSpeed;
        distanceTraveled += speed * Time.deltaTime;
    }
}
