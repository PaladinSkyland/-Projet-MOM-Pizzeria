using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Services;

namespace WebApplication2.Pages;

public class Kitchen : PageModel
{
    public IList<string> Notifications { get; set; } = null!;

    public void OnGet()
    {
        Notifications = RabbitClient.GetNotifications("kitchen_exchange", "kitchen");
    }
}

