using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Naped : MonoBehaviour
{


    
    [SerializeField]
    public Rigidbody osObr;

   
    public Rigidbody gondola;

    [SerializeField]
    CapsuleCollider colliderGondoli;


    [SerializeField]
    Vector3 srodekCiezkosciGondoli;

    [SerializeField]
    float speed = 1;

    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float powerSpeed;

    Network net = new Network();

    System.Random rnd = new System.Random();

    [SerializeField]
    bool isAi;

    //SerialController serialController;


    [SerializeField]
    bool isJoystick = true;


    [SerializeField]
    GameObject joystick;


    //[HideInInspector]
    public bool smallPump = false;
    //[HideInInspector]
    public bool mainPump = false;
    [HideInInspector]

    public bool pumpSwitch = false;
    
    public bool AUTO;

    [SerializeField]
    AudioSource rideNoise;

  
    public void ToogleAuto(bool val)
    {
        if(val)
        {
            AUTO = true;
            SetBrake(true,0.01f);
        }
        else
        {
            AUTO = false;
            StopAllCoroutines();
            SetSpeed(0);
            SetBrake(false);
            AddPower(0);
            GodPower(0);
        }
    }
    public void StopPump()
    {
        smallPump = false;
        mainPump = false;
    }

    void Start()
    {
        net.InitValues(ref rnd);

        //serialController = GetComponent<SerialController>();

        osObr.angularDrag = 1.0f;


    }
    public void RandomizeVals()
    {
        net.InitValues(ref rnd);
    }
    public void SetSpeed(float value)
    {
        autoSpeed  = value*3;
    }
    public void SetBrake(bool state,float rate = 0.1f, float value = 0)
    {
        isBrake = state;
        BrakeValue = value;
        BrakeRate =rate;
        firstRot = osObr.rotation.eulerAngles.z;
        firstGonRot = gondola.rotation.eulerAngles.x;
    }

    public void AddPower(float value)
    {
        power = value;
    }
    public void GodPower(float value)
    {
        powerSpeed = value;
    }

    float BrakeValue =  0;

    float BrakeRate = 0.1f;
    bool isBrake = false;

    float autoSpeed = 0;
    float rotat = 0;

[HideInInspector]
   public float rotSpeed;
     float value = 0;
    [Range(-180f,180f)]
     [SerializeField]
        float szy;
        float firstRot = 0;
        float firstGonRot = 0;


        float power =0;


        public bool lockBrake = false;

        
        float volume = 0;
    void Update()
    {
        gondola.AddTorque((gondola.angularVelocity)*7);

        rotSpeed = Mathf.Abs(osObr.angularVelocity.x);

        gondola.centerOfMass = srodekCiezkosciGondoli;
        volume = Mathf.Lerp(volume, rotSpeed, Time.deltaTime);
        rideNoise.volume = Mathf.Abs(volume);


        if (smallPump&&mainPump)
        {
  
            //print(Vector3.left);
            if (!AUTO)
            {
                //print(gondola.transform.rotation.eulerAngles.x);


                //print(osObr.rotation.eulerAngles.z);
                //print(gondola.rotation.eulerAngles.z);

                if (isAi)
                {
                    float[] data = new float[] { osObr.transform.rotation.x, gondola.transform.rotation.x };
                    print(data[0]);
                    if (rotSpeed < maxSpeed)
                        osObr.AddTorque(new Vector3(net.GetValue(data), 0, 0));
                    //print(net.GetValue(data));
                }
                if(Input.GetKey(KeyCode.L))
                if (rotSpeed < maxSpeed)
                    osObr.AddTorque(Vector3.left * 1 * speed);

                rotat = osObr.rotation.eulerAngles.x;

                if (Input.GetKey(KeyCode.P))
                {
                    gondola.AddTorque((gondola.angularVelocity) * powerSpeed);

                }
                //gondola.AddTorque((gondola.angularVelocity)*powerSpeed);


                /* string message = serialController.ReadSerialMessage();

                  if (message == null)
                      return;

                  if (isJoystick&&ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                      Debug.Log("Connection established");
                  else if (isJoystick&&ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                      Debug.Log("Connection attempt failed or disconnection detected");
                  else
                  {

                      value = (toFloat(message)-0.5f)*2;
                      autoSpeed = value;
                      //Debug.Log("Message arrived: " + message);
                      //print(value);
                      if(isJoystick&&rotSpeed<maxSpeed)
                          osObr.AddTorque(Vector3.left*value*speed);
                  }
              }*/
            }
            else
            {
                if (rotSpeed < maxSpeed)
                    osObr.AddTorque(Vector3.left * autoSpeed * speed);
                if (isBrake || lockBrake)
                {
                    /*gondola.angularVelocity = Vector3.Lerp(gondola.angularVelocity,new Vector3(0,0,0),0.9f);
                    float x = -osObr.rotation.eulerAngles.z + BrakeValue;
                    Quaternion fromRotation = gondola.rotation;
                    Quaternion toRotation = Quaternion.Euler(x , 0 ,0);
                    gondola.rotation =   Quaternion.Slerp(fromRotation, toRotation,BrakeRate);*/



                    float x = -osObr.rotation.eulerAngles.z + (firstRot + firstGonRot);
                    // print(firstRot +180);
                    // /print(x);
                    Quaternion fromRotation = gondola.rotation;
                    Quaternion toRotation = Quaternion.Euler(x, 0, 0);
                    gondola.rotation = Quaternion.Slerp(fromRotation, toRotation, 0.1f);
                    gondola.angularVelocity = new Vector3(0, 0, 0);
                }
                gondola.AddTorque((gondola.angularVelocity) * powerSpeed);

                gondola.AddTorque(new Vector3(power, 0, 0));


                //Reczny hamulec

                if (Input.GetKeyDown(KeyCode.R))
                {
                    firstRot = osObr.rotation.eulerAngles.z;
                    firstGonRot = gondola.rotation.eulerAngles.x;
                }

                if (Input.GetKey(KeyCode.R))
                {
                    /*-osObr.rotation.eulerAngles.z ;*/
                    // gondola.angularDrag = 1000;
                    //colliderGondoli.radius = 0.0026f;
                    float x = -osObr.rotation.eulerAngles.z + (firstRot + firstGonRot);
                    // print(firstRot +180);
                    // /print(x);
                    Quaternion fromRotation = gondola.rotation;
                    Quaternion toRotation = Quaternion.Euler(x, 0, 0);
                    gondola.rotation = Quaternion.Slerp(fromRotation, toRotation, 0.1f);
                    gondola.angularVelocity = new Vector3(0, 0, 0);

                }
            }        
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
                firstRot = osObr.rotation.eulerAngles.z;
                firstGonRot = gondola.rotation.eulerAngles.x;
            print(firstGonRot);

        }


        if(Input.GetKey(KeyCode.R)||lockBrake)
        {
            /*-osObr.rotation.eulerAngles.z ;*/
            // gondola.angularDrag = 1000;
                //colliderGondoli.radius = 0.0026f;
                float x = -osObr.rotation.eulerAngles.z + (firstRot + firstGonRot);
            // print(firstRot +180);
                // /print(x);
                Quaternion fromRotation = gondola.rotation;
                Quaternion toRotation = Quaternion.Euler(x , 0 ,0);
                gondola.transform.localRotation =   toRotation;//Quaternion.Slerp(fromRotation, toRotation,0.1f);
                gondola.angularVelocity = new Vector3(0,0,0);
        }   
       
    

        SetJoysticAngle();
    }  
     public void SetRot()
        {
                firstRot = osObr.rotation.eulerAngles.z;
                firstGonRot = gondola.rotation.eulerAngles.x;
        }
    float s=0; 
    void SetJoysticAngle()
    {
         s = Mathf.Lerp(s, -16 + autoSpeed * 25,Mathf.Clamp(Time.deltaTime*5,0,1));
        joystick.transform.localRotation =  Quaternion.Euler(0 , s ,0);
    }
    /*float GetJoystic()
    {
    if(serialController.ReadSerialMessage()== null)
    {
        
        print(serialController.ReadSerialMessage());
        return 0;
    }
        
     float value = 0f;
    try{
    value = (float.Parse(serialController.ReadSerialMessage())-0.5f)*2;
    }
       catch(Exception ex)
    {
        //print(serialController.ReadSerialMessage());
    }
     return value;
       
    }
    float toFloat(string text)
    {
        float value =0;
        
        switch(text[2])
        {
            case '1':
         ///   print("test");
            value += 0.1f;
            break;
             case '2':
            value += 0.2f;
            break;
             case '3':
            value += 0.3f;
            break;
             case '4':
            value += 0.4f;
            break;
             case '5':
            value += 0.5f;
            break;
             case '6':
            value += 0.6f;
            break;
             case '7':
            value += 0.7f;
            break;
             case '8':
            value += 0.8f;
            break;
             case '9':
            value += 0.9f;
            break;
        }
        switch(text[3])
        {
            case '1':
//            print("test");
            value += 0.01f;
            break;
             case '2':
            value += 0.02f;
            break;
             case '3':
            value += 0.03f;
            break;
             case '4':
            value += 0.04f;
            break;
             case '5':
            value += 0.05f;
            break;
             case '6':
            value += 0.06f;
            break;
             case '7':
            value += 0.07f;
            break;
             case '8':
            value += 0.08f;
            break;
             case '9':
            value += 0.09f;
            break;
        }
        return value;
    }*/
}
