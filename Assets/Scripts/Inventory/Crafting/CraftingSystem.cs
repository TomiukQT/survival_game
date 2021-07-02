using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
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
}
