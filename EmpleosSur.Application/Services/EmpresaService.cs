using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using EmpleosSur.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Services
{
    public class EmpresaService
    {
        private readonly IRepository<Empresa> _repository;
        public EmpresaService(IRepository<Empresa> repository)
        {
            _repository = repository;
        }
        public async Task<Empresa> GetEmpresaByEmailAsync(string email)
        {
            return (await _repository.GetAllAsync(e => ((Empresa)e).EmailCorporativo == email)).FirstOrDefault();
        }
        public async Task<OperationResult> CreateEmpresaAsync(Empresa empresa)
        {
            await _repository.CreateAsync(empresa);
            return OperationResult.SuccessResult();
        }
        public async Task DeleteEmpresaAsync(int id)
        {
            var empresa = await GetEmpresaByIdAsync(id);
            if (empresa == null)
            {
                throw new Exception("La empresa no existe.");
            }
            await _repository.DeleteAsync(id);
        }
        public async Task UpdateEmpresaAsync(Empresa empresa)
        {
            await _repository.UpdateAsync(empresa);
        }
        public async Task<Empresa> GetEmpresaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Empresa> GetEmpresaByRNCAsync(string rnc)
        {
            return (await _repository.GetAllAsync(e => ((Empresa)e).RNC == rnc)).FirstOrDefault();
        }

    }
}
