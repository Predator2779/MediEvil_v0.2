using Cainos.LucidEditor;
using Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes;

namespace Cainos.LucidEditor
{
    [CustomAttributeProcessor(typeof(IndentAttribute))]
    public class IndentAttributeProcessor : PropertyProcessor
    {
        public override void OnBeforeDrawProperty()
        {
            IndentAttribute indent = (IndentAttribute)attribute;
            property.indent = indent.indent;
        }
    }
}