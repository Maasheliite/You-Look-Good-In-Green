using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmChildren : MonoBehaviour
{
    //This Script makes the children ob the gameObject this is put on, move around semi-randomly.
    //a Circle gizmo is added to visualize the range set. (this is not shown in the actual game)

    //USAGE:
    //Attach this script to an empty gameObject in the Scene. Add gameObjects as children within the Range
    //as shown by the yellow circle.
    //They should automatically "fly" around within the specified range around the parent.

    public float range = 5f;
    public float speed = 2f;
    public float wiggleSpeed = 0.5f;
    public bool showDirection = false;
    private List<Vector2> directions = new List<Vector2>();
    private Transform[] children;
    internal virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        if (showDirection)
        {

            for (int i = 0; i < children.Length; i++)
            {
                Vector3 pos = children[i].position;
                Gizmos.DrawLine(pos, pos + new Vector3(directions[i].x, directions[i].y, 0) * 0.5f);
            }
        }
    }

    void Start()
    {
        children = new Transform[transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
            directions.Add(Random.insideUnitCircle);
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (Vector2.Distance(children[i].position, gameObject.transform.position) > range)
            {
                directions[i] = gameObject.transform.position - children[i].position;
            }
            directions[i] = (directions[i] + Random.insideUnitCircle * wiggleSpeed).normalized;
            float step = speed * Time.deltaTime;
            children[i].position = Vector3.MoveTowards(children[i].position, children[i].position + new Vector3(directions[i].x, directions[i].y, 0), step);
            if(directions[i].x > 0.2)
            {
                children[i].localScale = new Vector3(-1,1,1);
            }else
            {
                children[i].localScale = new Vector3(1, 1, 1);
            }
        }
    }
}