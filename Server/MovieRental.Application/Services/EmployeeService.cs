using AutoMapper;
using MovieRental.Application.Dtos.Employee;
using MovieRental.Application.Interfaces;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;

namespace MovieRental.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (!Enum.IsDefined(typeof(Role), employeeDto.Role))
            {
                throw new ArgumentException("Invalid role value.");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddEmployee(employee);
        }
    }
}
