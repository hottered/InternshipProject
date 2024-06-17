using Contracts.AdminPanel;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternshipProject.ViewComponents
{
    public class AdminPanelViewComponent : ViewComponent
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRequestRepository _userRequestRepository;

        public AdminPanelViewComponent(
            IAccountRepository accountRepository,
            IUserRequestRepository userRequestRepository
            )
        {
            _accountRepository = accountRepository;
            _userRequestRepository = userRequestRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var adminPanel = new AdminDashboardCount();

            adminPanel.LeaveRequests = await _userRequestRepository.GetAllRequestsCountAsync(new Contracts.Request.UserRequestFilter());
            adminPanel.Employees = await _accountRepository.GetAllUsersCountAsync(new Contracts.Employee.EmployeeFilter());
            adminPanel.LeaveRequestsOnWait = await _userRequestRepository.GetAllStandbyRequestsCountAsync(); ;

            return View(adminPanel);
        }
    }
}
