using UnityEngine;
using System;

public class CubeCreation : MonoBehaviour
{
    [SerializeField] private int cubeCount;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int radius;
    [SerializeField] private bool isEvenly = true;

    private int previousRadius;
    private bool previousIsEvenly;

    public int Radius => radius;
    public int CubeCount => cubeCount;
    public GameObject Prefab => prefab;
    public bool IsEvenly => isEvenly;

    public static event Action OnRadiusChanged;
    public static event Action OnIsEvenlyChanged;

    private void Awake()
    {
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
        OnRadiusChanged += RecreateCubes;
        OnIsEvenlyChanged += RecreateCubes;

    }

    private void OnDisable()
    {
        OnRadiusChanged -= RecreateCubes;
        OnIsEvenlyChanged -= RecreateCubes;
    }

    private void OnValidate()
    {
        if (Radius != previousRadius)
        {
            OnRadiusChanged?.Invoke();
            previousRadius = Radius;
        }
        if (IsEvenly != previousIsEvenly)
        {
            OnIsEvenlyChanged?.Invoke();
            previousIsEvenly = IsEvenly;
        }
    }

    private void CreateCubes()
    {
        float radianAngleStep;
        if (IsEvenly == true)
        {
            radianAngleStep = 2 * Mathf.PI / CubeCount;
        }
        else
        {
            radianAngleStep = 2 * (float)Math.Asin(Prefab.gameObject.transform.localScale.x / (2 * Radius));
        }
        for (var i = 0; i < CubeCount; i++)
        {
            var x = transform.position.x + Radius * Mathf.Cos(radianAngleStep * i);
            var z = transform.position.z + Radius * Mathf.Sin(radianAngleStep * i);
            var position = new Vector3(x, transform.position.y, z);
            var cube = Instantiate(Prefab, transform);
            cube.transform.localPosition = position;
            cube.transform.name = $"Cube {i}";
            Debug.Log($"Куб {i} создан по координатам {position}");
            cube.transform.SetParent(transform);
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