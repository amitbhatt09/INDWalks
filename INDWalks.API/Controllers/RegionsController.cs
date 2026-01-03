using AutoMapper;
using INDWalks.API.CustomActionFilters;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly INDWalksDbContext dbContext;

        public IRegionRepository regionRepository;
        private IMapper mapper;

        public RegionsController(INDWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            var regionsDomain = await regionRepository.GetAllAsync();



            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs
            return Ok(regionsDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            //var regions = dbContext.Regions.Find(id);
           //Get Region Domain Model From Database
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.ID==id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {


            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.ID }, regionDomainModel);

        }

            [HttpPut]
            [Route("{id:Guid}")]
            [ValidateModel]
            public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
            {


                var regionDomainModel = mapper.Map<UpdateRegionRequestDto>(updateRegionRequestDto);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //Update region Domain Model using data from DTO
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            }


        


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
        {
           // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }


            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }

}
