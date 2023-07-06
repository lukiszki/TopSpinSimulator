﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;

public class Naped : MonoBehaviour
{

    [Header("NAPED")]

    


    [SerializeField]
    HingeJoint osObrHingeJoint;

    public Rigidbody osObr;

 


    [SerializeField]
    JointMotor silnik;

    [SerializeField]

    HingeJoint gondolaHingeJoint;

    public Rigidbody gondola;

    [SerializeField]
    float maxSpeed = 60;
    public bool AUTO;



    public bool pompaLadujaca = false;
    public bool pompaGlowna = false;

    public bool wylacznikPompy = true;

    public bool hamulecPrzelacznik;

    public bool kluczyk=false;


    [SerializeField]
    float NAPIECIE = 0;

    [Header("AUTOMAT")]
    [SerializeField]
    bool HamulecAuto = true;
    [SerializeField]
    float predkoscAuto = 0;

    [Header("Dzwięk")]

    [SerializeField]
    AudioSource pompaDzwiek;

    [SerializeField]
    AudioSource pompaDzwiek2;

    [Header("Kompresor")]

    [SerializeField]
    AudioSource kompresorDzwiekZrodlo;
    [SerializeField]
    AudioClip kompresorStart;
    [SerializeField]
    AudioClip kompresorStop;
    [SerializeField]
    AudioClip kompresorRepeat;
    public bool kompresorPrzelacznik = false;
    [SerializeField]
    bool kompresorStan = false;
    [SerializeField]
    float cisnienie = 0;

    [Header("Wzkaźniki")]

    [SerializeField]
    HingeJoint woltomierz;
    [SerializeField]
    HingeJoint czestotliwosciomierz;
    JointSpring voltspring = new JointSpring();
    JointSpring freqspring = new JointSpring();

    [Header("Podesty")]
    public bool podestPrawyPrzelacznik;
    public bool podestLewyPrzelacznik;

    [SerializeField]
    Animator podestPrawySterownik;
    [SerializeField]
    Animator podestLewySterownik;

    [Header("Manipulator")]
    [SerializeField]
    Transform manipulator;
    float stickPos = 0;
    bool brakeButton = false;

    void Start()
    {
        silnik = osObrHingeJoint.motor;
        silnik.targetVelocity = maxSpeed;
        osObrHingeJoint.useMotor = true;
        UstawWzkazniki();

    }
  
 
        
        float volume = 0;


    void Update() 
    {
        HandleStick();
        if (kluczyk)
        {
            HandleKompresor();
            if (pompaGlowna)
            {
                pompaDzwiek.volume = Mathf.Abs(osObr.angularVelocity.x);
                if (!AUTO)
                {
                    
                    if (stickPos>0)
                    {
                        silnik.force = 2*Mathf.Abs(stickPos);
                        silnik.targetVelocity = maxSpeed;
                    }
                    else if (stickPos < 0)
                    {

                        silnik.force = 2 * Mathf.Abs(stickPos);
                        silnik.targetVelocity = -maxSpeed;
                    }
                    else
                    {
                        silnik.force = 0;
                    }
                }
                else
                {
                    silnik.force = Mathf.Abs(2 * predkoscAuto);
                    silnik.targetVelocity = predkoscAuto > 0 ? maxSpeed : -maxSpeed;
                }
                if (brakeButton || hamulecPrzelacznik || (AUTO && HamulecAuto))
                {
                    gondola.isKinematic = true;
                }
                else
                {
                    gondola.isKinematic = false;
                }
            }
            else
            {
                pompaDzwiek.volume = 0;
            }
        }


        osObrHingeJoint.motor = silnik;
        KorygujWzkazniki();
        UstawPodesty();
    }

    private void HandleStick()
    {
        stickPos = Input.GetAxisRaw("Vertical");
        brakeButton = Input.GetAxisRaw("Hamulec") == 1;
        manipulator.localRotation = Quaternion.Euler(0, -16.227f + stickPos*20.0f, 0);
    }

    internal void PompaStop()
    {
        pompaLadujaca = false;
        pompaGlowna = false;
        StartCoroutine(FadeOut(pompaDzwiek2, 0.2f));
    }
    

    public void PompLadujacaStart()
    {
        if (!wylacznikPompy&&!pompaLadujaca&&!pompaGlowna&&kluczyk)
            pompaLadujaca = true;
    }
    public void PompGlownaStart()
    {
        if (!wylacznikPompy && pompaLadujaca && !pompaGlowna && kluczyk)
        {
            pompaGlowna = true;
            StartCoroutine(SpadekNapiecia(300));
        }
    }
    private void UstawWzkazniki()
    {
        woltomierz.useSpring = true;
        czestotliwosciomierz.useSpring = true;
        voltspring.damper = 20;
        voltspring.spring = 100;
        voltspring.targetPosition = 0;
        freqspring.damper = 20;
        freqspring.spring = 100;
        freqspring.targetPosition = 0;
        woltomierz.spring = voltspring;
        czestotliwosciomierz.spring = freqspring;
    }
    private void KorygujWzkazniki()
    {
        
        voltspring.targetPosition = NAPIECIE* 0.17625f;
        freqspring.targetPosition = NAPIECIE > 50 ? 48 : 0 ;
        woltomierz.spring = voltspring;
        czestotliwosciomierz.spring = freqspring;
    }

    public void Zasilanie(bool v)
    {
        if(v)
        {
            NAPIECIE = 400; 
        }
        else
        {
            NAPIECIE = 0;
            PompaStop();
        }
    }
    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    IEnumerator SpadekNapiecia(float napiecieSpadku)
    {
        NAPIECIE = napiecieSpadku;

        while (NAPIECIE < 399.999f)
        {
            NAPIECIE = Mathf.Lerp(NAPIECIE, 400, 4f * Time.deltaTime);

            yield return null;
        }
        NAPIECIE = 400;
    }

    internal void UstawPodesty()
    {
        if (kluczyk && cisnienie > 4)
        {
            podestPrawySterownik.SetBool("isDown", podestPrawyPrzelacznik);
            podestLewySterownik.SetBool("isDown", podestLewyPrzelacznik);
        }

    }
    internal void SetSpeed(float speed)
    {
        predkoscAuto = speed;
    }
    internal void SetBrake(bool brake,float a=0,float b=0)
    {
        HamulecAuto = brake;
    }
    internal void GodPower(float a = 0) { }
    internal void AddPower(float a = 0) { }

    IEnumerator KompresorStart()
    {
        kompresorStan = true;
        kompresorDzwiekZrodlo.loop = false;
        kompresorDzwiekZrodlo.clip = kompresorStart;
        kompresorDzwiekZrodlo.Play();
        yield return new WaitForSeconds(kompresorStart.length);
        kompresorDzwiekZrodlo.clip = kompresorRepeat;
        kompresorDzwiekZrodlo.loop = true;
        kompresorDzwiekZrodlo.Play();
    }

    IEnumerator KompresorStop()
    {
        kompresorStan = false;
        kompresorDzwiekZrodlo.loop = false;
        kompresorDzwiekZrodlo.clip = kompresorStop;
        kompresorDzwiekZrodlo.Play();
        yield return new WaitForSeconds(kompresorStop.length);
        kompresorDzwiekZrodlo.Stop();
        
    }
    void HandleKompresor()
    {
        if (kompresorPrzelacznik && kompresorStan == false && cisnienie < 5)
        {
            StartCoroutine(KompresorStart());
        }
        if ((!kompresorPrzelacznik && kompresorStan == true) || (kompresorStan == true && cisnienie > 7))
        {
            StartCoroutine(KompresorStop());
        }
        if (kompresorStan == true)
        {
            cisnienie += -0.4f * Mathf.Log10(cisnienie*0.095f + 0.024f) * Time.deltaTime;
        }
        else
        {
            cisnienie -= cisnienie*0.002f * Time.deltaTime;
        }
    }

}