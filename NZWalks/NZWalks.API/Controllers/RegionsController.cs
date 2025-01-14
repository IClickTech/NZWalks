using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    //[Route("Regions")]
    //[Route("api/[controller]")]
    [Route("[controller]")]

    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);            

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegion([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.GetAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromForm] RegionAdd regionAdd)
        {
            var region = mapper.Map<Region>(regionAdd);

            region.Id = Guid.NewGuid();

            region = await regionRepository.AddAsync(region);

            var regionDto = mapper.Map<RegionDto>(region);

            //return Ok(regionDto);

            return CreatedAtAction(nameof(GetRegion), new {id= regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateResult([FromRoute] Guid id,[FromBody] RegionUpdate regionUpdate)
        {
            var regionDomain = mapper.Map<Region>(regionUpdate);

            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if(regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto> (regionDomain);

            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto> (region);

            return Ok(regionDto);
        }
    }
}
