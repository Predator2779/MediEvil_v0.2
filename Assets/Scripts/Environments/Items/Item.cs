using UnityEngine;

namespace Environments.Items
{
    public abstract class Item : MonoBehaviour
    {
        public abstract void PickUp();
        public abstract void Put();
    }
}