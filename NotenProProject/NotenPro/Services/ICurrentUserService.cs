using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services
{
    public interface ICurrentUserService
    {
        Task<AuthMeDto> GetMeAsync();
    }
}