using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    Light light;
    public float max = 1f;
    public float min = 0.3f;
    float value;
    public float timerMin = 0.05f;
    public float timerMax = 0.1f;
    public float speed = 10f;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        StartCoroutine(FlickerLight());
	}

    private void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, value, Time.deltaTime * speed);
    }

    IEnumerator FlickerLight()
    {
        value = Random.Range(min, max);
        yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
        StartCoroutine(FlickerLight());
    }
}
