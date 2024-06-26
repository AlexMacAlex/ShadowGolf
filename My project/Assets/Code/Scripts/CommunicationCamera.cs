using extOSC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private OSCReceiver _receiver;

    private const string _oscAddress = "/tuio/2Dobj";


    protected virtual void Start()
    {

        _receiver = gameObject.AddComponent<OSCReceiver>();
        _receiver.LocalPort = 3333;
        _receiver.Bind(_oscAddress, MessageReceived);
    }
    protected void MessageReceived(OSCMessage message)
    {
        try
        {
            int tuioID = message.Values[2].IntValue;
            float tuioX = message.Values[3].FloatValue;
            float tuioY = message.Values[4].FloatValue;
            if (tuioID == 0)
            {
                LevelManager.main.SpawnBoost();
                LevelManager.main.boostPad.transform.position = new Vector3((1 - tuioX) * 10, -tuioY * 6, 0);
            }
            if (tuioID == 1)
            {
                LevelManager.main.SpawnBoost();
                LevelManager.main.boostPadRed.transform.position = new Vector3((1 - tuioX) * 10, -tuioY * 6, 0);
            }
            //string result = "Received Message:";
            //result += "TUIO ID: " + message.Values[2].IntValue + "\n";
            //result += "X-Coord: " + message.Values[3].FloatValue + "\n";
            //result += "Y-Coord: " + message.Values[4].FloatValue + "\n \n \n";
            //Debug.Log(result);
        }
        catch (Exception e)
        {
        }
    }
}
