using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureRemover : MonoBehaviour
{
    [SerializeField] private float _scalingSpeed;
    [SerializeField] private float _flySpeed;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = transform.localPosition* _flySpeed;
    }

    private void Update()
    {
        if(transform.localScale.y > 0)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * _scalingSpeed;
        } 
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
