using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform PlayerFollow;


    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(PlayerFollow.position.x, PlayerFollow.position.y, gameObject.transform.position.z);
    }
}
