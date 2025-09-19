using UnityEngine;

public class SpawnPipes : MonoBehaviour
{
    [SerializeField] Transform pipesTransform;
    [SerializeField] float spawnTimerMax = 3.5f;
    [SerializeField] float heightOffset = 15f;

    private float spawnTimer;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnPipe();
            spawnTimer = spawnTimerMax;
        }
    }

    private void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(pipesTransform, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), transform.rotation);
    }
}
