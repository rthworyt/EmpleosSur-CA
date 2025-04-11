using EmpleosSur.Application.Interfaces.IServices;
using EmpleosSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleosSur.Application.Services
{
    public class EmpleoService : IEmpleoService
    {
        private readonly IRepository<Empleo> _empleoRepository;

        public EmpleoService(IRepository<Empleo> empleoRepository)
        {
            _empleoRepository = empleoRepository;
        }

        public async Task CreateEmpleoAsync(Empleo empleo)
        {
            await _empleoRepository.CreateAsync(empleo);
        }

        public async Task<bool> DeleteEmpleoAsync(int id)
        {
            var empleo = await GetEmpleoByIdAsync(id);
            if (empleo == null)
            {
                return false;
            }
            await _empleoRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Empleo>> GetEmpleosByEmpresaIdAsync(int empresaId)
        {
            return await _empleoRepository.GetAllAsync(e => ((Empleo)e).EmpresaId == empresaId);
        }

        public async Task<IEnumerable<Empleo>> SearchEmpleosByTitleAsync(string title)
        {
            return await _empleoRepository.GetAllAsync(e => ((Empleo)e).Titulo.Contains(title));
        }

        public async Task UpdateEmpleoAsync(Empleo empleo)
        {
            await _empleoRepository.UpdateAsync(empleo);
        }

        public async Task<Empleo> GetEmpleoByIdAsync(int id)
        {
            return await _empleoRepository.GetByIdAsync(id);
        }

        public async Task<List<Empleo>> GetAllEmpleosAsync()
        {
            return (await _empleoRepository.GetAllAsync(e => true)).ToList();
        }

    }
}
