using PruebaClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaClinica.Interfaces
{
    public interface IClinicaRepository
    {
        #region "Metodos de Medicamentos"
        List<Medicamento> GetMedicamentos();
        Task<Medicamento> GetMedicamento(int Id);

        Task<bool> InsertMedicamentos(Medicamento medicamento);

        Task<bool> DeleteMedicamento(int IdMedicamento);

        Task<Medicamento> UpdateMedicamento(Medicamento medicamento);

        #endregion
    }
}
