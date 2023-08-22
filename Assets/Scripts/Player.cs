using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _megaspeed = 1115f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private bool isTripleShotActive = false;
    [SerializeField] private bool isSpeedActive = false;
    [SerializeField] private bool isShieldActive = false;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private int _score = 0;
    [SerializeField]private GameObject _oneLive;
    [SerializeField]private GameObject _twoLive;
    [SerializeField] AudioSource _laser;
    [SerializeField] AudioSource _explosion;
    [SerializeField] AudioSource _powerup;
    private GameManager _gameManager;
    private UIManager _uiManager;
    public bool isPlayerOne;
    public bool isPlayerTwo;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _shieldPrefab.SetActive(false);
        _oneLive.SetActive(false);
        _twoLive.SetActive(false);

        if (_spawnManager is null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        if (_gameManager.isCoopMode is false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    void Update()
    {
        if (isPlayerOne && _gameManager._isGameOver is false)
        {
            CalculateMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
        
        if (isPlayerTwo && _gameManager._isGameOver is false)
        {
            CalculateMovement2();

            if (Input.GetKeyDown(KeyCode.O) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (isSpeedActive)
        {
            transform.Translate(direction * _megaspeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void CalculateMovement2()
    {
        float horizontalInput2 = Input.GetAxis("Horizontal2");
        float verticalInput2 = Input.GetAxis("Vertical2");

        Vector3 direction = new Vector3(horizontalInput2, verticalInput2, 0);

        if (isSpeedActive)
        {
            transform.Translate(direction * _megaspeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.9246438f, 0.414901f, -11.21959f), Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }

        _laser.Play();
    }

    public void Damage()
    {
        if(isShieldActive)
        {
            isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _lives --;

        _uiManager.UpdateLives(_lives);

        if (_lives is 2)
        {
            _twoLive.SetActive(true);
        }

        if (_lives is 1)
        {
            _oneLive.SetActive(true);
        }

        if (_lives is 0)
        {
            _explosion.Play();
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        isTripleShotActive = true;
        _powerup.Play();
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

    public void isSpeedActived()
    {
        _powerup.Play();
        isSpeedActive = true;
        StartCoroutine(SpeedActive());
    }

    IEnumerator SpeedActive()
    {
        yield return new WaitForSeconds(5f);
        isSpeedActive = false;
    }

    public void isShieldActived()
    {
        _powerup.Play();
        isShieldActive = true;
        _shieldPrefab.SetActive(true);
    }

    public void plusScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
        _uiManager.BestScore(_score);
    }
}