using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiCore.Data.Repositories.Interfaces;
using WebApiCore.Model.Entities;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace WebApiCore.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MySqlConfig _connectionString;
        public PatientRepository(MySqlConfig connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        async Task<IEnumerable<Patient>> IPatientRepository.GetAllPatients()
        {
            var db = dbConnection();

            List<Patient> patientsList = new();
            MySqlCommand command = db.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_getAllPatient";

            db.Open();
            using (var results = await command.ExecuteReaderAsync())
            {
                while (results.Read())
                {
                    Patient patients = new()
                    {
                        Id = results.GetInt32("id"),
                        Name = results.GetString("name"),
                        Surnames = results.GetString("surnames"),
                        Direction = results.GetString("direction"),
                        Email = results.GetString("email"),
                        Phone = results.GetString("phone"),
                        City = results.GetString("City"),
                        Location = results.GetString("location"),
                        Nationality = results.GetString("nationality"),
                        Birthday_date = results.GetDateTime("birthday_date"),
                    };
                    patientsList.Add(patients);
                }
            }
            db.Close();

            return patientsList;
        }

        async Task<Patient> IPatientRepository.GetPatientsId(int Id)
        {
            var db = dbConnection();

            var query = @"SELECT id
                          FROM bd_patients.paciente
                          WHERE id = @Id";

            return await db.QueryFirstOrDefaultAsync<Patient>(query, new { Id });
        }

        async Task<bool> IPatientRepository.createPatient(Patient patient)
        {
            var db = dbConnection();

            var query = @"INSERT INTO bd_patients.paciente
                        (id, name, surnames, direction, email, phone, city, location, birthday_date, nationality)
                        VALUES(@Id, @Name, @Surnames, @Direction, @Email, @Phone, @City, @Location, @Birthday_date, @Nationality)"
            ;

            var result = await db.ExecuteAsync(query, new { patient.Id, patient.Name, patient.Surnames, patient.Direction, patient.Email, patient.Phone, patient.City, patient.Location, patient.Birthday_date, patient.@Nationality });

            return result > 0;
        }

        async Task<bool> IPatientRepository.updatePatient(Patient patient)
        {
            var db = dbConnection();

            var query = @"UPDATE bd_patients.paciente
                        SET name = @Name, surnames=@Surnames, direction=@Direction, email=@Email, phone=@Phone, city=@City, location=@Location, birthday_date=@Birthday_date, nationality=@Nationality
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(query, new { patient.Id, patient.Name, patient.Surnames, patient.Direction, patient.Email, patient.Phone, patient.City, patient.Location, patient.Birthday_date, patient.@Nationality });

            return result > 0;
        }

        async Task<bool> IPatientRepository.deletePatient(Patient patient)
        {
            var db = dbConnection();

            var query = @"DELETE FROM bd_patients.paciente
                        WHERE id=@Id";

            var result = await db.ExecuteAsync(query, new {Id = patient.Id });

            return result > 0;  
        }
    }
}
