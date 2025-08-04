using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Domain.Entities;
using EmployeeContactSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeContactSystem.Application.Models;


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

        //public async Task<IEnumerable<EmployeeDto>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10)
        //{
        //    var query = _context.Employees
        //        .Include(e => e.Company)
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(search))
        //    {
        //        query = query.Where(e =>
        //            e.Name.Contains(search) ||
        //            e.Email.Contains(search) ||
        //            e.Phone.Contains(search) ||
        //            e.JobTitle.Contains(search));
        //    }

        //    return await query
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();
        //}

        public async Task<PagedResult<EmployeeDto>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10)
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

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<EmployeeDto>
            {
                Items = items,
                TotalCount = totalCount
            };
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

            var company = await _context.Companies.FirstOrDefaultAsync(e => e.ID == dto.CompanyID);
            if (company == null) throw new KeyNotFoundException("Company not found.");

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Reload with company to return full EmployeeDto
            employee = await _context.Employees.Include(e => e.Company).FirstAsync(e => e.ID == employee.ID);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            if (employee == null) throw new KeyNotFoundException("Employee not found.");

            if (dto.CompanyID.HasValue)
            {
                var company = await _context.Companies.FirstOrDefaultAsync(e => e.ID == dto.CompanyID.Value);
                if (company == null)
                    throw new KeyNotFoundException("Company not found.");
            }

            #region Mapping
            if (dto.Name != null)
                employee.Name = dto.Name;

            if (dto.Email != null)
                employee.Email = dto.Email;

            if (dto.Phone != null)
                employee.Phone = dto.Phone;

            if (dto.JobTitle != null)
                employee.JobTitle = dto.JobTitle;

            if (dto.CompanyID.HasValue)
                employee.CompanyID = dto.CompanyID.Value;

            if (dto.IsActive.HasValue)
                employee.IsActive = dto.IsActive.Value;
            #endregion

            await _context.SaveChangesAsync();

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

        public async Task<bool> ExistsAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            { 
                return false; 
            }

            return true;
        }
    }
}
