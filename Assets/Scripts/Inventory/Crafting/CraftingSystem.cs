using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CraftingSystem : Singleton<CraftingSystem>
{

    [SerializeField] private List<CraftingRecipe> _recipes;
    

    private void Awake()
    {
        CheckRecipesValidity();
    }

    private void CheckRecipesValidity()
    {
        foreach (var recipe in _recipes)
        {
            HashSet<Item> items = new HashSet<Item>();
            foreach (var ingredient in recipe.Ingredients)
            {
                if (items.Contains(ingredient.Item))
                    throw new DuplicateNameException("Item duplicate in recipe");
                items.Add(ingredient.Item);
                if (ingredient.Count < 0)
                    throw new ArgumentOutOfRangeException("Negative count of ingredients");
            }

        }
    }

    private bool CanCraftRecipe(CraftingRecipe recipe, Inventory inventory)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (inventory.Contains(ingredient.Item) < ingredient.Count)
                return false;
        }
        return true;
    }

    public int RecipeCraftCount(CraftingRecipe recipe, Inventory inventory)
    {
        int count = Int32.MaxValue;
        foreach (var ingredient in recipe.Ingredients)
        {
            int canCraft = inventory.Contains(ingredient.Item) / ingredient.Count;
            count = Mathf.Min(count, canCraft);

        }
        return count;
    }
    
    public IEnumerable<CraftingRecipe> GetAvailableRecipes(Inventory inventory)
    {
        List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        foreach (var recipe in _recipes)
        {
            if(CanCraftRecipe(recipe,inventory))
                recipes.Add(recipe);
        }

        return recipes;
    }

    //Maybe return string and show tooltip ???
    public string Craft(CraftingRecipe recipe, Inventory inventory)
    {
        if (recipe == null || inventory == null)
            return "NullReference";
        if (!CanCraftRecipe(recipe, inventory))
            return "Not Enough Resources";
        if(!inventory.IsSpace(recipe.Result))
            return "Not Enough Space in Inventory";

        foreach (var ingredient in recipe.Ingredients)
            inventory.RemoveItem(ingredient.Item, ingredient.Count);
        
        inventory.AddItem(recipe.Result, recipe.ResultCount);
        
        return $"Item {recipe.Result.Name} crafted";
    }
    
    
    
}
