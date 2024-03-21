using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);

        if (transform.position.y > 27.0f)
        {
            Destroy(gameObject);
        }
    }
}
