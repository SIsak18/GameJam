using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    [SerializeField]
    public Text _text;

    public int max = 5;
    //public int name_;
    public int state; // 0 - not exploded, 1 - exploding,  2 - exploded

    public int count = 0;
    [Range(2, 6)]
    public float radius = 4F;

    public float ObjectRange = 0.55F;


    int segments = 50;
    float xradius;
    float yradius;
    LineRenderer line;

    bool mouseDown = false;

    private void Start()
    {
        if(name == "bomb")
            _text.text = max.ToString();
    }

    private void Update()
    {
        if (name == "bomb")
        {
            int tmp = max - count;
            _text.text = tmp.ToString();
        }
            

        if (mouseDown)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (cursorPos.x > 8.6F) { cursorPos.x = 8.6F; }
            if (cursorPos.x < -8.6F) { cursorPos.x = -8.6F; }
            if (cursorPos.y > 4F) { cursorPos.y = 4F; }
            if (cursorPos.y < -2.25F) { cursorPos.y = -2.25F; }
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
        
    }

    private void OnMouseOver()
    {
        //Debug.Log("hover\n");
        if (name == "bomb") return;
        xradius = radius;
        yradius = radius;
        line = gameObject.GetComponent<LineRenderer>();

        Color c1 = Color.white;
        Color c2 = Color.yellow;
        //line.SetColors(c1, c2);
        line.startColor = c1;
        line.endColor = c2;

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

    private void OnMouseDown()
    {
        if(this.name == "bomb")
        {
            if(count < max)
            {
                count++;
                Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 new_position = new Vector2(cursorPos.x, cursorPos.y);
                string name_ = count.ToString();
                Object.Instantiate(GameObject.Find("bomb")).name = name_;
                GameObject.Find(name_).transform.position = new_position;
                GameObject.Find(name_).transform.localScale = new Vector3(0.5F, 0.5F,0);


            }
            
            return;
        }
        mouseDown = true;
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation = Quaternion.identity);
        Destroy(gameObject);
    }

}
