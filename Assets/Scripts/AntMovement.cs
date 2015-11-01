using UnityEngine;
using System.Collections;

public class AntMovement : MonoBehaviour
{

    public float moveSpeed = 0.5f;
    public bool moving = false;
    public LayerMask blockingLayer;
    public Direction direction = Direction.WEST;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            StartCoroutine(Move(transform.position + new Vector3(0, 1, 0)));
            moving = false;
        }
    }

    public void Turn()
    {
        transform.Rotate(0, 0, 90);
    }

    public IEnumerator Move(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, moveSpeed);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        moving = true;

    }

    public void CalculateDirection(Vector2 start, Vector2 end)
    {
        bool calculating = true;
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);

        while (calculating)
        {
            if (hit.transform == null)
            {
                StartCoroutine(Move(end));
            } else {
				switch (direction)
				{
					case Direction.NORTH:
					break;
					case Direction.EAST:
					break;
					case Direction.SOUTH:
					break;
					case Direction.WEST:
					break;
				}
			}
        }
    }
}

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}
