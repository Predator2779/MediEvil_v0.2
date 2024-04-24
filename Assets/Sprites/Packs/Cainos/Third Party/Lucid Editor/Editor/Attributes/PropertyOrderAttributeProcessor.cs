using Cainos.LucidEditor;
using Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(PropertyOrderAttribute))]
    public class PropertyOrderAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeInspectorGUI()
        {
            PropertyOrderAttribute propertyOrder = (PropertyOrderAttribute)attribute;
            property.order = propertyOrder.propertyOrder;
        }
    }
}