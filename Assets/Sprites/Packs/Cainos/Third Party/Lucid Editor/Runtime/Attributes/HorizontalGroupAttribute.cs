using System;

namespace Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class HorizontalGroupAttribute : PropertyGroupAttribute
    {
        public HorizontalGroupAttribute(string groupName) : base(groupName) { }
    }
}