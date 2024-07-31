using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildProjectSummer2024.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        private readonly ILogger<IndexModel> _logger;
        [ViewData]
        public string Message { get; set; }
        public IndexModel(ILogger<IndexModel> logger, BuildProject2024Context context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

          
        }
    }
}
