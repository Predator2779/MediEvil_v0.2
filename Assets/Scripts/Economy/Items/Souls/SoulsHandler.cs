using Character.ComponentContainer;
using Global;

namespace Economy.Items.Souls
{
    public class SoulsHandler
    {
        private PersonContainer _personContainer;
        private Item _droppedSoulsUnit;

        public SoulsHandler(PersonContainer personContainer, SoulItem dropSoulsUnitUnit)
        {
            _personContainer = personContainer;
            _droppedSoulsUnit = dropSoulsUnitUnit;
        }

        public void Subscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp += PickUpDroppedSouls;

            EventBus.OnPlayerDied.AddListener(DropSouls);
        }

        public void Unsubscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp -= PickUpDroppedSouls;
            
            EventBus.OnPlayerDied.RemoveListener(DropSouls);
        }

        private void PickUpDroppedSouls(Item item)
        {
            _personContainer.Inventory.AddItems(item);
            _personContainer.SoulWallet.Increase(item.Count);
            _droppedSoulsUnit.gameObject.SetActive(false);
        }
        
        private void DropSouls(PersonContainer container)
        {
            _droppedSoulsUnit.transform.position = _personContainer.transform.position;
            _droppedSoulsUnit.gameObject.SetActive(true);
            _droppedSoulsUnit.Count = _personContainer.Inventory.TryGetItems("My Soul", out ItemSet set) ? set.Count : 0;
            _personContainer.SoulWallet.Decrease(_droppedSoulsUnit.Count);
        }
    }
}