namespace HTLKrems.GradeManagement.Services;

public sealed class StudentApiService : IStudentService
{
    private readonly IGradeService _grades;
    private readonly INotificationService _notifications;

    public StudentApiService(IGradeService grades, INotificationService notifications)
    {
        _grades = grades;
        _notifications = notifications;
    }

    public async Task<StudentDashboardStats> GetDashboardStatsAsync()
    {
        var grades = await _grades.GetMyGradesAsync();
        var unread = await _notifications.GetUnreadCountAsync();

        // Average: nur dort wo GradeValue gesetzt > 0
        var graded = grades.Where(g => g.GradeValue > 0).ToList();
        var avg = graded.Count > 0 ? graded.Average(g => g.GradeValue) : 0m;

        // Ungraded: Status != Graded ODER GradeValue == 0
        var ungraded = grades.Count(g => g.GradeValue <= 0 || g.Status != Models.GradeStatus.Graded);

        return new StudentDashboardStats
        {
            AverageGrade = avg,
            UngradedTests = ungraded,
            UnreadNotifications = unread,
            ClassName = "-" // ClassName machen wir als nächsten Schritt sauber (braucht API-Unterstützung)
        };
    }
}