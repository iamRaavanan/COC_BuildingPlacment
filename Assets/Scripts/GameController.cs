using UnityEngine;
using System.Collections;

namespace Raavanan
{
    public class GameController : MonoBehaviour
    {
        void Update()
        {
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVelocity = moveInput.normalized * 5;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            float rayDistance;
            if (plane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);

            }
        }
    }
}
