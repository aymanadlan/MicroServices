

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
 {
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepo repository,
        IMapper mapper,
        ICommandDataClient commandDataClient)
        {
            _repository=repository;
            _mapper=mapper;
            _commandDataClient=commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            System.Console.WriteLine("--> Getting Platforms");
          var allPlatforms = _repository.GetAllPlatforms();

          return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(allPlatforms));
        }

        [HttpGet("{id}",Name ="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
          var PlatformItem = _repository.GetPlatformById(id);

          if (PlatformItem != null)
          {
          return Ok(_mapper.Map<PlatformReadDto>(PlatformItem));
          }
          else
          {
            return NotFound();
          }
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platformDto)
        {
            var platformModel =_mapper.Map<Platform>(platformDto);

            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);


            try
            {
              await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (System.Exception ex)
            {
              System.Console.WriteLine($"Could not send synchronusly, {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById),new {Id=platformReadDto.Id}, platformReadDto);
        }
    }
 }