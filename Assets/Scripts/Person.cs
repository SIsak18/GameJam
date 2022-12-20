using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject dead;
    public void Kill()
    {
        Instantiate(dead, transform.position, transform.rotation = Quaternion.identity);
        Destroy(gameObject);
    }

    public bool InRange(Vector2 position, float range)
    {
        Vector2 myposition = transform.position;
        float x;
        float y;

            y = GetComponent<BoxCollider2D>().size.y;
            x = GetComponent<BoxCollider2D>().size.x;

        //float scale = transform.localScale.x;

        //x *= scale;
        //y *= scale;

        float distanceX = Mathf.Abs(position.x - myposition.x);
        float distanceY = Mathf.Abs(position.y - myposition.y);


        if (distanceX > (x / 2 + range)) { return false; }
        if (distanceY > (y / 2 + range)) { return false; }

        if (distanceX <= (x / 2)) { return true; }
        if (distanceX <= (y / 2)) { return true; }

        float corner_distance = Mathf.Pow(distanceX - (x / 2), 2) + Mathf.Pow(distanceY - (y / 2), 2);

        return (corner_distance <= Mathf.Pow(range, 2));
    }
}
