using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = -10f;
    private GameObject asteroid;
    private SpawnManager _spawnManager;
    [SerializeField] AudioSource _explosion;

    void Start()
    {
        asteroid = this.gameObject;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 1);
        transform.Rotate(direction *  _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Laser")
        {
            Destroy(other.gameObject);
            StartCoroutine(DestroyRoutine());
        }
    }

    IEnumerator DestroyRoutine()
    {
        Color newColor = new Color(0xD7 / 255f, 0x63 / 255f, 0x34 / 255f);
        asteroid.GetComponent<SpriteRenderer>().color = newColor;
        yield return new WaitForSeconds(0.1f);
        _spawnManager.StartSpawning();
        _explosion.Play();
        Destroy(this.gameObject);
    }
}
