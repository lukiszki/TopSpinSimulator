using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Button : MonoBehaviour
{
    
    [SerializeField]
    Naped naped;
    [SerializeField]
    Automat automat;
    [SerializeField]

    ButtonType thisType;

    [SerializeField]
    AudioSource audio;

    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip clip2;


    [SerializeField]
    AudioClip noiseClip;

     [SerializeField]
    AudioClip rideClip;



    [SerializeField]
    AudioSource noise;

    [SerializeField]
    AudioSource rideNoise;
   public void Click()
    {
        switch (thisType)
        {
            case ButtonType.AUTOSWITHCH:
                if(!naped.AUTO)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y,-60);
                    naped.AUTO = (true);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
                   naped.AUTO = (false);
                }
            break;
            case ButtonType.START:
                if(naped.AUTO&&naped.pompaGlowna)
                    automat.StartAuto();
                break;
            case ButtonType.STOP:
                automat.Stop();
            break;
            case ButtonType.PARK:
                if(naped.AUTO&&naped.pompaGlowna)
                    automat.StartPark();
            break;
            case ButtonType.SOUND:
                audio.clip = clip;
                audio.Play();
            break;
            case ButtonType.SMALLPUMP:
                if(!naped.pompaLadujaca&&!naped.pompaGlowna&&!naped.wylacznikPompy&&naped.kluczyk)
                {
                    naped.PompLadujacaStart();
                    audio.PlayOneShot(clip);
                    noise.clip = noiseClip;
                    noise.Play();
                }
            break;
            case ButtonType.MAINPUMP:
                if(naped.pompaLadujaca && !naped.pompaGlowna && !naped.wylacznikPompy && naped.kluczyk)
                {
                    naped.PompGlownaStart();
                    audio.PlayOneShot(clip);
                   // noise.Stop();
                    noise.clip = noiseClip;
                    noise.Play();

                    rideNoise.clip = rideClip;
                    rideNoise.Play();
                }
            break;
            case ButtonType.PUMPSTOP:
                naped.PompaStop();
            break;
            case ButtonType.PUMPSWITCH:
                if(!naped.wylacznikPompy)
                {
                    naped.PompaStop();
                    transform.localPosition = new Vector3(0.00799140427f, 0.00643120287f, 0.01036003f);
                    naped.wylacznikPompy = true;
                    audio.PlayOneShot(clip);
                }
                else
                {
                    transform.localPosition = new Vector3(0.00799140427f, 0.00643120287f, 0.0103970272f);
                      naped.wylacznikPompy = false;
                    audio.PlayOneShot(clip2);

                }
                break;
            case ButtonType.TARZAN:
                if(naped.AUTO&&naped.pompaGlowna)
                    automat.StartTarzan();

            break;
            case ButtonType.KACZUCHY:
                if(naped.AUTO&&naped.pompaGlowna)
                    automat.StartKaczuchy();

            break;
            case ButtonType.MORTAL:
                if(naped.AUTO&&naped.pompaGlowna)
                    automat.StartMortal();

            break;
            case ButtonType.LOCKBRAKE:
                if(!naped.hamulecPrzelacznik)
                {
                    naped.hamulecPrzelacznik = true;
                   // naped.SetRot();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -14.5f,transform.localRotation.eulerAngles.z);
                }
               else
                {
                    naped.hamulecPrzelacznik = false;
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -53.6f,transform.localRotation.eulerAngles.z);
                }
                break;
            case ButtonType.KLUCZYK:
                if (!naped.kluczyk)
                {
                    naped.Zasilanie(true);
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 45);
                    naped.kluczyk = true;
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
                    naped.Zasilanie(false);
                    naped.kluczyk = false;
                }
                break;

            case ButtonType.PODESTR:
                if (!naped.podestPrawyPrzelacznik)
                {
                    naped.podestPrawyPrzelacznik = true;
                    naped.UstawPodesty();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -14.5f, transform.localRotation.eulerAngles.z);
                }
                else
                {
                    naped.podestPrawyPrzelacznik = false;
                    naped.UstawPodesty();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -53.6f, transform.localRotation.eulerAngles.z);
                }
                break;

            case ButtonType.PODESTL:
                if (!naped.podestLewyPrzelacznik)
                {
                    naped.podestLewyPrzelacznik = true;
                    naped.UstawPodesty();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -14.5f, transform.localRotation.eulerAngles.z);
                }
                else
                {
                    naped.podestLewyPrzelacznik = false;
                    naped.UstawPodesty();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -53.6f, transform.localRotation.eulerAngles.z);
                }
                break;
            case ButtonType.KOMPRESOR:
                if (!naped.kompresorPrzelacznik)
                {
                    //StartCoroutine(naped.KompresorStart());
                    naped.kompresorPrzelacznik = true;
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -14.5f, transform.localRotation.eulerAngles.z);
                }
                else
                {
                    //StartCoroutine(naped.KompresorStop());
                    naped.kompresorPrzelacznik = false;
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -53.6f, transform.localRotation.eulerAngles.z);
                }
                break;



        }
    }





       
  
}
enum ButtonType{AUTOSWITHCH,START, STOP, PARK, SOUND, SMALLPUMP, MAINPUMP,PUMPSTOP,PUMPSWITCH,TARZAN,KACZUCHY,MORTAL,LOCKBRAKE,PODESTL,PODESTR,ZAMKNIECIEA,ZAMKNIECIEB,KLUCZYK,KOMPRESOR}
