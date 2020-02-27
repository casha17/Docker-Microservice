using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Worker;

namespace frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        

        public IndexModel(ILogger<IndexModel> logger , IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public void OnGet()
        {
        }
        
        public IActionResult OnPost(string value)
        {
            _rabbitMqService.publish(value);
            return Page();
        }
    }
}