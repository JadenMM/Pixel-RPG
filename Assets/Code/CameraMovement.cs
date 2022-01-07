using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform PlayerFollow;


    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(PlayerFollow.position.x, PlayerFollow.position.y, gameObject.transform.position.z);
    }
}
