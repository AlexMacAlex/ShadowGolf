using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CommunicationArduino : MonoBehaviour
{
    public static CommunicationArduino main;
    SerialPort dataStream = new SerialPort("COM3", 9600);
    private string receivedString;
    [SerializeField] private Rigidbody2D rb;
    private bool ableToShoot = false;

    private string[] data;
    private float[] ballData = new float[3];

    // Start is called before the first frame update
    void Start()
    {   
        try
        {
            dataStream.Close();
            dataStream.Open();
            StartCoroutine(Waiter());
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }

    private void Awake()
    {
        main = this;
        StartCoroutine(Waiter());

        Debug.Log("Awake");
    }

    public void InvokeWaiter()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        ableToShoot = false;
        float timeElapsed = 0;

        while (timeElapsed < 2.2f)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        ableToShoot = true;
    }



    public void CloseDataStream()
    {

    dataStream.Close(); 
    
    }

    // Update is called once per frame
    void Update()
    {
        RawImage image = LevelManager.main.ReadyToShootUI.GetComponent<RawImage>();
        if (rb.velocity.magnitude <= 0.2f && ableToShoot)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
        if (dataStream.IsOpen && dataStream.BytesToRead > 0)
        {
            receivedString = dataStream.ReadLine();
            if (receivedString.Contains(','))
            {
                data = receivedString.Split(',');
                int i = 0;
                foreach (string str in data)
                {
                    float v = float.Parse(str);
                    ballData[i] = v;
                    i++;
                }

                if (data.Length == 3)
                {
                    if (ballData[0] != 0 && ballData[1] != 0 && ballData[2] != 0)
                    {
                        printAxValues(ballData);
                        moveBall(ballData);
                    }
                }
            }
        }
    }

    void moveBall(float[] ballData)
    {
        Debug.Log("test");

        if (rb.velocity.magnitude <= 0.2f && ableToShoot == true)
        {
            if (LevelManager.main.levelCompleted == true && rb.velocity.magnitude <= 0.001f)
            {
                Debug.Log("haus");
                StateManager.main.LoadNextLevel();
                return;
            }

            if (LevelManager.main.outOfStrokes && rb.velocity.magnitude <= 0.001f)
            {
                StateManager.main.ReloadLevel();
            }

            if (!LevelManager.main.outOfStrokes && !LevelManager.main.levelCompleted)
            {
                LevelManager.main.IncreaseStroke();

                if ((ballData[0] < 1000 && ballData[0] > -1000) && (ballData[1] < 1000 && ballData[1] > -1000))
                {
                    Vector2 dir = new Vector2(ballData[0] / 800, ballData[1] / 800);
                    rb.velocity = dir;
                }
                else if ((ballData[0] < 2000 && ballData[0] > -2000) && (ballData[1] < 2000 && ballData[1] > -2000))
                {
                    Vector2 dir = new Vector2(ballData[0] / 1500, ballData[1] / 1500);
                    rb.velocity = dir;
                }
                else if ((ballData[0] < 3000 && ballData[0] > -3000) && (ballData[1] < 3000 && ballData[1] > -3000))
                {
                    Vector2 dir = new Vector2(ballData[0] / 2500, ballData[1] / 2500);
                    rb.velocity = dir;
                }
                else
                {
                    Vector2 dir = new Vector2(ballData[0] / 5000, ballData[1] / 5000);
                    rb.velocity = dir;
                }
            }
        }
    }

    void printAxValues(float[] data)
    {
        print($"x= {data[0]} - y= {data[1]} - z= {data[2]}");
    }
}
