using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandCube : MonoBehaviour {

    public int band;
    public float startScale, maxScale;
    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }
    void Update() {
        if (AudioAnalyzer.normalizedBufferedFreqValue[band] > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioAnalyzer.normalizedBufferedFreqValue[band] * maxScale) + startScale, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, (((AudioAnalyzer.normalizedBufferedFreqValue[band] * maxScale) + startScale) - 1) * 0.5f, transform.position.z);
            Color color = new Color(AudioAnalyzer.normalizedBufferedFreqValue[band], AudioAnalyzer.normalizedBufferedFreqValue[band], AudioAnalyzer.normalizedBufferedFreqValue[band]) * 0.5f;
            material.SetColor("_EmissionColor", color);
        }
    }
}
