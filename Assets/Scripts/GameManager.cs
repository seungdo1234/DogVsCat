using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject normalCatPrefab;
    [SerializeField] private GameObject fatCatPrefab;
    [SerializeField] private GameObject pirateCatPrefab;
    [SerializeField] private GameObject retryBtn;
    [SerializeField] private Text levelText;
    [SerializeField] private RectTransform levelFront;
    [HideInInspector] public bool isPlay;

    private int level = 1;
    private int score = 0;
    private int percent = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPlay = true;
        Time.timeScale = 1f ;
    }

    private void Start()
    {
        // cat 엔티티 반복 생성
        InvokeRepeating(nameof(MakeCat), 0, 1f);
    }

    void MakeCat() // cat 엔티티 생성
    {
        Instantiate(normalCatPrefab);
        percent = Random.Range(0, 10);
        switch (level)
        {
            case 0: // lv.1 20% 확률로 고양이를 더 생성
                if (percent < 2)
                {
                    Instantiate(normalCatPrefab);
                }
                break;
            case 1: // lv.2 50% 확률로 고양이를 더 생성
                if (percent < 5)
                {
                    Instantiate(normalCatPrefab);
                }
                break;
            case 2:  // lv.3 FatCat 생성
                if (percent < 5)
                {
                    Instantiate(fatCatPrefab);
                }
                break; 
            default:  // lv.4 PirateCat 생성
                if (percent < 5)
                {
                    Instantiate(fatCatPrefab);
                    Instantiate(pirateCatPrefab);
                }
                break;
        }
    }

    public void GameOver() // 게임 오버
    {
        isPlay = false;
        Time.timeScale = 0f ;
        retryBtn.SetActive(true);
    }

    public void AddScore() // 점수 추가
    {
        score++;
        level = score / 5;
        levelFront.localScale = new Vector3((score - level * 5) / 5.0f, 1f, 1f);
        levelText.text = $"{level + 1}";
    }
}
