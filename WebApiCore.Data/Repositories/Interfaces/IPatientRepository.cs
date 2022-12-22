using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Model.Entities;

namespace WebApiCore.Data.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientsId(int Id);
        Task<bool> createPatient(Patient patien);
        Task<bool> updatePatient(Patient patien);
        Task<bool> deletePatient(Patient patient);
    }
}
