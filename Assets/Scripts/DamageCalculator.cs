using System;
using System.Collections.Generic;

public static class DamageCalculator
{

    public static Dictionary<Tuple<Element, Element>, Element> _combinations =
        new Dictionary<Tuple<Element, Element>, Element>()
        {
            //{new Tuple<Element, Element>(Element.,Element.),Element.},
            //{new Tuple<Element, Element>(Element,Element),Element},
            {new Tuple<Element, Element>(Element.Fire, Element.Water), Element.Air},
            {new Tuple<Element, Element > (Element.Fire, Element.Ice), Element.Water},
            {new Tuple<Element, Element>(Element.Holy,Element.Shadow),Element.None},
            {new Tuple<Element, Element>(Element.Water,Element.Ice),Element.Ice},
        };
    
    public static float CalculateDamage(float damage)
    {
        return 0f;
    }

    public static Element GetElementCombination(Element a, Element b)
    {
        Element final = Element.None;
        if (_combinations.TryGetValue(new Tuple<Element, Element>(a, b), out final))
            return final;
        if (_combinations.TryGetValue(new Tuple<Element, Element>(b, a), out final))
            return final;
        return final;
    }
    
}