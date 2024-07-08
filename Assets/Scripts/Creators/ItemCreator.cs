namespace Creators
{
    public class ItemCreator : AbstractCreator
    {
        protected override void InstantiateUnitComponents() => CreateUnit(_unitPrefabBase);
        
        protected override void Initialize()
        {
        }
    }
}