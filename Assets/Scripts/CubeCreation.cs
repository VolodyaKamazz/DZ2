using UnityEngine;
using System;

public class CubeCreation : MonoBehaviour
{
    [SerializeField][Min(0)] private int cubeCount;
    [SerializeField][Min(0)] private int radius;
    [SerializeField] private bool isEvenly = true;

    private GameObject prefab;

    public int Radius => radius;
    public int CubeCount => cubeCount;
    private int InitialCubeCount;
    public GameObject Prefab => prefab;
    public bool IsEvenly => isEvenly;

    public static event Action OnSomethingChanged;

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/cube");
        InitialCubeCount = CubeCount;
        CreateCubes();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        OnSomethingChanged += RecreateCubes;
    }

    private void OnDisable()
    {
        OnSomethingChanged -= RecreateCubes;
    }

    private void OnValidate()
    {
        OnSomethingChanged?.Invoke();
    }

    private void CreateCubes()
    {
        float radianAngleStep;
        if (IsEvenly == true)
        {
            radianAngleStep = 2 * Mathf.PI / InitialCubeCount;
        }
        else
        {
            if (Radius == 0)
            {
                radianAngleStep = 0;
            }
            else
            {
                radianAngleStep = 2 * (float)Math.Asin(Prefab.gameObject.transform.localScale.x / (2 * Radius));
            }
        }
        transform.position = Vector3.zero;
        for (var i = 0; i < InitialCubeCount; i++)
        {
            var x = Radius * Mathf.Cos(radianAngleStep * i);
            var z = Radius * Mathf.Sin(radianAngleStep * i);
            var localPosition = new Vector3(x, 0, z);
            var cube = Instantiate(Prefab, transform);
            cube.transform.SetParent(transform);
            cube.transform.localPosition = localPosition;
            cube.transform.name = $"Cube {i}";
            Debug.Log($"Куб {i} создан по координатам {localPosition}");
            
        }
    }

    private void RecreateCubes()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        CreateCubes();
    }
}