using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandLight : MonoBehaviour {

    public int band;
    public float minIntensity, maxIntensity;
    Light light;

	void Start () {
        light = GetComponent<Light>();
	}
	
	void Update () {
        light.intensity = (AudioAnalyzer.normalizedBufferedFreqValue[band] * (maxIntensity - minIntensity)) + minIntensity;
	}
}
