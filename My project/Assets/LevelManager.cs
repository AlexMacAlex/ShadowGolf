using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelUIText;
    [SerializeField] private TextMeshProUGUI levelUIHeader;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject boostPad;

    [Header("Attributes")]
    [SerializeField] private int maxStrokes;

    private int strokes;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateStrokeUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))SpawnBoost();
    }

    public void SpawnBoost()
    {
        boostPad.SetActive(true);
    }

    public void DeleteBoost()
    {
        boostPad.SetActive(false);
    }


    public void IncreaseStroke()
    {
        strokes++;
        UpdateStrokeUI();

        if (strokes >= maxStrokes)
        {
            outOfStrokes = true;
        }
    }

    public void LevelComplete()
    {
        levelCompleted = true;

        levelUIText.text = strokes > 1 ? "you putted in " + strokes + " strokes" : "You got a hole in one!";

        levelCompleteUI.SetActive(true);
    }

    public void LevelFailed()
    {
        levelUIHeader.text = "LEVEL FAILED";
        levelUIText.text = "You failed the level. Hit the ball to restart";
        levelCompleteUI.SetActive(true);
    }


    public void GameOver ()
    {
        gameOverUI.SetActive(true);
    }

    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }
}
