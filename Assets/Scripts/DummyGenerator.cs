using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject dummy;

    [SerializeField]
    Transform generatePos;

    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        Instantiate(dummy, generatePos.position, Quaternion.identity);
    }
}
