using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] LayerMask scoreZoneLayerMask;

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == scoreZoneLayerMask.value)
        {
            Pipes pipes = other.GetComponentInParent<Pipes>();
            ScoreManager.Instance.AddPoint(pipes.GetPoint());
        }
    }
}
