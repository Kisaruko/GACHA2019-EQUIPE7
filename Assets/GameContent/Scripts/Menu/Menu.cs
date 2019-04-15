using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private int[] t;
    
    [SerializeField] public Animator anim;

    public string sNum= "000";
    public string nID;
    public int nMilisecondes;

     
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void init()
    {
        t = new int[3];

        t[0] = 0;
        t[1] = 0;
        t[2] = 0;
    }

    void SetValue(int _n1, int _n2)
    {
        t[_n1] = _n2;
    }

    public void playAnim(int _n)
    {
        //play l'anim numero _n
        if(_n == 0)
        {
            anim.SetBool("bRoll1", true);

        }
        else if (_n == 1)
        {
            //anim.SetBool("bRoll1", true);

        }
        else if (_n == 2)
        {
            //anim.SetBool("bRoll1", true);
        }


    }
}
