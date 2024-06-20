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
            if (tuioID == 11)
            {
                LevelManager.main.SpawnBoost();
            }
            if (tuioID == 10)
            {
                LevelManager.main.DeleteBoost();
            }
            string result = "Received Message:";
            result += "TUIO ID: " + message.Values[2].IntValue + "\n";
            result += "X-Coord: " + message.Values[3].FloatValue + "\n";
            result += "Y-Coord: " + message.Values[4].FloatValue + "\n \n \n";
            Debug.Log(result);
        }
        catch (Exception e)
        {

        }
    }
}
