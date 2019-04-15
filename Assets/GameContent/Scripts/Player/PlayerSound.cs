using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

 public void Footsteps()
    {
        AkSoundEngine.PostEvent("Gacha_Char_FTPS_Switchcontainer", gameObject);


    }

}
