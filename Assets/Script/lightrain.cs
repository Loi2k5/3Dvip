using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightrain : MonoBehaviour
{
    public Light _light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100)> 98)
        {
            StartCoroutine(Effectlight());
        }
    }

    IEnumerator Effectlight()
    {
        _light.enabled = true;
        yield return new WaitForSeconds(2f);
        _light.enabled = false;
    }
}
