//using J3m_BE.Data;
//using J3m_BE.Models;
//using J3m_BE.Repositories.Implementations;
//using Microsoft.EntityFrameworkCore;

//namespace J3m_BE.Tests.Repositorys
//{
//    public class AllergyRepositoryTests
//    {
//        private readonly AppDbContext _context;
//        private readonly AllergyRepository _repository;

//        public AllergyRepositoryTests()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            _context = new AppDbContext(options);
//            _repository = new AllergyRepository(_context);

//            SeedData();
//        }

//        private void SeedData()
//        {
//            var allergies = new List<Allergy>
//            {
//                new Allergy { AllergyId = 1, AllergyName = "Peanuts" },
//                new Allergy { AllergyId = 2, AllergyName = "Milk" }
//            };

//            var allergy = new Allergy
//            {
//                AllergyId = 3,
//                AllergyName = "Gluten",

//            };
//            _context.Allergies.Add(allergy);
//            _context.SaveChanges();
//        }


//    }
//}
