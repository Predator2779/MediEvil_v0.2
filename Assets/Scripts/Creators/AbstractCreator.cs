using UnityEngine;

namespace Creators
{
    public abstract class AbstractCreator : MonoBehaviour
    {
        [SerializeField] protected GameObject _unitPrefabBase;
        [SerializeField] [Min(1)] protected int _countObjects = 1;
        [SerializeField] protected string _path;

        protected GameObject _unit;

        private void Awake()
        {
            for (int i = 0; i < _countObjects; i++)
                StartCreator();
        }

        protected virtual void StartCreator()
        {
            EnableCreator();
            InstantiateUnitComponents();
            Initialize();
            DisableCreator();
        }

        protected abstract void InstantiateUnitComponents();
        protected abstract void Initialize();

        protected void CreateUnit(GameObject unit)
        {
            _unit = Instantiate(unit, transform.position, Quaternion.identity);
            _unit.transform.SetParent(FindOrCreatePath(_path));
        }

        protected void EnableCreator() => gameObject.SetActive(true);
        protected void DisableCreator() => gameObject.SetActive(false);

        protected Transform FindOrCreatePath(string name)
        {
            var path = GameObject.Find(name);

            if (path == null)
            {
                path = new GameObject();
                path.transform.position = Vector3.zero;
                path.name = name;
            }

            return path.transform;
        }
    }
}