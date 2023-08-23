
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
         bool SaveChanges();

        //Platforms
         IEnumerable<Platform> GetPlatforms();
         void CreatePlatform(Platform platform);
         bool PlatformExists(int PlatformId);


        //Commands
        IEnumerable<Command> GetCommands(int PlatformId);
        Command GetCommand(int PlatformId,int CommandId);
        void CreateCommand(int platformId, Command command);


    }
}