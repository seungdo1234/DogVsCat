using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        InvokeRepeating(nameof(MakeFood), 0f, 0.15f);
    }

    private void MakeFood() // 총알 생성
    {
        Vector2 position = transform.position + new Vector3(0, 2f, 0);
  
        Instantiate(foodPrefab, position, quaternion.identity);
    }

    private void Update()
    {
        if(!GameManager.instance.isPlay) // 게임 오버가 됐을 때 Dog의 이동을 멈춤
        {
            return;
        }
        
        // 마우스 포인터의 x위치에 따라 dog의 위치 변경
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        float x = mousePos.x;
        if (x > 9.2f)
        {
            x = 9.2f;
        }
        else if (x < -9.2f)
        {
            x = -9.2f;
        }
        
        transform.position = new Vector2(x, transform.position.y);
    }
}
