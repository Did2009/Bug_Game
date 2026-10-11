using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        // Gets the mouse position on the screen
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Rotates arrow to point at the mouse using these claculations
        Vector3 direction =  mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
