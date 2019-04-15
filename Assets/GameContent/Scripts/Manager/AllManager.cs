using UnityEngine;

public class AllManager : MonoBehaviour
{
    public static AllManager Instance;
    public bool[] Matriochka = new bool[5];

    public int colorBlindEnumIndex;

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
