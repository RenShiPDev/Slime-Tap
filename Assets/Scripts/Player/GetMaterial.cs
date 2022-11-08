using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public Material GetObjectMaterial()
    {
        return _meshRenderer.material;
    }
}
