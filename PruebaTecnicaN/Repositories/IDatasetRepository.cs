using PruebaTecnicaN.DTOs;

namespace PruebaTecnicaN.Repositories
{
    public interface IDatasetRepository
    {

        // ENDPOINT 1: Obtener DataSets por UserID a través de procedimientos
        Task<List<DatasetsDto>> GetDatasetsByUserIdAsync(int userId);

        // ENDPOINT 2: Crear nuevo DataSet con validaciones
        Task<DatasetsDto> CreateDatasetAsync(DatasetsDto createDatasetDto);
        Task<bool> ProcedureExistsAsync(int procedureId);
        Task<bool> FieldExistsAsync(int fieldId);

        // ENDPOINT 3: Obtener DataSets por ProcedureID con tipo de Field
        Task<List<DatasetsDto>> GetDatasetsByProcedureIdAsync(int procedureId);

        // ENDPOINT 4: Crear un nuevo Procedure
        Task<ProceduresDto> CreateProcedureAsync(ProceduresDto createProcedureDto);
        Task<bool> UserIsAdminAsync(int userId); // Para validar rol Admin

    }
}
