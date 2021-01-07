using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lives = 3;
    [SerializeField]
    private float _speed = 3.0f;
    public GameObject EnemyExplosionPrefab;
    private UIManager Stext;
    [SerializeField]
    private AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -7)
        {
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3 (randomX,7.0f,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            lives--;
            if (lives < 1)
            {
                Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);
                Stext = GameObject.Find("Canvas").GetComponent<UIManager>();

                if (Stext != null)
                {
                    Stext.UpdateScore();
                }
            
        }
           
            
        }

        else if(other.tag == "Player")
        {
            Player player = other.GetComponent <Player>();
            if(player != null)
            {
                player.Damage();
            }
            Instantiate(EnemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
           
        }

    }
   
}
