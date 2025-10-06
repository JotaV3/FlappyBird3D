using System.Collections.Generic;
using UnityEngine;

public class PipesSpawner : MonoBehaviour
{
    [SerializeField] private PipesListSO pipesListSO;
    [SerializeField] private int pipesSpawnerIndex;
    [SerializeField] private float heightOffset = 30f;

    private void Start()
    {
        PipesSpawnerManager.Instance.OnSpawnPipes += SpawnPipesManager_OnSpawnPipes;
    }

    private void SpawnPipesManager_OnSpawnPipes(object sender, PipesSpawnerManager.OnSpawnPipesEventArgs e)
    {
        SpawnPipes(e.pipesSOListIndex, e.closedState[pipesSpawnerIndex]);
    }

    private void SpawnPipes(int pipesSOListIndex, bool closedState)
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        PipesSO pipesSO = pipesListSO.pipesSOList[pipesSOListIndex];
        Transform pipesTransform = Instantiate(pipesSO.pipesTransform, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), transform.rotation);

        if (closedState)
        {
            Pipes pipes = pipesTransform.GetComponent<Pipes>();
            pipes.ClosePipes();
        }
    }
}
