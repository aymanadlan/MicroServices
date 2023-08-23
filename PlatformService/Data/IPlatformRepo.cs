
using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepo
{
    bool  SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int id);

    void CreatePlatform(Platform platform);
    void UpdatePlatform(int id, Platform platform);
    void DeletePlatform(int id);
}