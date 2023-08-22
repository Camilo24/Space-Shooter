using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private GameObject villain;
    private AudioSource _audioSource;
    [SerializeField] private float _fireRate; 
    private float _canFire;
    [SerializeField] private GameObject _laserPrefabenemy;

    void Start()
    {
        villain = this.gameObject;
        _audioSource = villain.GetComponent<AudioSource>();
        _fireRate = Random.Range(2f, 5f);
        _canFire = Time.time + _fireRate;
    }

    void Update()
    {
        transform.Translate(UnityEngine.Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new UnityEngine.Vector3(randomX, 7f, 0);
        }

        if (Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _audioSource.Play();

            StartCoroutine(DestroyRoutine());
        }

        if (other.tag is "Laser")
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

            Destroy(other.gameObject);
            
            if (player != null)
            {
                player.plusScore(10);
            }

            _audioSource.Play();

            StartCoroutine(DestroyRoutine());
        }
    }

    IEnumerator DestroyRoutine()
    {
        Color newColor = new Color(0xD7 / 255f, 0x63 / 255f, 0x34 / 255f);
        villain.GetComponent<SpriteRenderer>().color = newColor;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void FireLaser()
    {
        _fireRate = Random.Range(1f, 2f);
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefabenemy, transform.position + new Vector3(0, -1f, 0), UnityEngine.Quaternion.identity);
    }
}
