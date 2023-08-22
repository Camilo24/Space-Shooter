using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float _sp = 8.0f;
    [SerializeField] private GameObject _laserContainer;
    private AudioSource _audioSource;
    private GameObject enemylaser;

    private void Start()
    {
        enemylaser = this.gameObject;
        _audioSource = enemylaser.GetComponent<AudioSource>();
        Vector3 newRotation = new Vector3(0, 0, 180);
        enemylaser.transform.rotation = Quaternion.Euler(newRotation);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _sp * Time.deltaTime * -1);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
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
    }

    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
