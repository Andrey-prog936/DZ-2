using Librarry.ActionResults;
using Librarry.Data.Services;
using Librarry.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publisherService;

        public PublishersController(PublishersService publishersService)
        {
            _publisherService = publishersService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            var allPublishers = _publisherService.GetAllPublishers(sortBy, searchString, pageNumber);
            return Ok(allPublishers);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResults GetPublisherById(int id)
        {
            var _publisher = _publisherService.GetPublisherById(id);
            if(_publisher != null)
            {
                var _responceObject = new CustomActionResultVM()
                {
                    Publisher = _publisher
                };

                return new CustomActionResults(_responceObject);
            }
            else
            {
                var _responceObject = new CustomActionResultVM()
                {
                    Exception = new Exception("Publisher not found.")
                };

                return new CustomActionResults(_responceObject);
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _pablosherData = _publisherService.GetPublisherData(id);
            if(_pablosherData != null)
            {
                return Ok(_pablosherData);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            var newPublisher = _publisherService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), newPublisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
