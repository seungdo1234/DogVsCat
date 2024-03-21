using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    [SerializeField] private GameObject hungryCat;
    [SerializeField] private GameObject fullCat; 
    [SerializeField] private RectTransform front;
    [SerializeField] private int type;
    
    
    private float full = 5.0f;
    private float energy = 0.0f;
    private bool isFull = false;
    private float speed;
    
    private void Start()
    {
        float x = Random.Range(-9.2f, 9.2f);
        float y = Random.Range(30f, 32f);
        transform.position = new Vector2(x, y);

        switch (type)
        {
            case 1:
                full = 5;
                speed = 4f;
                break;
            case 2:
                full = 10;
                speed = 2.8f;
                break;
            case 3:
                full = 5;
                speed = 8f;
                break;
        }
    }

    void Update()
    {
        if (energy < full)
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
            if (transform.position.y < -17.0f)
            {
                GameManager.instance.GameOver();
            }
        }
        else // 배가 다 찼다면
        {
            // 가운데를 바탕으로 왼쪽 오른쪽으로 이동
            if (transform.position.x > 0)
            {
                transform.position += Vector3.right* (speed * Time.deltaTime);
            }
            else
            {
                transform.position += Vector3.left * (speed * Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Food":
                if (energy < full) // 배가 안찼다면
                {
                    energy += 1.0f;
                    front.localScale = new Vector3(energy / full, 1.0f, 1.0f);
                    Destroy(other.gameObject);
                    if (!isFull && energy == full) // 배가 다찼다면
                    {
                            isFull = true;
                            hungryCat.SetActive(false);
                            fullCat.SetActive(true);
                            Destroy(gameObject, 5.0f);
                            GameManager.instance.AddScore();
                    }
                }
                break;
        }
}
}
