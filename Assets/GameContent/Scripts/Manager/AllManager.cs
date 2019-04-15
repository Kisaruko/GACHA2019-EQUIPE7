using System;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    public static AllManager Instance;
    public bool[] Matriochka = new bool[7];

    public int colorBlindEnumIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if (Instance) DestroyImmediate(Instance);
        Instance = this;
    }
}
