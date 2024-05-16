using Contracts.Employee;
using DataLayer.Domain;
using DataLayer.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace InternshipProject.Mappers
{
    public static class EmployeeMappingExtensions
    {
        public static Employee ToEmployee(this EmployeeCreateRequest employee)
        {
            if (employee == null)
            {
                return null;
            }
            return new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address = employee.Address,
                IDNumber = employee.IdNumber,
                PositionId = employee.PositionID,
                DaysOffNumber = 24,
                Email = employee.Email,
                EmailConfirmed = true,
                UserName = employee.Email,
                NormalizedUserName = employee.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
        public static EmployeeGetResponse ToEmployeeGetResponse(this Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            return new EmployeeGetResponse(
                employee.FirstName,
                employee.LastName,
                employee.Address,
                employee.IDNumber,
                (int)employee.DaysOffNumber,
                (int)employee.PositionId,
                employee.EmploymentStartDate,
                (DateTime)employee.EmploymentEndDate
            );
        }
    }
}
