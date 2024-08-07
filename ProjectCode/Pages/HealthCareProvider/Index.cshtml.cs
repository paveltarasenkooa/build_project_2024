using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildProjectSummer2024.Pages.HealthCareProvider
{
    public class IndexModel : PageModel
    {
        // Let's add variable to store our DB context
        private readonly BuildProject2024Context _context;
        public List<Models.HealthCareProvider> Providers;

        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }


        public void OnGet()
        {
            Providers = _context.HealthCareProviders.ToList();
        }
    }
}
