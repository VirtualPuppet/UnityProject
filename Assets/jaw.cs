using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jaw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Jaw1 = new Vector3(-0.091f, 0.035f, 0f);
        transform.localPosition = Jaw1;

    }
}
