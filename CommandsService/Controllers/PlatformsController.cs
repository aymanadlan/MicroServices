
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
 {
   [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository,IMapper mapper)
       {
         _repository=repository;
         _mapper=mapper;
       }
       
       [HttpGet]
       public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
       {
         System.Console.WriteLine("Getting Platforms from CommandService");

         var formItems = _repository.GetPlatforms();

          return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(formItems));
       } 

      [HttpPost]
       public ActionResult TestInboundConnection()
       {
          System.Console.WriteLine("--> Inbound POST # Commands Service");

          return Ok("Inbound test from PlatformsController");
       }
    }
 }