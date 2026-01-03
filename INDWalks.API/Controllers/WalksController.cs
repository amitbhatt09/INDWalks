using AutoMapper;
using INDWalks.API.CustomActionFilters;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private IMapper mapper;
        private IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Create walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //MAP dommain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            
            
        }


        //api/walks?filteron=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string?filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true
              , pageNumber, pageSize);

            //Map domain model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
                return NotFound();

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWakRequestDto)
        {
            
                //MAP DTO to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWakRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                    return NotFound();

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
                return NotFound();

            //Map domain model to DTO
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }

    }
}
