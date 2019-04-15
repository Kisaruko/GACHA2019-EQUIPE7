using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public void OpenMenu()
    {
        AkSoundEngine.PostEvent("Gacha_UI_Menu_Openmenu_SFX", gameObject);
    }

    public void CloseMenu()
    {
        AkSoundEngine.PostEvent("Gacha_UI_Menu_Closingmenu_SFX", gameObject);
    }

    public void EnteringSection()
    {
        AkSoundEngine.PostEvent("Gacha_UI_Menu_Entering_Section_RC", gameObject);
    }

    public void ComingBackFromSection()
    {
        AkSoundEngine.PostEvent("Gacha_UI_Menu_Getting_Out_RC", gameObject);
    }







}
