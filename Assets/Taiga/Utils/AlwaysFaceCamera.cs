using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    public void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
