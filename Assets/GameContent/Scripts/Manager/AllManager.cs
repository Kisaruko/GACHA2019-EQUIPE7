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

        if (Instance) DestroyImmediate(Instance);
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
