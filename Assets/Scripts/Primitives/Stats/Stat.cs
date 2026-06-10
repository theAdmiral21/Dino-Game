using System.Collections.Generic;

namespace Primitives.Stats
{
    [System.Serializable]
    public class Stat
    {

        public readonly float DefaultValue;
        private readonly List<float> modifiers = new();
        public float Value
        {
            get
            {
                float final = DefaultValue;
                foreach (float mod in modifiers)
                {
                    final += mod;
                }
                return final;
            }
        }

        public Stat(float baseValue)
        {
            DefaultValue = baseValue;
        }

        public void AddModifier(float mod) => modifiers.Add(mod);
        public void RemoveModifier(float mod) => modifiers.Remove(mod);
    }
}