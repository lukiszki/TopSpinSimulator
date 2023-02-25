using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Automat : MonoBehaviour
{
    
    [SerializeField]
    Naped naped;

    [SerializeField]
    float timeScale;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;

        // if(Input.GetKeyDown(KeyCode.K))
        // {
        //    StartAuto();
        //}
    }
    public void StartAuto()
    {
        StopAllCoroutines();
        StartCoroutine(Auto());
    }
    public void Stop()
    {
        StopAllCoroutines();
        naped.GodPower(0);
        naped.SetSpeed(0);
    }
    public void StartPark()
    {
        StopAllCoroutines();
        StartCoroutine(Park());
    }
    public void StartTarzan()
    {
        StopAllCoroutines();
        StartCoroutine(Tarzan());
    }
    public void StartKaczuchy()
    {
        StopAllCoroutines();
        StartCoroutine(Kaczuchy());
    }
    public void StartMortal()
    {
        StopAllCoroutines();
        StartCoroutine(Mortal());
    }
    IEnumerator Park()
    {
        naped.GodPower(0);
        if (naped.osObr.rotation.eulerAngles.z != 90)
        {
            naped.SetSpeed(0.3f);
            while (naped.osObr.rotation.eulerAngles.z < 90)
                yield return null;

            naped.SetSpeed(-0.3f);
            while (naped.osObr.rotation.eulerAngles.z > 90)
                yield return null;
            naped.SetSpeed(0);

        }
        naped.SetBrake(false);
        yield return new WaitForSeconds(2f);
        naped.SetSpeed(0.3f);
        yield return new WaitForSeconds(2f);

        while (naped.osObr.rotation.eulerAngles.z < 130f)
            yield return null;
        naped.SetSpeed(0f);
        yield return new WaitForSeconds(1f);
        naped.SetBrake(true);

        naped.SetSpeed(-0.2f);
        while (naped.osObr.rotation.eulerAngles.z > 90)
            yield return null;
        naped.SetSpeed(0.1f);
        while (naped.osObr.rotation.eulerAngles.z < 90)
            yield return null;
        naped.SetSpeed(0.00f);
        naped.SetBrake(false);
        print(naped.gondola.rotation.eulerAngles.x);

        while (naped.gondola.rotation.eulerAngles.x > 290f)
        {

            yield return null;
        }
        naped.SetBrake(true);




    }
    float speed = 0;
    float D()
    {
        return Time.deltaTime * 60;
    }
    IEnumerator Auto()
    {
        //START
        naped.SetBrake(true);
        naped.GodPower(0);
        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z > 1f)
        {

            speed = Mathf.Lerp(speed, -0.34f, 0.004f*D());
            naped.SetSpeed(speed);
            yield return null;
        }

        naped.SetSpeed(0);
        yield return new WaitForSeconds(3);
        naped.SetSpeed(0.2f);
        yield return new WaitForSeconds(4);


        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z < 180f)
        {
            speed = Mathf.Lerp(speed, 0.35f, 0.01f * D());
            naped.SetSpeed(speed);
            yield return null;
        }
        naped.SetSpeed(0);


        while (naped.gondola.angularVelocity.x > 0.15f)
        {
            yield return null;
        }
        //COME ON
        yield return new WaitForSeconds(0.5f);

        naped.SetBrake(false);
        yield return new WaitForSeconds(2.5f);

        naped.SetSpeed(-0.1f);
        while (naped.osObr.rotation.eulerAngles.z > 90f)
        {
            yield return null;
        }
        //naped.SetBrake(true,0.005f);
        yield return new WaitForSeconds(2);
        naped.SetBrake(false);
        while (naped.osObr.rotation.eulerAngles.z < 1f)
        {
            //print("czekam");
            yield return null;
        }

        // HAHAHA     275
        //print(naped.gondola.rotation.eulerAngles.x);
        //Debug.Break();
        while (naped.gondola.rotation.eulerAngles.x < 265 || naped.gondola.rotation.eulerAngles.x > 285)
        {
            //print(naped.gondola.rotation.eulerAngles.x);
            yield return null;
        }


        naped.SetBrake(true, 0.023f, -100);
        naped.SetSpeed(-0.15f);
        yield return new WaitForSeconds(1);

        while (naped.osObr.rotation.eulerAngles.z > 190f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        naped.SetSpeed(0.15f);
        yield return new WaitForSeconds(1f);
        naped.SetSpeed(0f);
        yield return new WaitForSeconds(2f);


        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z > 110f)
        {
            speed = Mathf.Lerp(speed, -0.12f, 0.0035f * D());
            naped.SetSpeed(speed);
            yield return null;
        }
        naped.SetBrake(false, 0.01f, -100);
        naped.SetSpeed(-0.10f);
        while (naped.osObr.rotation.eulerAngles.z > 45f)
        {

            yield return null;
        }
        naped.SetSpeed(0);
        yield return new WaitForSeconds(3);

        yield return StartCoroutine(Tarzan());
        yield return new WaitForSeconds(2);


        yield return StartCoroutine(Kaczuchy());

        yield return StartCoroutine(Mortal());

    }

    IEnumerator Tarzan()
    {
        naped.SetBrake(false);

        naped.SetSpeed(0);
        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z < 200f)
        {
            //            print(speed);

            speed = Mathf.Lerp(speed, 0.7f, 0.0025f * D());
            naped.SetSpeed(speed);
            yield return null;
        }
        while (naped.gondola.rotation.eulerAngles.x < 290 || naped.gondola.rotation.eulerAngles.x > 303)
        {
            print(naped.gondola.rotation.eulerAngles.x);
            yield return null;
        }

        naped.SetSpeed(1);
        naped.SetBrake(true, 0.01f, 90);
        while (naped.osObr.rotation.eulerAngles.z < 320f)
        {
            yield return null;
        }
        naped.SetBrake(false);
        while (naped.osObr.rotation.eulerAngles.z < 355f)
        {
            yield return null;
        }

        naped.SetSpeed(0.3f);
        // print(naped.osObr.rotation.eulerAngles.z);
        yield return new WaitForSeconds(1);
        naped.SetSpeed(0f);
    }
    IEnumerator Kaczuchy()
    {
        yield return StartCoroutine(ParkToUp());
        //KACZUCHY
        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z > 100f)
        {
            speed = Mathf.Lerp(speed, -1, 0.0025f * D());
            naped.SetSpeed(speed);
            yield return null;
        }
        naped.SetBrake(false);

        //yield return new WaitForSeconds(1f);
        //naped.GodPower(40);


        naped.SetSpeed(-0.0f);
        naped.AddPower(1000);
        yield return new WaitForSeconds(1f);
        naped.AddPower(0);
        naped.GodPower(20);

        while (naped.gondola.rotation.eulerAngles.x > 70 && naped.gondola.rotation.eulerAngles.x < 50f)
        {
            yield return null;
        }
        naped.SetSpeed(-1.0f);
        yield return new WaitForSeconds(1f);
        naped.SetSpeed(-0.8f);
        naped.GodPower(5);

        while (naped.osObr.rotation.eulerAngles.z > 2f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (naped.osObr.rotation.eulerAngles.z > 250f)
        {
            yield return null;
        }
        naped.SetBrake(true, 0.2f, 180);
        yield return new WaitForSeconds(1f);
        while (naped.osObr.rotation.eulerAngles.z > 120f)
        {
            yield return null;
        }
        naped.SetBrake(false);
        yield return new WaitForSeconds(1f);
        while (naped.osObr.rotation.eulerAngles.z > 2f)
        {
            yield return null;
        }

    }


    IEnumerator ParkToUp()
    {
        naped.osObr.angularDrag = 1.0f;

        naped.SetBrake(false);

        if (naped.osObr.rotation.eulerAngles.z < 90)
        {
            naped.SetSpeed(-0.1f);
            while (naped.osObr.rotation.eulerAngles.z > 5)
                yield return null;

            yield return new WaitForSeconds(1f);

            while (naped.osObr.rotation.eulerAngles.z > 260)
                yield return null;


            naped.SetSpeed(0f);

            yield return new WaitForSeconds(1f);
            naped.SetBrake(true);

        }
        else
        {
            naped.SetSpeed(0.1f);
            while (naped.osObr.rotation.eulerAngles.z < 200)
                yield return null;



            naped.SetSpeed(0f);

            yield return new WaitForSeconds(1f);
            naped.SetBrake(true);

        }
        naped.SetSpeed(0.05f);
        while (naped.osObr.rotation.eulerAngles.z < 250)
            yield return null;
        naped.SetSpeed(0f);
        naped.SetBrake(false);

        while (naped.gondola.rotation.eulerAngles.x > 280)
            yield return null;
        naped.SetBrake(true);
    }
    IEnumerator Mortal()
    {

        yield return StartCoroutine(ParkToUp());

        naped.SetSpeed(0.03f);
        yield return new WaitForSeconds(3f);

        naped.SetSpeed(-0.03f);
        yield return new WaitForSeconds(4f);
        naped.SetSpeed(0.03f);
        yield return new WaitForSeconds(2f);
        naped.SetSpeed(0f);
        yield return new WaitForSeconds(1f);



        //<10
        //280

        //MORTAL

        naped.osObr.angularDrag = 3.0f;
        speed = 0;
        while (naped.osObr.rotation.eulerAngles.z < 355f)
        {
            //Debug.Break();
            //print(speed);
            speed = Mathf.Lerp(speed, 0.45f, 0.0025f * D());
            naped.SetSpeed(speed);
            //print(naped.osObr.rotation.eulerAngles.z);
            yield return null;
        }
        naped.SetSpeed(0.5f);
        yield return new WaitForSeconds(0.5f);
        while (naped.osObr.rotation.eulerAngles.z < 120f)
        {
            yield return null;
        }

        naped.SetBrake(false);//Relase brake
        naped.GodPower(80);




        //wait for relalising joysick
        while (naped.gondola.rotation.eulerAngles.x > 5f)
        {
            yield return null;
        }


        yield return new WaitForSeconds(0.5f);
        while (naped.gondola.rotation.eulerAngles.x > 310f)
        {
            yield return null;
        }
        naped.SetSpeed(0f);//relase joystic

        //float time = Time.timeSinceLevelLoad;
        //while(true)
        //{
        //    print(Time.timeSinceLevelLoad - time);
        //    yield return null;
        // }
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.55f);
            naped.SetSpeed(0.5f);
            yield return new WaitForSeconds(0.4f);
            naped.SetSpeed(0.0f);
        }
        yield return new WaitForSeconds(0.55f);

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.55f);
            naped.SetSpeed(-0.3f);
            yield return new WaitForSeconds(0.4f);
            naped.SetSpeed(0.0f);
        }
        yield return new WaitForSeconds(15f);
        naped.osObr.angularDrag = 1.0f;

        yield return StartCoroutine(Park());











        naped.SetSpeed(-0.45f);
        yield return new WaitForSeconds(0.15f);
        naped.SetSpeed(0f);
        //naped.gondola.rotation.eulerAngles.x <170&&naped.gondola.rotation.eulerAngles.x >180f
        while(naped.gondola.rotation.eulerAngles.x >60&&naped.gondola.rotation.eulerAngles.x >5f)
        {
            //print(naped.gondola.rotation.eulerAngles.x);
            yield return null;
        }

        naped.SetSpeed(1f);
        yield return new WaitForSeconds(0.5f);
        naped.SetSpeed(0f);

        naped.GodPower(40);

        yield return new WaitForSeconds(1.5f);


        naped.SetSpeed(0.7f);
        yield return new WaitForSeconds(0.5f);
        naped.SetSpeed(0f);

        yield return new WaitForSeconds(1.0f);


        naped.SetSpeed(0.7f);
        yield return new WaitForSeconds(0.5f);
        naped.SetSpeed(0f);

        yield return new WaitForSeconds(1.0f);


        naped.SetSpeed(0.035f);
        yield return new WaitForSeconds(6.0f);
        print(naped.osObr.rotation.eulerAngles.z);
        while(naped.osObr.rotation.eulerAngles.z < 90f)
            yield return null;
            
        naped.SetSpeed(0f);
        yield return new WaitForSeconds(9.0f);

        naped.SetBrake(true,0.02f,-150);
        yield return new WaitForSeconds(1f);
        naped.SetBrake(true,0.1f,-150);
        yield return new WaitForSeconds(2f);
        naped.SetBrake(false);

        

        

        naped.GodPower(10);

    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
