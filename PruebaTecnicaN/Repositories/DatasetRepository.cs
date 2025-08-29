using Microsoft.EntityFrameworkCore;
using PruebaTecnicaN.Data;
using PruebaTecnicaN.DTOs;
using PruebaTecnicaN.Mappers;
using PruebaTecnicaN.Models;

namespace PruebaTecnicaN.Repositories
{
    public class DatasetRepository : IDatasetRepository
    {
        private readonly PruebaTecnicaNContext _context;

        public DatasetRepository(PruebaTecnicaNContext context)
        {
            _context = context;
        }
        // ENDPOINT 1: Obtener DataSets asociados a un UserID específico
        public async Task<List<DatasetsDto>> GetDatasetsByUserIdAsync(int userId)
        {
            var datasets = await _context.Datasets
                .Where(dataset =>
                    dataset.Procedure.CreatedByUserID == userId ||
                    dataset.Procedure.LastModifiedUserID == userId)
                .OrderByDescending(d => d.CreatedDate)
                .ToListAsync();

            return datasets.Select(DatasetMapper.ToDto).ToList();
        }

        // ENDPOINT 2: Validar existencia de Procedure
        public async Task<bool> ProcedureExistsAsync(int procedureId)
        {
            return await _context.Procedures
                .AnyAsync(p => p.ProcedureID == procedureId);
        }


        // ENDPOINT 2: Validar existencia de Field
        public async Task<bool> FieldExistsAsync(int fieldId)
        {
            return await _context.Fields
                .AnyAsync(f => f.FieldID == fieldId);
        }

        // ENDPOINT 2: Crear nuevo DataSet con validaciones
        public async Task<DatasetsDto> CreateDatasetAsync(DatasetsDto createDatasetDto)
        {
            // Validar existencia de Procedure
            var procedureExists = await ProcedureExistsAsync(createDatasetDto.ProcedureID);
            if (!procedureExists)
            {
                throw new ArgumentException($"El Procedure con ID {createDatasetDto.ProcedureID} no existe.");
            }

            // Validar existencia de Field
            var fieldExists = await FieldExistsAsync(createDatasetDto.FieldId);
            if (!fieldExists)
            {
                throw new ArgumentException($"El Field con ID {createDatasetDto.FieldId} no existe.");
            }

            // Crear el DataSet
            var dataset = DatasetMapper.ToModel(createDatasetDto);
            dataset.CreatedDate = DateTime.UtcNow;

            _context.Datasets.Add(dataset);
            await _context.SaveChangesAsync();

            return DatasetMapper.ToDto(dataset);
        }


        // ENDPOINT 3: Obtener DataSets por ProcedureID con tipo de Field
        public async Task<List<DatasetsDto>> GetDatasetsByProcedureIdAsync(int procedureId)
        {
            var datasets = await _context.Datasets
                .Where(dataset => dataset.ProcedureID == procedureId)
                .Include(d => d.Field) // opcional si necesitas más info del campo
                .OrderByDescending(d => d.CreatedDate)
                .ToListAsync();

            return datasets.Select(DatasetMapper.ToDto).ToList();
        }
        // ENDPOINT 4 : Crear un Procedure con autorización de rol Admin 
        public async Task<bool> UserIsAdminAsync(int userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .AnyAsync(ur => ur.UserID == userId && ur.Role.RoleName == "Admin");
        }

        public async Task<ProceduresDto> CreateProcedureAsync(ProceduresDto createProcedureDto)
        {
            // Validar rol Admin
            var isAdmin = await UserIsAdminAsync(createProcedureDto.CreatedByUserID);
            if (!isAdmin)
                throw new UnauthorizedAccessException("Solo usuarios Admin pueden crear procedimientos.");

            var procedure = new Procedures
            {
                ProcedureName = createProcedureDto.ProcedureName,
                Description = createProcedureDto.Description,
                CreatedByUserID = createProcedureDto.CreatedByUserID,
                CreatedDate = DateTime.UtcNow
            };

            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();

            createProcedureDto.ProcedureID = procedure.ProcedureID;
            createProcedureDto.CreatedDate = procedure.CreatedDate;

            return createProcedureDto;
        }

    }
}
