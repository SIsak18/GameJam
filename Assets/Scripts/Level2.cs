using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{

    public GameObject retry;
    public GameObject next;

    tnt TNT;
    int currentBombs;
    // Start is called before the first frame update
    void Start()
    {
        TNT = new();
        TNT.state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.Find("bomb").TryGetComponent(out Bomb bomb))
        {
            currentBombs = bomb.count;
        }
        if(TNT.state == 2)
        {

            TNT.state = 3;
            if (GameObject.Find("dead(Clone)"))
            {
                Retry();
                return;
            }
            for (int i = 1; i <= 6; i++)
            {
                if (GameObject.Find("plank" + i.ToString()))
                {
                    Debug.Log("plank" + i.ToString() + " need to destoy all objeects");
                    Retry();
                    return;
                }
            }
            Next();
        }
        if(TNT.state == 1)
        {
            TNT.state = 2;
        }

    }

    public void StartGame()
    {

        TNT.state = 1;
        Explode("tnt", TNT.radius);
        GameObject.Find("tnt").TryGetComponent(out tnt currenttnt);
        currenttnt.Explode();

    }

    public void Explode(string name, float radius)
    {

        // all current bombs
        for (int i = 1; i <= currentBombs; i++)
        {
            // if in radius
            if (CheckRadius(name, radius, i.ToString()))
            {
                if (GameObject.Find(i.ToString()).TryGetComponent(out Bomb currentbomb))
                {
                    if (currentbomb.state == 0)
                    {
                        currentbomb.state = 1;
                        Explode(currentbomb.name, currentbomb.radius);
                        for (int j = 1; j <= 6; j++)
                        {
                            GameObject.Find("plank" + j.ToString()).TryGetComponent(out Plank currentplank);
                            if (currentplank.InRange(currentbomb.transform.position, currentbomb.radius * currentbomb.GetComponent<CircleCollider2D>().radius))
                            {
                                currentplank.Destroy();
                            }
                        }
                        GameObject.Find("person").TryGetComponent(out Person currentperson);
                        if (currentperson.InRange(currentbomb.transform.position, currentbomb.radius * currentbomb.GetComponent<CircleCollider2D>().radius))
                        {
                            currentperson.Kill();
                            return;
                        }
                        currentbomb.Explode();
                    }

                }
            }
        }

    }

    public bool CheckRadius(string name1, float radius1, string name2)
    {
        Vector2 position1 = GameObject.Find(name1).transform.position;
        Vector3 scale1 = GameObject.Find(name1).transform.localScale;
        Vector2 position2 = GameObject.Find(name2).transform.position;
        Vector3 scale2 = GameObject.Find(name2).transform.localScale;
        float objectRange2 = GameObject.Find(name2).GetComponent<CircleCollider2D>().radius;

        float distance = Vector2.Distance(position1, position2);

        if (distance < (radius1 * scale1.x + objectRange2 * scale2.x))
            return true;
        else
            return false;
    }

    public void Retry()
    {
        Vector2 middle = new(0, 0);
        Instantiate(retry, middle, transform.rotation = Quaternion.identity);
    }

    public void Next()
    {
        Vector2 middle = new(0, 0);
        Instantiate(next, middle, transform.rotation = Quaternion.identity);
    }
}
