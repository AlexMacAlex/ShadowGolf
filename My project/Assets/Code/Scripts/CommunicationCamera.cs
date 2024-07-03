using extOSC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

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
        if (LevelManager.main.strokes > 0)
            return;
        try
        {
            Debug.Log("received a message");
            Debug.Log("ID:" + message.Values[1].IntValue);
            int tuioID = message.Values[2].IntValue;
            float tuioX = message.Values[3].FloatValue;
            float tuioY = message.Values[4].FloatValue;
            float rotation = message.Values[5].FloatValue;
            if (tuioID == 4)
            {
                LevelManager.main.SpawnBoost();
                LevelManager.main.boostPad.transform.position = new Vector3((1 - tuioX) * 10, -tuioY * 6, 0);
            }
            if (tuioID == 7)
            {
                LevelManager.main.SpawnBoost();
                LevelManager.main.boostPadRed.transform.position = new Vector3((1 - tuioX) * 10, -tuioY * 6, 0);
            }
            if (tuioID == 3)
            {
                float rot360 = Map(rotation, 0, 6.3f, 0, 360);
                var rot = new Vector3(0, 0, rot360);
                Debug.Log("Rotation:" + rot360);
                LevelManager.main.ventilator.transform.localRotation = Quaternion.Euler(rot);
            }

        }
        catch (Exception e)
        {

        }
        
    }
    static float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
