using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommunicationArduino : MonoBehaviour
{
    SerialPort dataStream = new SerialPort("COM3", 9600);
    private string receivedString;
    [SerializeField] private Rigidbody2D rb;

    private string[] data;
    private float[] ballData = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            dataStream.Open();
        }
        catch (System.Exception)
        {
            Debug.Log("Couldn't find port when opening datastream");
        }

    }

    // Update is called once per frame
    void Update()
    {
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
        if(rb.velocity.magnitude <= 0.2f)
        {
            if (LevelManager.main.outOfStrokes)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            LevelManager.main.IncreaseStroke();
            
            Vector2 dir = new Vector2(ballData[0] / 2000, ballData[1] / 2000);
            rb.velocity = dir;

        }
    }

    void printAxValues(float[] data)
    {
        print($"x= {data[0]} - y= {data[1]} - z= {data[2]}");
    }
}
