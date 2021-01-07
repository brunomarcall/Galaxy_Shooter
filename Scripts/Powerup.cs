using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _audioClip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Collided with:" + other.name);

        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
                if (powerupID == 0) 
                {
                    player.TripleShotPowerupOn();
                }

                else if (powerupID == 1) 
                {
                    player.SpeedPowerupOn();

                }

                else if(powerupID == 2)
                {
                    player.ShieldsOn();
                }
                
            }
            StartCoroutine(player.TripleShotPowerDownRotine());
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
        
    }
}
