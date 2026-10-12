using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new Vector3(0, 2, -10);

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
