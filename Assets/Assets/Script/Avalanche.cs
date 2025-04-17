using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public SnowWaveParent snowWaveParent;
    [SerializeField] public Vector2 DesiredMoving = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (snowWaveParent == null)
        {
            snowWaveParent = GetComponentInParent<SnowWaveParent>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {   
    }

    /*public void Direction(int a)
    {   
        if (a == 1)
        {
            DesiredMoving = new Vector2(1, 0) * Time.deltaTime * moveSpeed;
            Move(DesiredMoving);
        }
        if(a == -1)
        {
            DesiredMoving = new Vector2(-1, 0) * Time.deltaTime * moveSpeed;
            Move(DesiredMoving);
        }
        
    }*/

    public void Move(Vector2 Velocity)
    {   
        transform.position += new Vector3(Velocity.x, Velocity.y);
    }
}
