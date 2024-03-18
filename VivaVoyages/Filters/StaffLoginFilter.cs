using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VivaVoyages.Models;
using System.Threading.Tasks;
namespace VivaVoyages.Filters
{
    public class StaffLoginFilter : IAsyncActionFilter
    {
        private readonly LoginChecker _loginChecker;

        public StaffLoginFilter(LoginChecker loginChecker)
        {
            _loginChecker = loginChecker;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await CheckStaffAsync();
            if (result != null)
            {
                context.Result = result;
                return;
            }

            await next();
        }

        private async Task<IActionResult> CheckStaffAsync()
        {
            // Assuming your CheckStaff method is synchronous
            return await Task.FromResult(_loginChecker.CheckStaff());
        }
    }
}