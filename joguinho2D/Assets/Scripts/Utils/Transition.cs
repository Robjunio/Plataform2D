using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private bool startTransition;
    void FixedUpdate()
    {
        if (startTransition)
        {
            transform.localScale += Vector3.one;
        }
    }

    public void SetTransition()
    {
        startTransition = true;
    }
}
