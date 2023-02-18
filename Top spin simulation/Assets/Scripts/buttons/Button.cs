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
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 135,transform.localRotation.eulerAngles.z);
                    naped.ToogleAuto(true);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 90,transform.localRotation.eulerAngles.z);
                    naped.ToogleAuto(false);
                }
            break;
            case ButtonType.START:
                if(naped.AUTO&&naped.mainPump)
                    automat.StartAuto();
                break;
            case ButtonType.STOP:
                automat.Stop();
            break;
            case ButtonType.PARK:
                if(naped.AUTO&&naped.mainPump)
                    automat.StartPark();
            break;
            case ButtonType.SOUND:
                audio.clip = clip;
                audio.Play();
            break;
            case ButtonType.SMALLPUMP:
                if(!naped.smallPump&&!naped.mainPump&&!naped.pumpSwitch)
                {
                    naped.smallPump = true;
                    audio.PlayOneShot(clip);
                    noise.clip = noiseClip;
                    noise.Play();
                }
            break;
            case ButtonType.MAINPUMP:
                if(naped.smallPump&&!naped.mainPump&&!naped.pumpSwitch)
                {
                    naped.mainPump = true;
                    audio.PlayOneShot(clip);
                    noise.Stop();
                    noise.clip = noiseClip;
                    noise.Play();

                    rideNoise.clip = rideClip;
                    rideNoise.Play();
                }
            break;
            case ButtonType.PUMPSTOP:
                naped.StopPump();
                StartCoroutine(FadeOut(noise, 0.2f));
            break;
            case ButtonType.PUMPSWITCH:
                if(!naped.pumpSwitch)
                {
                    naped.StopPump();   
                    transform.localPosition = new Vector3(0.007999f,0.0064312f,0.010375f);
                    naped.pumpSwitch = true;
                    StartCoroutine(FadeOut(noise, 0.2f));
                }
                else
                {
                    transform.localPosition = new Vector3(0.007991404f,0.006431203f,0.01039703f);
                    naped.pumpSwitch = false;
                }
            break;
            case ButtonType.TARZAN:
                if(naped.AUTO&&naped.mainPump)
                    automat.StartTarzan();

            break;
            case ButtonType.KACZUCHY:
                if(naped.AUTO&&naped.mainPump)
                    automat.StartKaczuchy();

            break;
            case ButtonType.MORTAL:
                if(naped.AUTO&&naped.mainPump)
                    automat.StartMortal();

            break;
            case ButtonType.LOCKBRAKE:
                if(!naped.lockBrake)
                {
                    naped.lockBrake = true;
                    naped.SetRot();
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -14.5f,transform.localRotation.eulerAngles.z);
                }
                else
                {
                    naped.lockBrake = false;
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, -53.6f,transform.localRotation.eulerAngles.z);
                }
                break;
                


        }
    }





        IEnumerator FadeOut (AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;
    
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
                yield return null;
            }
 
            audioSource.Stop ();
            audioSource.volume = startVolume;
        }
  
}
enum ButtonType{AUTOSWITHCH,START, STOP, PARK, SOUND, SMALLPUMP, MAINPUMP,PUMPSTOP,PUMPSWITCH,TARZAN,KACZUCHY,MORTAL,LOCKBRAKE}
