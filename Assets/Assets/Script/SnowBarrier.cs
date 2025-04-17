using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBarrier : MonoBehaviour
{
    [SerializeField] public Avalanche avalanche;
    [SerializeField] public SnowWaveParent snowWaveParent;
    // Start is called before the first frame update
    void Start()
    {
        if (snowWaveParent == null)
        {
            snowWaveParent = GetComponentInParent<SnowWaveParent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (avalanche == null)
        {
            avalanche = snowWaveParent.avalanche;
        }
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.transform.parent == this.transform.parent)
        {
            snowWaveParent.Switch = true;
            StartCoroutine(Teleport());
        }
    }
    /*public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.transform.parent == this.transform.parent)
        {
            snowWaveParent.Switch = true;
            this.transform.position += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
        }
    }*/
    public IEnumerator Teleport()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        this.transform.position += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.5f, 0.5f), 0);
    }
}
