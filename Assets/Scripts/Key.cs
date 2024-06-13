using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : RecycleObject
{
    public float rotateSpeed = 360.0f;

    Transform modelTransform;

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Awake()
    {
        modelTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        modelTransform.Rotate(Time.deltaTime * rotateSpeed * Vector3.up);
    }
}
