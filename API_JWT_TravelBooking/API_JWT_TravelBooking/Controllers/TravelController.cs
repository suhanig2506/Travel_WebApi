using API_JWT_TravelBooking.Models;
using API_JWT_TravelBooking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT_TravelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ITravelRepository _repository;

        public TravelController(ITravelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelRequest>>> Get()
        {
            IEnumerable<TravelRequest> travelRequests = await _repository.GetTravelRequests();
            if (travelRequests != null)
            {
                return Ok(travelRequests);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelRequest>> GetTravelRequestById(int id)
        {
            TravelRequest travelRequest = await _repository.GetTravelRequestById(id);

            if (travelRequest != null)
            {
                return Ok(travelRequest);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TravelRequest travelRequest)
        {
            if (travelRequest == null)
            {
                return BadRequest();
            }

            travelRequest.ApprovalStatus = "Pending";
            travelRequest.BookingStatus = "Pending";
            travelRequest.CurrentStatus = "Open";

            await _repository.AddTravelRequest(travelRequest);
            return CreatedAtAction(nameof(GetTravelRequestById), new { id = travelRequest.RequestId }, travelRequest);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTravelRequest(int id, [FromBody] TravelRequest updatedTravelRequest)
        {
            if (updatedTravelRequest == null || id != updatedTravelRequest.RequestId)
            {
                return BadRequest();
            }

            TravelRequest existingTravelRequest = await _repository.GetTravelRequestById(id);

            if (existingTravelRequest == null)
            {
                return NotFound();
            }

            existingTravelRequest.LocFrom = updatedTravelRequest.LocFrom;
            existingTravelRequest.LocTo = updatedTravelRequest.LocTo;
            existingTravelRequest.ReqDate = updatedTravelRequest.ReqDate;
            existingTravelRequest.ApprovalStatus = updatedTravelRequest.ApprovalStatus;
            existingTravelRequest.BookingStatus = updatedTravelRequest.BookingStatus;
            existingTravelRequest.CurrentStatus = updatedTravelRequest.CurrentStatus;

            await _repository.UpdateTravelRequest( existingTravelRequest,id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTravelRequest(int id)
        {
            TravelRequest? travelRequest = await _repository.GetTravelRequestById(id);
            if (travelRequest != null)
            {
                await _repository.DeleteTravelRequest(id);
                return NoContent();
            }

            return NotFound();
        }
        [HttpPut("Approve/{id}")]
        public async Task<ActionResult> PutApproveReq(int id, [FromBody] TravelRequest updatedTravelRequest)
        {
            
            await _repository.ApproveTravelRequest( id, updatedTravelRequest);
            return Ok();
        }
        [HttpPut("Book/{id}")]
        public async Task<ActionResult> PutBookReq(int id, [FromBody] TravelRequest updatedTravelRequest)
        {
           
            await _repository.BookTravelRequest(id, updatedTravelRequest);
            return Ok();
        }
    }
}
