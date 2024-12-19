using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource alarm;
    public AudioSource damage;
    public AudioSource acceleration;
    public AudioSource shipMoving;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayAlarm()
    {
        if (!alarm.isPlaying){
            Debug.Log("Playing alarm");
            alarm.Play();
        }
    }
    public void StopAlarm()
    {
        alarm.Stop();
    }

    public void PlayDamage()
    {
        damage.Play();
    }

    public void PlayAcceleration()
    {
        acceleration.Play();    
        // Play the ship moving sound after the acceleration sound
        float accelerationDuration = acceleration.clip.length;
        shipMoving.PlayScheduled(AudioSettings.dspTime + accelerationDuration);

    }

    public void StopShipMoving()
    {
        if (shipMoving.isPlaying){
            shipMoving.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
