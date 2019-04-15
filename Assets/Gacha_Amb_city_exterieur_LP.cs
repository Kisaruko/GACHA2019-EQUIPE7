using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha_Amb_city_exterieur_LP : MonoBehaviour
{

    [SerializeField] private string _event;
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent(_event, gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
