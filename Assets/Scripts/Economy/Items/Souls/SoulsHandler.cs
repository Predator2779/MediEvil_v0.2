using Character.ComponentContainer;
using Global;
using UnityEngine;

namespace Economy.Items.Souls
{
    public class SoulsHandler
    {
        private PersonContainer _personContainer;
        private Item _mySoulUnit;
        private Item _dropSoulsUnit;
        private string _soulName;

        private bool _soulIsDropped;
        
        public SoulsHandler(PersonContainer personContainer, SoulItem mySoulUnit, SoulItem dropSoulsUnitUnit)
        {
            _personContainer = personContainer;
            _mySoulUnit = mySoulUnit;
            _dropSoulsUnit = dropSoulsUnitUnit;
            _soulName = dropSoulsUnitUnit.Name;
        }

        public void Subscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp += PickUpSouls;

            EventBus.OnPlayerDied.AddListener(DropSouls);
        }

        public void Unsubscribe()
        {
            if (_personContainer.SoulWallet != null)
                _personContainer.ItemHandler.OnSoulPickedUp -= PickUpSouls;
            
            EventBus.OnPlayerDied.RemoveListener(DropSouls);
        }

        private void PickUpSouls(Item item)
        {
            _mySoulUnit.Count += item.Count;
            _personContainer.Inventory.AddItems(_mySoulUnit);
            _personContainer.SoulWallet.Increase(_mySoulUnit.Count);
            _dropSoulsUnit.gameObject.SetActive(false);
            _soulIsDropped = false;
        }
        
        private void DropSouls(PersonContainer container)
        {
            _dropSoulsUnit.transform.position = _personContainer.transform.position;
            _dropSoulsUnit.Count = _personContainer.Inventory.TryGetItems(_soulName, out Item item) ? item.Count : 0;
            _mySoulUnit.Count -= _dropSoulsUnit.Count;
            _personContainer.SoulWallet.Decrease(_dropSoulsUnit.Count);
            if (_soulIsDropped) _dropSoulsUnit.Count = 0;
            _dropSoulsUnit.gameObject.SetActive(true);
            _soulIsDropped = true;
        }
    }
}