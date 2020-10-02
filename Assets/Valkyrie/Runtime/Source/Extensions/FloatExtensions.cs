using UnityEngine;

namespace Valkyrie.Extensions 
{
    public static class FloatExtensions 
    {
        /// <summary>
        /// Round 'a' to nearest whole float, .5 will round down
        /// </summary>
        public static float Round(this float self) 
        {
            return Mathf.Round(self);
        }
        
        /// <summary>
        /// Round 'a' to the nearest 'snapValue'
        /// </summary>
        public static float Round(this float self, float increment) 
        {
            if (Mathf.Approximately(increment, 0)) 
                return self;

            float round = Mathf.Round(self * increment);
            float res = round / increment;
            
            return res;
        }

        /// <summary>
        /// Try to parse 'a'
        /// </summary>
        // public static float Parse(this float self, string text, float defaultValue)
        // {
        //     if (!float.TryParse(text, out float value))
        //     {
        //         return defaultValue;
        //     }
        //
        //     return value;
        // }
        
    }
}