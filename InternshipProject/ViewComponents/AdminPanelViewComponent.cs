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

            var countUsers = await _accountRepository.GetAllUsersCountAsync(new Contracts.Employee.EmployeeFilter());
            var countRequests = await _userRequestRepository.GetAllRequestsCountAsync(new Contracts.Request.UserRequestFilter());
            var countRequestsOnStandby = await _userRequestRepository.GetAllStandbyRequestsCountAsync();

            adminPanel.LeaveRequests = countRequests;
            adminPanel.Employees = countUsers;
            adminPanel.LeaveRequestsOnWait = countRequestsOnStandby;

            return View(adminPanel);
        }
    }
}
