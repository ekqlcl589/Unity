using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public Mesh[] meshs;
    private MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            meshFilter.sharedMesh = meshs[0];

    }
}
