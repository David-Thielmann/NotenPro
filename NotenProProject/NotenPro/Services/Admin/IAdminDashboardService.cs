using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public interface IAdminDashboardService
{
    Task<AdminDashboardStatsDto?> GetStatsAsync();
}