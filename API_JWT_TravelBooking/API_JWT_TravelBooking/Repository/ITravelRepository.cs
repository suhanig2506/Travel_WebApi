using API_JWT_TravelBooking.Models;

namespace API_JWT_TravelBooking.Repository
{
    public interface ITravelRepository
    {
        Task<IEnumerable<TravelRequest>> GetTravelRequests();

        Task<TravelRequest> AddTravelRequest(TravelRequest travelRequest);
        Task DeleteTravelRequest(int requestId);
        

        Task<TravelRequest> GetTravelRequestById(int id);
        Task UpdateTravelRequest(TravelRequest travelRequest, int requestId);
        //void UpdateRequest(TravelRequest travelRequest, int id);
        Task  BookTravelRequest(int RequestId, TravelRequest tr);
        Task ApproveTravelRequest(int RequestId, TravelRequest tr);
    }
}
