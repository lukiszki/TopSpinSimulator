using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class Network 
{
    static int inputSize =  2;
    static int hiddenLSize = 60;

    static int outputSize = 1;

    float off = 0.01f;


    float[] W1 = new float[inputSize * hiddenLSize];
    float[] W2 = new float[hiddenLSize * outputSize];


    float[] b1 = new float[hiddenLSize];

    float[] b2 = new float[outputSize];

    float l, r,f;

    float[] Z1 = new float[hiddenLSize];

    float[] A1 = new float[hiddenLSize];

    float[] Z2 = new float[outputSize];

    float[] A2 = new float[outputSize];



    public Network()
    {
        //InitValues();
        //LoadValues();

    }

    /*public void LoadValues(ref Random rnd)
    {
        Values data =  ValueContaier.value;
        float range = 0.015f;
        
        //Debug.Log(data.W1[0]);

        for (int i = 0; i < W1.Length; i++)
        {
            W1[i] = data.W1[i] + Randomize(range, ref rnd);
        }
        for (int i = 0; i < W2.Length; i++)
        {
            W2[i] = data.W2[i] + Randomize(range, ref rnd);
        }
        for (int i = 0; i < b1.Length; i++)
        {
            b1[i] = data.b1[i] + Randomize(range, ref rnd);
        }
        for (int i = 0; i < b2.Length; i++)
        {
            b2[i] = data.b2[i] + Randomize(range, ref rnd);
       }
    }*/

    /*public void LoadValues()
    {
        Values data =  ValueContaier.value;
        


        for (int i = 0; i < W1.Length; i++)
        {
            W1[i] = data.W1[i];
        }
        for (int i = 0; i < W2.Length; i++)
        {
            W2[i] = data.W2[i];
        }
        for (int i = 0; i < b1.Length; i++)
        {
            b1[i] = data.b1[i];
        }
        for (int i = 0; i < b2.Length; i++)
        {
            b2[i] = data.b2[i];
       }
    }*/

    public void InitValues(ref System.Random rnd)
    {
        for (int i = 0; i < W1.Length; i++)
        {
            W1[i] = (float)rnd.NextDouble() - 0.5f;
        }
        for (int i = 0; i < W2.Length; i++)
        {
            W2[i] = (float)rnd.NextDouble() -0.5f;
        }
        for (int i = 0; i < b1.Length; i++)
        {
            b1[i] = (float)rnd.NextDouble() - 0.5f;
        }
        for (int i = 0; i < b2.Length; i++)
        {
            b2[i] = (float)rnd.NextDouble() - 0.5f;
        }
    }

//float forDist, float forLeftDist,float forRightDist, float leftDist, float rightDist
    private void CalulateValues(float[] inp)
    {
        //Debug.Log(W1[0]);

        ClearArray(Z1);
        ClearArray(A1);
        ClearArray(Z2);
        //Debug.Log("Before: " + A2[0].ToString());

        ClearArray(A2);
        //Debug.Log("After: " + A2[0].ToString());

        for(int i = 0; i < hiddenLSize; i++)
        {
            for(int j = 0; j < inputSize; j++)
            {
                Z1[i] += inp[j] * W1[j + (inputSize * i)];
            }
            Z1[i] += b1[i];
            A1[i] = ReLU(Z1[i]);
        }

        /*float a1 = ReLU(forDist * W1[0] + forLeftDist * W1[1] + forRightDist * W1[2] +    leftDist * W1[3] +  rightDist * W1[4]  + b1[0]);
        float a2 = ReLU(forDist * W1[5] + forLeftDist * W1[6] + forRightDist * W1[7] +    leftDist * W1[8] +  rightDist * W1[9]  + b1[1]);
        float a3 = ReLU(forDist * W1[10] + forLeftDist * W1[11] + forRightDist * W1[12] + leftDist * W1[13] + rightDist * W1[14] + b1[2]);
        float a4 = ReLU(forDist * W1[15] + forLeftDist * W1[16] + forRightDist * W1[17] + leftDist * W1[18] + rightDist * W1[19] + b1[3]);
        float a5 = ReLU(forDist * W1[20] + forLeftDist * W1[21] + forRightDist * W1[22] + leftDist * W1[23] + rightDist * W1[24] + b1[4]);*/


        for(int i = 0; i < outputSize; i++)
        {
            for(int j = 0; j < hiddenLSize; j++)
            {


                Z2[i] += A1[j] * W2[j + (hiddenLSize * i)];
            }
            Z2[i] += b2[i];
            //A2[i] = Sigmoid(Z2[i]);
        }
        softmax(Z2);
        A2 = Z2;

        /*l = Sigmoid(a1 * W2[0] + a2 * W2[1] + a3 * W2[2] + a4 * W2[3] + a5 * W2[4] + b2[0]);
        r = Sigmoid(a1 * W2[5] + a2 * W2[6] + a3 * W2[7] + a4 * W2[8] + a5 * W2[9] + b2[1]);
        f = Sigmoid(a1 * W2[10] + a2 * W2[11] + a3 * W2[12] + a4 * W2[13] + a5 * W2[14] + b2[2]);*/
//        Debug.Log(A2[2]);

        
    }
    public float GetValue(float[] inp)
    {
        CalulateValues(inp);
        // Debug.Log("L: ");Debug.Log(l);
        //Debug.Log("R: "); Debug.Log(r);
        //Debug.log(A2[0]);

        return A2[0];
    }
    bool GetBool(int index, bool mem)
    {
        switch(index)
        {
            case 0: return mem;
            break;
            case 1: return false;
            break;
            case 2: return true;
            default: return mem;
        }

    }
    
    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }
    void ClearArray(float[] arr)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] =0;
        }
    }
    float ReLU(float x)
    {
        return Mathf.Max(0,x);
    }
   /* public void Save()
    {
        Values val = new Values(W1, W2, b1, b2);
        SaveSystem.SaveValues(val);
    }*/
    private float Randomize(float range, ref Random rnd)
    {
        float f = (1f / range)/2;
        return ((float)rnd.NextDouble()/f) - range;
    }
   /* public Values GetValues()
    {
        return new Values(W1, W2, b1, b2);
    }
    public void LoadVal(Values x)
    {
        W1 = x.W1;
        W2 = x.W2;
        b1 = x.b1;
        b2 = x.b2;
    }*/
    void softmax(float[] input) 
    {

        int size = input.Length;


        int i;
        float m, sum, constant;

        m = Mathf.NegativeInfinity;
        for (i = 0; i < size; ++i) {
            if (m < input[i]) {
                m = input[i];
            }
        }

        sum = 0.0f;
        for (i = 0; i < size; ++i) {
            sum += Mathf.Exp(input[i] - m);
        }

        constant = m + Mathf.Log(sum);
        for (i = 0; i < size; ++i) {
            input[i] = Mathf.Exp(input[i] - constant);
        }

    }
}
[System.Serializable]
public class Values
{
    public float[] W1;
    public float[] W2;
    public float[] b1;
    public float[] b2;
    public Values(float[] _W1, float[] _W2, float[] _b1, float[] _b2)
    {
        W1 = _W1;
        W2 = _W2;
        b1 = _b1;
        b2 = _b2;
    }
    public Values()
    {

    }

}
