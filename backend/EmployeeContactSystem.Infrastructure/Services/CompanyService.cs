using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Application.Interfaces;
using EmployeeContactSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies(string? search = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Companies
                .Include(e => e.Employees)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.CompanyName.Contains(search) ||
                    e.Domain.Contains(search) ||
                    e.Industry.Contains(search) ||
                    e.Website.Contains(search));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
