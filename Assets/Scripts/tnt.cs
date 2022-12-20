using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class tnt : MonoBehaviour
{
    public GameObject explosion;
    [Range(1, 5)]
    public float radius = 3F;
    
    int segments = 50;
    float xradius;
    float yradius;
    LineRenderer line;

    public int state;
    // Start is called before the first frame update
    /*void Start()
    {
        //Debug.Log("Test\n");
    }*/

    // Update is called once per frame
    /*void Update()
    {

    }*/

    private void OnMouseOver()
    {
        xradius = radius;
        yradius = radius;
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        //line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    private void OnMouseExit()
    {
        xradius = 0F;
        yradius = 0F;

        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        //line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        //float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation = Quaternion.identity);
        Destroy(gameObject);
    }
}
