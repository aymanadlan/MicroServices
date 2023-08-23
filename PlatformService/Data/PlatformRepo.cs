
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public IEnumerable<Platform> GetAllPlatforms() => _context.Platforms.ToList();

        public Platform GetPlatformById(int id) => _context.Platforms.FirstOrDefault(n => n.Id == id);
        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
              throw new ArgumentNullException(nameof(platform));
            }
            _context.Platforms.Add(platform);
            _context.SaveChanges();
        }
        public void UpdatePlatform(int id, Platform platform)
        {
            throw new NotImplementedException();
        }
        public void DeletePlatform(int id)
        {
          var platform = _context.Platforms.FirstOrDefault(n=>n.Id == id);
          _context.RemoveRange(platform);
          _context.SaveChanges();
        }
    }
}