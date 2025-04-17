using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D rg2D;
    public Player player;
    [SerializeField] private float Spearspeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
        //player = Player.Instance;
        rg2D.velocity = Vector2.left * Spearspeed;
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player"))
        {
            player.Die();
            print("Touch");
        }
    }
}
