using Cainos.LucidEditor;
using Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(LabelTextAttribute))]
    public class LabelTextAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            property.displayName = ((LabelTextAttribute)attribute).label;
        }
    }
}