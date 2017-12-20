using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour {

    private AudioSource audioSource;
    private const int SAMPLE_SIZE = 512;
    public static float[] samples = new float[SAMPLE_SIZE];
    private static float[] frequencyBands = new float[8];
    private static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    //Transform the sample values in a range between 0, 1
    public float[] frequencyBandHighest = new float[8];
    public float startHighest;
    public static float[] normalizedBufferedFreqValue = new float[8];

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeHighestFrequency(startHighest);
    }


    private void Update()
    {
        GetSpectrumAudioSource();
        SetFrequencyBands();
        SetBandBuffer();
        CreateNormalValues();
    }

    private void InitializeHighestFrequency(float startHighest)
    {
        for (int i = 0; i < 8; ++i)
        {
            frequencyBandHighest[i] = startHighest;
        }
    }
    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
    }
    private void SetFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; ++i)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i);
            for (int j = 0; j < sampleCount; ++j)
            {
                average += samples[count] * (count + 1); //Multiplying for count + 1 it's needed because we need to give more weight on higher frequency values
                count++;
            }

            average /= count;
            frequencyBands[i] = average * 10;
        }

    }
    private void SetBandBuffer()

    {
        for (int i = 0; i < 8; ++i)
        {
            if (frequencyBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = frequencyBands[i];
                bufferDecrease[i] = 0.005f;
            }

            if (frequencyBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }
    private void CreateNormalValues()
    {
        for (int i = 0; i < 8; ++i)
        {
            if (frequencyBands[i] > frequencyBandHighest[i])
            {
                frequencyBandHighest[i] = frequencyBands[i]; 
                //It is also possibile to multiply frequency band for 1.2f or similar to avoid stacking on the max value with repeating beats 
            }
            normalizedBufferedFreqValue[i] = (bandBuffer[i] / frequencyBandHighest[i]);
            if (normalizedBufferedFreqValue[i] < 0)
            {
                normalizedBufferedFreqValue[i] = 0f;
            }
        }
    }
}
