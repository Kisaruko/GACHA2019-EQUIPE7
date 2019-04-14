using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllManager : MonoBehaviour
{
    public static AllManager Instance;
    public bool[] Matriochka = new bool[7];

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (Instance) DestroyImmediate(Instance);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
