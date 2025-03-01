using UnityEngine;

public class PipeMiddleTrigger : MonoBehaviour
{
    public Logic logic;
    public BirdScript birdScript;
    
    void Start()
    {
        // Try to find the Logic component if not assigned
        if (logic == null)
        {
            GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
            if (logicObject != null)
            {
                logic = logicObject.GetComponent<Logic>();
            }

            GameObject birdObject = GameObject.FindGameObjectWithTag("Player");
            if (birdObject != null)
            {
                birdScript = birdObject.GetComponent<BirdScript>();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 3 && birdScript != null && birdScript.birdIsAlive)
        {
            if (logic != null)
            {
                logic.addScore(1);
            }
        }
    }
}