using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkMover : MonoBehaviour
{
    [SerializeField] private GameObject _markObject;
    [SerializeField] private ChangeMenuModel _changeMenuModel;

    public void ChangeMark(GameObject obj, Vector3 position)
    {
        _markObject.transform.SetParent(obj.transform);
        _markObject.transform.localPosition = position;
        _markObject.SetActive(true);
    }
}
