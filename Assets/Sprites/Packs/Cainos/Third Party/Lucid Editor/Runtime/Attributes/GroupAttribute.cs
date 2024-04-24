using System;

namespace Sprites.Packs.Cainos.Third_Party.Lucid_Editor.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class GroupAttribute : PropertyGroupAttribute
    {
        public GroupAttribute(string groupName) : base(groupName) { }
    }
}