using J3m_BE.DTOs.Recipes;
using J3m_BE.Models;
using J3m_BE.Models.Links;

namespace J3m_BE.Mappers;

public static class RecipeMapper
{
    // Map Recipe entity to RecipeSummaryDto
    public static RecipeSummaryDto ToSummaryDto(this Recipe recipe)
    {
        return new RecipeSummaryDto
        {
            RecipeId = recipe.RecipeId,
            RecipeName = recipe.RecipeName,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            IngredientCount = recipe.IngredientLinks?.Count ?? 0,
            DietCount = recipe.DietLinks?.Count ?? 0
        };
    }

    // Map Recipe entity to RecipeDetailDto
    public static RecipeDetailDto ToDetailDto(this Recipe recipe)
    {
        return new RecipeDetailDto
        {
            RecipeId = recipe.RecipeId,
            RecipeName = recipe.RecipeName,
            Description = recipe.Description,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            ImageUrl = recipe.ImageUrl,
            Diets = recipe.DietLinks.Select(d => d.Diet!.DietName)
                .OrderBy(n => n),
            Ingredients = recipe.IngredientLinks
                .Select(ir => new IngredientLineDto
                {
                    IngredientId = ir.IngredientId,
                    IngredientName = ir.Ingredient!.IngredientName,
                    Measurement = ir.Measurement,
                })
            .OrderBy(x => x.IngredientName)
        };
    }
    
    // Map RecipeCreateDto to Recipe entity
    public static Recipe ToEntity(this RecipeCreateDto dto)
    {
        return new Recipe
        {
            RecipeName = (dto.RecipeName ?? string.Empty).Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description!.Trim(),
            PrepTimeMinutes = dto.PrepTimeMinutes,
            ImageUrl = string.IsNullOrWhiteSpace(dto.ImageUrl) ? null : dto.ImageUrl!.Trim(),
        };
    }
    
    // Update existing Recipe entity with data from RecipeUpdateDto
    public static void ApplyUpdate(this RecipeUpdateDto dto, Recipe entity)
    {
        if (dto.RecipeName != null)
            entity.RecipeName = dto.RecipeName.Trim();
        
        if (dto.Description != null)
            entity.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
        
        if (dto.PrepTimeMinutes.HasValue)
            entity.PrepTimeMinutes = dto.PrepTimeMinutes.Value;
        
        if (dto.ImageUrl != null)
            entity.ImageUrl = string.IsNullOrWhiteSpace(dto.ImageUrl) ? null : dto.ImageUrl.Trim();
        
        if (dto.Ingredients != null)
            SyncIngredientLinks(entity, dto.Ingredients);
        if (dto.DietIds != null)
            SyncDietLinks(entity, dto.DietIds);
    }
    
    // Sync Ingredient links based on DTO
    private static void SyncIngredientLinks(Recipe entity, IReadOnlyCollection<IngredientAmountDto> dtos)
    {
        // Clean the incoming input, keeping only the last occurrence of each IngredientId and trimming measurements
        // Then build a lookup dictionary
        // This ensures that if an ingredient appears multiple times, only the last one is used
        var requestedById = dtos
            .GroupBy(i => i.IngredientId)
            .Select(g =>
            {
                var last = g.Last();
                var measurement = string.IsNullOrWhiteSpace(last.Measurement) 
                    ? null 
                    : last.Measurement!.Trim();
                return new { last.IngredientId, Measurement = measurement };
            })
            .ToDictionary(x => x.IngredientId, x => x);
        
        // Quick lookup of existing links by IngredientId
        // This allows efficient checking of what needs to be added, updated, or removed
        var existingById = entity.IngredientLinks.ToDictionary(ir => ir.IngredientId, ir => ir);
        
        // Remove links that are no longer desired, i.e., those not present in the desired set
        foreach (var toRemove in entity.IngredientLinks
                     .Where(ir => !requestedById.ContainsKey(ir.IngredientId))
                     .ToList())
        {
            entity.IngredientLinks.Remove(toRemove);
        }
        
        // Add or update links to match the desired state
        foreach (var (ingredientId, requested) in requestedById)
        {
            if (existingById.TryGetValue(ingredientId, out var existingLink))
            {
                // Already exists, only update if measurement has changed
                if (existingLink.Measurement != requested.Measurement)
                {
                    existingLink.Measurement = requested.Measurement;
                }   
            }
            else
            {
                // If link doesn't exist, create a new one
                entity.IngredientLinks.Add(new IngredientRecipe
                {
                    IngredientId = ingredientId,
                    Measurement = requested.Measurement
                });
            }
        }
    }
    
    // Sync Diet links based on DTO
    private static void SyncDietLinks(Recipe entity, IReadOnlyCollection<int> dietIds)
    {
        // Clean the incoming input to get unique DietIds
        // This ensures no duplicate DietIds are processed
        var requested = dietIds.Distinct().ToHashSet();
        
        // Quick lookup of existing links by DietId
        // This allows efficient checking of what needs to be added or removed
        var existing = entity.DietLinks.Select(l => l.DietId).ToHashSet();
        
        // Remove links that are no longer desired
        // i.e., those DietIds not present in the desired set
        foreach (var link in entity.DietLinks.Where(l => !requested.Contains(l.DietId)).ToList())
        {
            entity.DietLinks.Remove(link);
        }
        
        // Add new links for DietIds that are requested but not currently linked
        // i.e., those present in request but not in existing
        foreach (var toAdd in requested.Except(existing))
        {
            // Create new link
            entity.DietLinks.Add(new DietRecipe { DietId = toAdd });
        }
    }
}
