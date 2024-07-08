using Character.ComponentContainer;
using UnityEngine.Events;

namespace Global
{
    public static class EventBus
    {
        public static UnityEvent<PersonContainer> OnPlayerDied = new UnityEvent<PersonContainer>();
        public static UnityEvent<PersonContainer> OnUnitSpawned = new UnityEvent<PersonContainer>();
        public static UnityEvent<string> OnLocationSwitched = new UnityEvent<string>();
    }
}