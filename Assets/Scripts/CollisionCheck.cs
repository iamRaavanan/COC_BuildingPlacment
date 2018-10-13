using UnityEngine;
using System.Collections;

namespace Raavanan
{
    public class CollisionCheck : MonoBehaviour
    {

        void OnCollisionEnter(Collision col)
        {
            MainController.instance.isOverlap = true;
        }

        void OnCollisionExit(Collision collisionInfo)
        {
            MainController.instance.isOverlap = false;
        }
    }
}
