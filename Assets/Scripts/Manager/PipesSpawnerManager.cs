using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PipesSpawnerManager : MonoBehaviour
{
    public static PipesSpawnerManager Instance { get; private set; }

    [SerializeField] private PipesListSO pipesListSO;
    [SerializeField] private List<Transform> spawnPointsList;

    public event EventHandler<OnSpawnPipesEventArgs> OnSpawnPipes;
    public class OnSpawnPipesEventArgs : EventArgs
    {
        public int pipesSOListIndex;
        public bool[] closedState;
    }

    private float spawnTimer;
    private float spawnTimerMax = 3.5f;
    private bool[] closedState;

    private void Awake()
    {
        Instance = this;

        closedState = new bool[spawnPointsList.Count];
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            // get a random pipesSO from the list
            int pipesSOListIndex = Random.Range(0, pipesListSO.pipesSOList.Count);
            PipesSO pipesSO = pipesListSO.pipesSOList[pipesSOListIndex];

            SetClosedStates(pipesSO);

            OnSpawnPipes?.Invoke(this, new OnSpawnPipesEventArgs
            {
                pipesSOListIndex = pipesSOListIndex,
                closedState = closedState
            });

            spawnTimer = spawnTimerMax;   
        }
    }

    private void SetClosedStates(PipesSO pipesSO)
    {
        // set closedStates for every spawnPointsList
        for(int i = 0; i < spawnPointsList.Count; i++)
        {
            // serialize closeChance
            closedState[i] = Random.value < pipesSO.closeChance / 100f;
        }

        if (AreAllPipesClosed(closedState))
        {
            int randomIndex = Random.Range(0, spawnPointsList.Count);
            closedState[randomIndex] = false;
        }
    }

    private bool AreAllPipesClosed(bool[] closedStates)
    {
        foreach(bool state in closedStates)
        {
            // at least one pipe is open
            if (!state) return false;
        }

        return true;
    }
}
