using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private FinishTrigger _trigger;

    public FinishTrigger GetTrigger()
    {
        return _trigger;
    }
}
