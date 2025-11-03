using J3m_BE.DTOs.Allergies;
using J3m_BE.Repositories.Interfaces;
using J3m_BE.Data;
using Microsoft.EntityFrameworkCore;
using J3m_BE.Models;

namespace J3m_BE.Repositories.Implementations
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly AppDbContext _context;

        public AllergyRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all allergies
        public async Task<List<AllergyDto>> GetAllAllergiesAsync()
        {
            var allergiesList = await _context.Allergies.ToListAsync();

            return allergiesList.Select(a => new AllergyDto
            {
                AllergyId = a.AllergyId,
                AllergyName = a.AllergyName
               
            }).ToList();
        }

        // Get allergy by ID
        public async Task<AllergyDto?> GetAllergyByIdAsync(int allergyId)
        {
            var allergy = await _context.Allergies.FindAsync(allergyId);
            if (allergy == null) return null;

            return new AllergyDto
            {
                AllergyId = allergy.AllergyId,
                AllergyName = allergy.AllergyName
            };

        }

        // Create/add a new allergy
        public async Task<AllergyCreateDto> CreateAllergyAsync(AllergyCreateDto allergyCreateDto)
        {
            var newAllergy = new Allergy
            {
                AllergyName = allergyCreateDto.AllergyName
            };
                
            _context.Allergies.Add(newAllergy);
            await _context.SaveChangesAsync();

            return new AllergyCreateDto
            {
                AllergyId = newAllergy.AllergyId,
                AllergyName = newAllergy.AllergyName
            };

        }

        // Update an existing allergy
        public async Task<AllergyUpdateDto?> UpdateAllergyAsync(int allergyId, AllergyUpdateDto allergyUpdateDto)
        {
            var allergy = await _context.Allergies.FindAsync(allergyId);
            if (allergy == null) return null;

            allergy.AllergyName = allergyUpdateDto.AllergyName;
            await _context.SaveChangesAsync();

            return new AllergyUpdateDto
            {
                AllergyId = allergy.AllergyId,
                AllergyName = allergy.AllergyName
            };
        }

        // Delete an allergy
        public async Task<bool> DeleteAllergyAsync(int allergyId)
        {
            var allergy = await _context.Allergies.FindAsync(allergyId);
            if (allergy == null) return false;

            _context.Allergies.Remove(allergy);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
