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
using System.Text.Json;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly INDWalksDbContext dbContext;

        public IRegionRepository regionRepository;
        private IMapper mapper;

        public ILogger<RegionsController> logger { get; }

        public RegionsController(INDWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                

                logger.LogInformation("GetAll action method invoked");


                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAllregions request with data:{JsonSerializer.Serialize(regionsDomain)}");

                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

                //Return DTOs
                return Ok(regionsDto);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something went wrong in the GetAll method");
                throw;
            }
        }


        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader")]
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
        [Authorize(Roles = "Writer")]
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
         [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Writer")]
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
