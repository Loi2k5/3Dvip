using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightpot : MonoBehaviour
{
    public Light _light;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Effectlight());
    }

    IEnumerator Effectlight()
    {
        // Nhấp nháy lần thứ nhất
        _light.enabled = true;
        yield return new WaitForSeconds(1f);
        _light.enabled = false;
        yield return new WaitForSeconds(1f);

        // Nhấp nháy lần thứ hai
        _light.enabled = true;
        yield return new WaitForSeconds(1f);
        _light.enabled = false;

        // Thời gian hồi 1 giây trước khi có thể nhấp nháy lại
        yield return new WaitForSeconds(1f);
    }
}
