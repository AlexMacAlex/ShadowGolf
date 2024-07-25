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
    [SerializeField] public GameObject ReadyToShootUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelUIText;
    [SerializeField] private TextMeshProUGUI levelUIHeader;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] public GameObject boostPad;
    [SerializeField] public GameObject jumpPad;

    [Header("Attributes")]
    [SerializeField] private int maxStrokes;

    public int strokes;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;
    [HideInInspector] private bool failSoundPlayed = false;

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
        if (Input.GetKeyDown(KeyCode.C)) SpawnBoost();
        
    }

    public void SpawnBoost()
    {
        boostPad.SetActive(true);
    }

    public void SpawnJumpPad()
    {
        jumpPad.SetActive(true);
    }


    public void DeleteBoost()
    {
        boostPad.SetActive(false);
    }


    public void IncreaseStroke()
    {
        AudioManager audioManager = (AudioManager)Object.FindFirstObjectByType(typeof(AudioManager));
        audioManager.Play("Ball_Hit");
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
        AudioManager audioManager = (AudioManager)Object.FindFirstObjectByType(typeof(AudioManager));
        audioManager.Play("Cheer");
        levelUIText.text = strokes > 1 ? "you putted in " + strokes + " strokes. Hit the ball for the next level!" : "You got a hole in one! Hit the ball for the next level!";
        CommunicationArduino.main.InvokeWaiter();
        levelCompleteUI.SetActive(true);
    }

    public void LevelFailed()
    {
        Debug.Log(levelCompleted);
        if (!failSoundPlayed)
        {
            AudioManager audioManager = (AudioManager)Object.FindFirstObjectByType(typeof(AudioManager));
            audioManager.Play("Level_Fail");
            failSoundPlayed = true;
        }
        levelCompleted = false;
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
