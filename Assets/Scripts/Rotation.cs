using UnityEngine;
using System;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3Int angularVelocity;

    private Rigidbody Rigidbody;

    public Vector3Int AngularVelocity => angularVelocity;

    public static event Action OnSomethingChanged;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        ChangeVelocity();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ChangeVelocity();
    }

    private void OnEnable()
    {
        OnSomethingChanged += ChangeVelocity;
    }

    private void OnDisable()
    {
        OnSomethingChanged -= ChangeVelocity;
    }

    private void OnValidate()
    {
        OnSomethingChanged?.Invoke();
    }

    private void ChangeVelocity()
    {
        Rigidbody.angularVelocity = AngularVelocity;
    }
}