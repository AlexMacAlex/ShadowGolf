using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform ball;
    public static CameraMovement main;

    private void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = ball.transform.position + new Vector3(-4, 0, -10);
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void ResetCam()
    {
        transform.position = ball.transform.position + new Vector3(-4, 0, -10);
    }
}
