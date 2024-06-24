using Character.ComponentContainer;
using UnityEngine.Events;

namespace Global
{
    public static class EventBus
    {
        // Death
        public static UnityEvent<PersonContainer> OnPlayerDied = new UnityEvent<PersonContainer>();
        public static UnityEvent OnPlayerRespawned = new UnityEvent();
        public static UnityEvent<PersonContainer> OnUnitDied = new UnityEvent<PersonContainer>();
        public static UnityEvent OnSoulPicked = new UnityEvent();
        public static UnityEvent<string> OnLocationSwitched = new UnityEvent<string>();
    }
}