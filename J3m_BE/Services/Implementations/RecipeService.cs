using J3M.Shared.DTOs.Recipes;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Services.Implementations;

// Service for managing Recipe entities

public class RecipeService : IRecipeService
{
    // Dependency: Recipe repository
    private readonly IRecipeRepository _repo;
    public RecipeService(IRecipeRepository repo)
    {
        _repo = repo;
    }

    // Get all Recipes (summary)
    public async Task<IEnumerable<RecipeSummaryDto>> GetAllAsync()
    {
        // Fetch all recipes with related data
        var recipes = await _repo.QueryWithIncludes().ToListAsync();
        // Map to summary DTOs
        return recipes.Select(r => r.ToSummaryDto());
    }

    // Get a full Recipe by ID
    public async Task<RecipeDetailDto> GetByIdAsync(int id)
    {
        // Fetch recipe with related details
        var recipe = await _repo.GetWithDetailsAsync(id);
        if (recipe is null)
            throw new NotFoundDomainException($"Recipe with ID {id} not found.");

        // Map to Detail DTO
        return recipe.ToDetailDto();
    }

    // Create a new Recipe
    public async Task<int> CreateAsync(RecipeCreateDto dto)
    {
        // Validate name
        var name = dto.RecipeName.Trim();
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Recipe name cannot be empty.");

        // Check for duplicate names
        if (await _repo.ExistsByNameAsync(name))
            throw new ConflictDomainException($"Recipe with name '{name}' already exists.");

        // Map to entity
        var entity = dto.ToEntity();

        // Add and save the new entity
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity.RecipeId;
    }

    // Update an existing Recipe
    public async Task<bool> UpdateAsync(int id, RecipeUpdateDto dto)
    {
        // Fetch the entity
        var entity = await _repo.GetWithDetailsAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"Recipe with ID {id} not found.");

        // Map to simple fields
        dto.ApplyUpdate(entity);

        // Update entity
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        return true;
    }

    // Delete a Recipe by ID
    public async Task<bool> DeleteAsync(int id)
    {
        // Fetch the entity
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"Recipe with ID {id} not found.");

        // Remove the entity
        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
        return true;
    }

    //Filter Recipes by matching ingredient IDs
    public async Task<List<RecipeDetailDto>> FilterByIngredientsAsync(IEnumerable<int> ingredientsIds)
    {
        //Set minimum match count
        const int minMatchCount = 2;
        var recipeList = await _repo.GetByMatchingIngredientsAsync(ingredientsIds, minMatchCount);

        if (recipeList is null || !recipeList.Any())
            throw new NotFoundDomainException("No recipes found matching the provided ingredients.");

        return recipeList.Select(r => r.ToDetailDto()).ToList();
    }
}