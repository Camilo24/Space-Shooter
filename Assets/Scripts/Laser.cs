using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private GameObject _laserContainer;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8.0f)
        {
            if (transform.parent != null) 
            { 
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
