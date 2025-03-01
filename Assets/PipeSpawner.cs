using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe; 
    public float spawnrate = 4f; 
    private float timer = 0f; 
    public float heightOffset = 9f; 
    public float spawnDistance = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe(); 
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        
        if (timer >= spawnrate)
        {
            spawnPipe(); 
            timer = 0f; 
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // Calculate the spawn position
        float spawnX = transform.position.x + spawnDistance; 
        float spawnY = Random.Range(lowestPoint, highestPoint); 

        // Spawn a pipe at the calculated position
        Instantiate(pipe, new Vector3(spawnX, spawnY, 0), transform.rotation);
    }
}