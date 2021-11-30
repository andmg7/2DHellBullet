using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variable to know if you collected the tripleshoot powerup
    public bool TripleShoot = false;
    //variable to know if you collected the speed powerup
    public bool SpeedBoost = false;
    // variable to know if you collected the Shield powerup
    public bool ShieldsActive = false;

    public int lives = 3;

    [SerializeField]
    private GameObject _ExplosionPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShootPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = .20f;

    private float _canFire = .0f;

    [ SerializeField ]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        // current pos = new position
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine(); 
        }

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;

    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        // if  space key or left clic is pressed  spawn a laser at player position
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {

        // if i have triple shoot  shoot 3 fire if not only shoot one
        
         
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (TripleShoot == true)
            {
                // triple shoot
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                // spawn laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, .88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        //mover de izq a derecha y mover arriba y abajo
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if SpeedPowerUp is enable move 3x the normal speed else move normal
        if( SpeedBoost == true)
        {
            
            transform.Translate(Vector3.right * _speed * 3f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 3f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        

        //if player  is greater than 0  set player in  y 0

        if (transform.position.y > 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //si quiero que se mueva al otro lado  -> if player position on x is greater than 9.5  send to position -9f

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
    }

    //Damage on player
    public void Damage()
    {
        

        if(ShieldsActive == true)
        {
            ShieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        hitCount++;

        if (hitCount == 1)
        {
            //encender el fallo del motor izq
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            //encender el fallo del motor derecho
            _engines[1].SetActive(true);
        }

        //reduce 1 life from player when its damaged and if lives(0) < 1 --> destroy player un metodo para hacerlo Lives = Lives - 1;
        lives--;

        _uiManager.UpdateLives(lives);
        if(lives < 1)
        {
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    // control TripleShoot Function
    public void TripleShootOn()
    {
        TripleShoot = true;
        StartCoroutine(TripleShootDownRoutine()); 
    }

    public IEnumerator TripleShootDownRoutine()
    {
        yield return new WaitForSeconds(8f);
        TripleShoot = false;
    }

    //control SpeedPowerUp -- method to enable SpeedBoost
    public void SpeedBoostOn()
    {
        SpeedBoost = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    //courutine method (ienumerator) to powerdown the speedbost
    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(8f);
        SpeedBoost = false;
    }

    public void EnableShields()
    {
        ShieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    
}


//si quiero limitar hasta donde llega el player
//if player  is greater than 0
// set player in  x 0

/* if(transform.position.x > 8.2)
 {
     transform.position = new Vector3(8.2f, transform.position.y, 0);
 }else if( transform.position.x < -8.2f)
 {
     transform.position = new Vector3(-8.2f, transform.position.y, 0);
 }
*/
