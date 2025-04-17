using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWaveParent : MonoBehaviour
{
    [SerializeField] public Avalanche avalanche;
    private SnowBarrier[] snowBarrier;
    [SerializeField] public bool Switch = false;
    private int ChangeDirection = -1;
    private SnowBarrier barrier;
    private int Counter = 0;
    private float scaleChange;
    private float minDistance = 0f; // Minimum distance to trigger scaling
    private float maxDistance = 10f; // Maximum distance to trigger scaling
    private float minScale = 0.25f; // Minimum scale
    private float maxScale = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if (avalanche == null)
        {
            avalanche = GetComponentInChildren<Avalanche>();
        }
        if (snowBarrier == null)
        {
            snowBarrier = GetComponentsInChildren<SnowBarrier>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        barrier = snowBarrier[Counter];
        if (Switch)
        {
            if (Counter == 0)
            {
                Counter += 1;
            }
            else if (Counter == 1)
            {
                Counter -= 1;
            }
            Switch = false;
        }
        float distance = Vector3.Distance(avalanche.transform.localPosition, barrier.transform.localPosition);
        distance = Mathf.Clamp01(distance);
        float normalizedDistance = Mathf.Lerp(maxDistance, minDistance, distance) / maxDistance;

        print($"{distance} - {normalizedDistance}");

        //avalanche.DesiredMoving = new Vector2(barrier.transform.position.x, barrier.transform.position.y) * Time.deltaTime * avalanche.moveSpeed;
        
        //avalanche.Move(avalanche.DesiredMoving);
        avalanche.transform.position = Vector3.MoveTowards(avalanche.transform.position, barrier.transform.position, Time.deltaTime * (avalanche.moveSpeed*Vector3.Distance(avalanche.transform.localPosition, barrier.transform.localPosition)*0.5f));
        Vector3 temp = avalanche.transform.localScale;
        
        /*temp.x *= scaleChange;
        temp.y *= scaleChange;
        avalanche.transform.localScale = temp;*/
        
        float scale = Mathf.Lerp(minScale, maxScale, normalizedDistance);

        print($"{scale} {normalizedDistance}");

        avalanche.transform.localScale = new Vector3(scale, scale, scale);
        Move(new Vector2(1, 0) * Time.deltaTime * 3f);
    }
    public void Move(Vector2 Velocity)
    {
        transform.position += new Vector3(Velocity.x, Velocity.y);
    }
}
