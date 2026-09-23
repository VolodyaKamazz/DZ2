using UnityEngine;

public class CubeCreation : MonoBehaviour
{
    [SerializeField] private int cubeCount;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int radius;
    [SerializeField] private bool isEvenly = true;
    [SerializeField] private int unevenlyAngleStep;

    public int Radius => radius;
    public int CubeCount => cubeCount;
    public GameObject Prefab => prefab;
    public bool IsEvenly => isEvenly;
    public int UnevenlyAngleStep => unevenlyAngleStep;

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

    private void CreateCubes()
    {
        float radianAngleStep;
        if (IsEvenly == true)
        {
            radianAngleStep = 2 * Mathf.PI / CubeCount;
        }
        else
        {
            radianAngleStep = UnevenlyAngleStep * Mathf.PI / 180;
        }
        for (var i = 0; i < CubeCount; i++)
        {
            var x = transform.position.x + radius * Mathf.Cos(radianAngleStep * i);
            var z = transform.position.z + radius * Mathf.Sin(radianAngleStep * i);
            var position = new Vector3(x, transform.position.y, z);
            var cube = Instantiate(Prefab, transform);
            cube.transform.localPosition = position;
            Debug.Log($"Куб {i} создан по координатам {position}");
            cube.transform.SetParent(transform);
        }
    }
}
