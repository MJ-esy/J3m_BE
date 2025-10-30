using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;
using J3m_BE.DTOs.Ingredients;
using J3m_BE.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Services;

// Ingredient Service Implementation

public class IngredientService : IIngredientService
{
    private readonly IIngredientRepository _repo;
    
    public IngredientService(IIngredientRepository repo)
    {
        _repo = repo;
    }
    
    // Get all Ingredients
    public async Task<IEnumerable<IngredientDto>> GetAllAsync()
    {
        // Fetch all ingredients
        var ingredients = await _repo.GetAllAsync();
        
        // Map to IngredientDto
        return ingredients.Select(i => new IngredientDto
        {
            IngredientId = i.IngredientId,
            IngredientName = i.IngredientName,
            FoodGroupName = i.FoodGroup?.FoodGroupName
        });
    }
    
    // Get Ingredient by ID with details
    public async Task<IngredientDetailDto> GetByIdAsync(int id)
    {
        // Fetch ingredient with related details
        var ingredient = await _repo.GetWithDetailsAsync(id);
        if (ingredient is null)
            throw new NotFoundDomainException($"Ingredient with ID {id} not found.");

        // Map to IngredientDetailDto
        return new IngredientDetailDto
        {
            IngredientId = ingredient.IngredientId,
            IngredientName = ingredient.IngredientName,
            FoodGroupName = ingredient.FoodGroup?.FoodGroupName ?? "Unknown",
            Allergies = ingredient.AllergyLinks.Select(a => a.Allergy!.AllergyName),
            NutrientGroups = ingredient.NutrientLinks.Select(ng => ng.NutrientGroup!.NutrientGroupName)
        };
    }
    
    // Create a new Ingredient
    public async Task<int> CreateAsync(IngredientCreateDto dto)
    {
        var name = dto.IngredientName?.Trim();
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Ingredient name is required.");
        
        // Prevent duplicates
        if (await _repo.ExistsAsync(i => i.IngredientName.ToLower() == name.ToLower()))
            throw new ConflictDomainException($"Ingredient with name {name} already exists.");
        
        // Create new Ingredient entity
        var ingredient = new Ingredient
        {
            IngredientName = name,
            FoodGroupId = dto.FoodGroupId
        };
        
        // Save to repository
        await _repo.AddAsync(ingredient);
        await _repo.SaveChangesAsync();
        
        return ingredient.IngredientId;
    }
    
    // Update an existing Ingredient
    public async Task<bool> UpdateAsync(int id, IngredientUpdateDto dto)
    {
        // Fetch existing ingredient
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"Ingredient with ID {id} not found.");
        
        entity.IngredientName = dto.IngredientName.Trim();
        entity.FoodGroupId = dto.FoodGroupId;
        
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        
        return true;
    }
    
    // Remove an Ingredient
    public async Task<bool> DeleteAsync(int id)
    {
        // Check if ingredient is used in any recipe
        if (await _repo.IsUsedInRecipeAsync(id))
            throw new ConflictDomainException($"Ingredient with ID {id} is used in a recipe.");
        
        // Fetch existing ingredient
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"Ingredient with ID {id} not found.");
        
        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
        return true;
    }
}