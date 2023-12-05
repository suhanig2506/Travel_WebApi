using API_JWT_TravelBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API_JWT_TravelBooking.Repository
{
    public class TravelRepository:ITravelRepository
    {
        private readonly ApplicationDbContext _context;

        public TravelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequests()
        {
            return await _context.TravelRequests.ToListAsync();
        }

        public async Task<TravelRequest> AddTravelRequest(TravelRequest travelRequest)
        {
            if (travelRequest != null)
            {
                await _context.AddAsync(travelRequest);
                await _context.SaveChangesAsync();
            }
            return travelRequest;
        }

        public async Task DeleteTravelRequest(int requestId)
        {
            TravelRequest? travelRequest = _context.TravelRequests.FirstOrDefault(x => x.RequestId == requestId);
            if (travelRequest != null)
            {
                _context.Remove(travelRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TravelRequest> GetTravelRequestById(int requestId)
        {
            TravelRequest? travelRequest = _context.TravelRequests.FirstOrDefault(x => x.RequestId == requestId);
            return travelRequest;
        }
        public async Task UpdateTravelRequest(TravelRequest travelRequest, int requestId)
        {
            TravelRequest? travelRequestOld = _context.TravelRequests.FirstOrDefault(x => x.RequestId == requestId);
            if (travelRequestOld != null)
            {
                
                travelRequestOld.LocFrom = travelRequest.LocFrom;
                travelRequestOld.LocTo = travelRequest.LocTo;
                travelRequestOld.ReqDate = travelRequest.ReqDate;
                travelRequestOld.ApprovalStatus = travelRequest.ApprovalStatus;
                travelRequestOld.BookingStatus = travelRequest.BookingStatus;
                travelRequestOld.CurrentStatus = travelRequest.CurrentStatus;

                await _context.SaveChangesAsync();
            }
        }
        public async Task ApproveTravelRequest(int Id,  TravelRequest tr)
        {
            TravelRequest? travelRequestOld = _context.TravelRequests.FirstOrDefault(x => x.RequestId == Id);
            if (travelRequestOld != null)
            {
                travelRequestOld.ApprovalStatus=tr.ApprovalStatus;
                if(tr.ApprovalStatus=="Not Approve")
                {
                    travelRequestOld.CurrentStatus = "Close";
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task BookTravelRequest(int Id, TravelRequest tr)
        {
            TravelRequest? travelRequestOld = _context.TravelRequests.FirstOrDefault(x => x.RequestId == Id);
            if (travelRequestOld != null)
            {
                travelRequestOld.BookingStatus = tr.BookingStatus;
                travelRequestOld.CurrentStatus = "Close";
                await _context.SaveChangesAsync();
            }
        }
    }
}
