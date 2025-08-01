using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Domain.Entities;
using EmployeeContactSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Infrastructure.Services
{
    /// <summary>
    /// Provides business logic for employee operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Employees
                .Include(e => e.Company)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.Name.Contains(search) ||
                    e.Email.Contains(search) ||
                    e.Phone.Contains(search) ||
                    e.JobTitle.Contains(search));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.ID == id);

            if (employee == null) throw new KeyNotFoundException("Employee not found.");

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            employee.CreatedAt = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Reload with company to return full EmployeeDto
            employee = await _context.Employees.Include(e => e.Company).FirstAsync(e => e.ID == employee.ID);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");

            // Manual mapping to avoid tracking issues
            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.JobTitle = dto.JobTitle;
            employee.CompanyID = dto.CompanyID;

            await _context.SaveChangesAsync();

            employee = await _context.Employees.Include(e => e.Company).FirstAsync(e => e.ID == employee.ID);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
