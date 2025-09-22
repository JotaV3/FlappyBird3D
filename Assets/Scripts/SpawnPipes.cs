using Unity.VisualScripting;
using UnityEngine;

public class SpawnPipes : MonoBehaviour
{
    [SerializeField] PipesListSO pipesListSO;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnTimerMax = 3.5f;
    [SerializeField] float heightOffset = 30f;

    private float spawnTimer;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer = spawnTimerMax;
            SpawnPipe();
        }
    }

    private void SpawnPipe()
    {
        int pipesClosed = 0;

        foreach (Transform spawnPointTransform in spawnPoints)
        {
            float lowestPoint = transform.position.y - heightOffset;
            float highestPoint = transform.position.y + heightOffset;

            // pipes can only be closed if Random.Range returns 0
            bool canPipesBeClosed = Random.Range(0, 2) == 0 && pipesClosed < spawnPoints.Length - 1;
            pipesClosed += canPipesBeClosed ? 1 : 0;

            Instantiate(GetPipesObjectTransform(canPipesBeClosed), new Vector3(spawnPointTransform.position.x, Random.Range(lowestPoint, highestPoint), spawnPointTransform.position.z), spawnPointTransform.rotation);
        }            
    }

    private Transform GetPipesObjectTransform(bool canPipesBeClosed)
    {
        PipesObjectSO pipesObjectSO = pipesListSO.pipesSOList[Random.Range(0, pipesListSO.pipesSOList.Count)];

        if(canPipesBeClosed)
        {
            return pipesObjectSO.pipesClosedPrefab.GetComponent<Transform>();
        }
        else
        {
            return pipesObjectSO.pipesOpenPrefab.GetComponent<Transform>();
        }
            
    }
}
