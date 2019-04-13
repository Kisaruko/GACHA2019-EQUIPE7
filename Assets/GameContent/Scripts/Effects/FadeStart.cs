using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour
{
    public GameObject matriochka;
    public int time = 3;
    public GameObject date;
    public int time2 = 3;
    public Image fondu;
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("StartFade");
    }
    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(2);
         matriochka.gameObject.SetActive(true);
    yield return new WaitForSeconds(time);
        matriochka.gameObject.SetActive(false);
        fondu.gameObject.SetActive(true);
        yield return new WaitForSeconds(time2);
        fondu.gameObject.SetActive(false);
        float _delay = 0;
        while (_delay < 1)
        {
            _delay += Time.deltaTime;
            var tempColor1 = fondu.color;
            tempColor1.a = 1 - _delay;
            fondu.color = tempColor1;
            yield return null;
        }
        var tempColor2 = fondu.color;
        tempColor2.a = 0;
        fondu.color = tempColor2;
        yield return null;
    }
}
