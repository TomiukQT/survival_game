using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<Ingredient> Ingredients;
    public Item Result;

}

[System.Serializable]
public class Ingredient
{
    public Item Item;
    public int Count;
}
