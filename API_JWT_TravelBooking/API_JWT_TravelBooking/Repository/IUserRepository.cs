using ClassLibraryAPI_JWP_Trvael;

namespace API_JWT_TravelBooking.Repository
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    }
}
