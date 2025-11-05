using J3m_BE.DTOs.FoodGroups;
using J3m_BE.Exceptions;
using J3m_BE.Mappers;
using J3m_BE.Models;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Services.Interfaces;

namespace J3m_BE.Services;

// Service for managing food group operations

public class FoodGroupService : IFoodGroupService
{
    private readonly IFoodGroupRepository _repo;

    public FoodGroupService(IFoodGroupRepository repo)
    {
        _repo = repo;
    }

    // Get all food groups with ingredient counts
    public async Task<IEnumerable<FoodGroupDto>> GetAllAsync() =>
        await _repo.GetAllWithIngredientsCountAsync();
    
    // Get a FoodGroup by ID with associated ingredients
    public async Task<FoodGroupDto?> GetByIdAsync(int id)
    {
        var group = await _repo.GetWithIngredientsAsync(id);
        if (group is null)
            throw new NotFoundDomainException($"FoodGroup with ID {id} not found.");

        return group.ToDto();
    }
    
    // Create a new food group
    public async Task<int> CreateAsync(FoodGroupCreateDto dto)
    {
        var name = dto.FoodGroupName?.Trim();
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("FoodGroup name is required.");
        
        // Check for duplicate names
        if (await _repo.ExistsAsync(f => f.FoodGroupName.ToLower() == name.ToLower()))
            throw new ConflictDomainException($"FoodGroup '{name}' already exists.");

        var entity = dto.ToEntity();
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity.FoodGroupId;
    }
    
    // Update an existing food group
    public async Task<bool> UpdateAsync(int id, FoodGroupUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"FoodGroup with ID {id} not found.");
        
        dto.MapToEntity(entity);
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        return true;
    }
    
    // Delete a food group by ID
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundDomainException($"FoodGroup with ID {id} not found.");
        
        _repo.Remove(entity);
        await _repo.SaveChangesAsync();
        return true;
    }
}