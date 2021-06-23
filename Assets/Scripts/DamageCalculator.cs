using System;
using System.Collections.Generic;

public static class DamageCalculator
{

    public static Dictionary<int,int> _combinations =
        new Dictionary<int,int>()
        {
            //{new Tuple<Element, Element>(Element.,Element.),Element.},
            //{new Tuple<Element, Element>(Element,Element),Element},
            {(int)Element.Fire + (int)Element.Water, (int)Element.Water},
            {(int)Element.Ice + (int)Element.Water, (int)Element.Ice},
            {(int)Element.Water + (int)Element.Lightning, (int)Element.Lightning},
        };
    
    public static float CalculateDamage(float damage)
    {
        return 0f;
    }

    public static Element GetElementCombination(Element a, Element b)
    {
        return _combinations.TryGetValue((int) a + (int) b, out int value) ? (Element)value : Element.None;
    }
    
}