namespace OrderService.Application.Constants;

public static class CacheTtl
{
    public static readonly TimeSpan Short = TimeSpan.FromMinutes(1);
    public static readonly TimeSpan Medium = TimeSpan.FromMinutes(10);
    public static readonly TimeSpan Long = TimeSpan.FromHours(1);
}
