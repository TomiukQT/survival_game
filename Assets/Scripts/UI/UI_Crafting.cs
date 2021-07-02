using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crafting : MonoBehaviour
{
    [SerializeField] private PlayerItems _playerItems;
    private Inventory _inventory;

    private CraftingRecipe _chosenCraftingRecipe;

    [SerializeField] private Transform _availableRecipesParent;
    [SerializeField] private GameObject _craftingRecipePrefab;


    private void Start()
    {
        _inventory = _playerItems.Inventory;
        _inventory.OnItemChanged += OnInventoryChange;
    }

    private void ShowAvailableRecipes()
    {
        Utils.RemoveAllChilds(_availableRecipesParent);
        List<CraftingRecipe> availableRecipes = CraftingSystem.Instance.GetAvailableRecipes(_inventory).ToList();
        foreach (var recipe in availableRecipes)
        {
            GameObject recipeButton = Instantiate(_craftingRecipePrefab, _availableRecipesParent);
            recipeButton.GetComponent<Button>().onClick.AddListener(() => SelectRecipe(recipe));
            recipeButton.transform.Find("recipe_name").GetComponent<TextMeshProUGUI>().text =
                $"{recipe.Result.Name}  ({recipe.ResultCount})";
        }
    }
    
    private void OnInventoryChange(object sender, EventArgs e)
    {
        ShowAvailableRecipes();
    }

    public void SelectRecipe(CraftingRecipe recipe)
    {
        _chosenCraftingRecipe = recipe;
    }
    
    public void Craft()
    {
        if (_chosenCraftingRecipe == null)
            return;
        CraftingSystem.Instance.Craft(_chosenCraftingRecipe, _inventory);
        ShowAvailableRecipes();
    }
    
    
}
