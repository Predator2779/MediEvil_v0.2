using Cainos.LucidEditor;
using Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(EnableIfAttribute))]
    public class EnableIfAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            EnableIfAttribute enableIf = (EnableIfAttribute)attribute;
            property.isEditable = ReflectionUtil.GetValueBool(property.parentObject, enableIf.condition);
        }
    }
}