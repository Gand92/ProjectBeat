using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateCubes : MonoBehaviour {

    public GameObject sampleCubePrefab;
    public float maxScale;
    private GameObject[] samplesCubes = new GameObject[512];


	void Start () {
		for (int i = 0; i < 512; ++i)
        {
            GameObject instaceSampleCube = Instantiate(sampleCubePrefab);
            instaceSampleCube.transform.position = this.transform.position;
            instaceSampleCube.transform.parent = this.transform;
            instaceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            instaceSampleCube.transform.position = Vector3.forward * 100;
            samplesCubes[i] = instaceSampleCube;
        }
	}
	
	void Update () {
		for (int i = 0; i < 512; ++i)
        {
            if (samplesCubes != null)
            {
                samplesCubes[i].transform.localScale = new Vector3(1, AudioAnalyzer.samples[i] * maxScale + 1, 1);
            }
        }
	}
}
