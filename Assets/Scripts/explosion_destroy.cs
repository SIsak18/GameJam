using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_destroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Destroy(gameObject, 0.75F);
    }
}
