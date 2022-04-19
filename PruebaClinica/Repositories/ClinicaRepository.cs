using Microsoft.EntityFrameworkCore;
using PruebaClinica.Interfaces;
using PruebaClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaClinica.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {

        private readonly ClinicaContext _context;
        public ClinicaRepository(ClinicaContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMedicamento(int IdMedicamento)
        {
            var currentMedicamento = await GetMedicamento(IdMedicamento);
            _context.Medicamento.Remove(currentMedicamento);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public List<Medicamento> GetMedicamentos()
        {
            var medicamentos = _context.Medicamento.ToList();
            return medicamentos;
        }

        public async Task<Medicamento> GetMedicamento(int Id)
        {
            var medicamento = await _context.Medicamento.FirstOrDefaultAsync(x => x.Id == Id);

            return medicamento;
        }

        public async Task<bool> InsertMedicamentos(Medicamento medicamento)
        {
            bool ingresado = false;
            try
            {
                _context.Medicamento.Add(medicamento);
                await _context.SaveChangesAsync();
                ingresado = true;
            }
            catch (Exception)
            {

                ingresado = false;
            }
            
            return ingresado;
        }

        public async Task<Medicamento> UpdateMedicamento(Medicamento medicamento)
        {
            var currentMedicamento = await GetMedicamento(medicamento.Id);
            currentMedicamento.NombreMedicamento = medicamento.NombreMedicamento;
            currentMedicamento.FechaRecibido = medicamento.FechaRecibido;
            currentMedicamento.Cantidad = medicamento.Cantidad;
            currentMedicamento.Valor = medicamento.Valor;

            await _context.SaveChangesAsync();
            return currentMedicamento;
        }
    }
}
