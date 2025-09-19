using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = false;

    private void Update()
    {
        if (!isFirstUpdate)
        {
            isFirstUpdate = true;

            Loader.LoaderCallback();
        }
    }
}
