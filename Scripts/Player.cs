using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    public int lives = 3;
    public GameObject LaserPrefab;
    public GameObject TripleShotPrefab;
    public GameObject ExplosionPrefab;
    public GameObject Shield;
    public GameObject[] Engines;
    [SerializeField]
    private float _FireRate = 0.25f;
   
    [SerializeField]
    private float _speed = 5.0f;

    private float _speedBoost = 10.0f;

    private float _CanFire = 0.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private Spawn_Manager _spawnManager;
    private AudioSource _audioSource;

    private int hitcount = 0;

    public bool canFireTripleShot = false;
    public bool canSpeedBoost = false;
    public bool canActiveShield = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();


        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }

        _audioSource = GetComponent<AudioSource>();

           hitcount = 0;
}

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
            
        }


    }
    private void Shoot()
    {
        if (Time.time > _CanFire)
        {
            _audioSource.Play();
            if (canFireTripleShot == false)
            {
                Instantiate(LaserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(TripleShotPrefab, transform.position, Quaternion.identity);
               
            }

            _CanFire = Time.time + _FireRate;
        }
    }
    private void Movement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        if (canSpeedBoost == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalinput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalinput);
        }
        else if(canSpeedBoost == true)
        {
            
            transform.Translate(Vector3.right * Time.deltaTime * _speedBoost * horizontalinput);
            transform.Translate(Vector3.up * Time.deltaTime * _speedBoost * verticalinput);
        }
        if (transform.position.y > 4.2)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }
        else if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        if (transform.position.x > 9.4)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    public void Damage()
    {

        if(canActiveShield == true)
        {
            canActiveShield = false;
            Shield.SetActive(false);
            return;
        }
        lives--;
        _uiManager.UpdateLives(lives);
        hitcount ++;
        if (hitcount == 1)
        {
            Engines[0].SetActive(true);
        }

        else if (hitcount == 2)
        {
            Engines[1].SetActive(true);
        }
        if (lives < 1)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
            
        }
    }

    public void ShieldsOn()
    {
        canActiveShield = true;
        Shield.SetActive(true);
    }

    public void TripleShotPowerupOn() 
    {
        canFireTripleShot = true;
        StartCoroutine(TripleShotPowerDownRotine());
    }
    public IEnumerator TripleShotPowerDownRotine() 
    {
        yield return new WaitForSeconds(5.0f);
        canFireTripleShot = false;
    
    }
    public void SpeedPowerupOn() 
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
}
