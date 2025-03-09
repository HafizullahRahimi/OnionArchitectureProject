namespace OnionArchitectureProject.Application.Common.Extensions;
public static class DateTimeExtensions
{
    public static DateOnly ToUtcDateOnly(this DateTime localDateTime)
    {
        return DateOnly.FromDateTime(localDateTime.ToUniversalTime());
    }

    public static TimeOnly ToUtcTimeOnly(this TimeOnly localTime)
    {
        var localDateTime = DateTime.Now.Date.Add(localTime.ToTimeSpan());
        var utcDateTime = localDateTime.ToUniversalTime();
        return TimeOnly.FromDateTime(utcDateTime);
    }
}