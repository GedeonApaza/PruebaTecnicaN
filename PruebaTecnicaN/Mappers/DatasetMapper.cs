using PruebaTecnicaN.DTOs;
using PruebaTecnicaN.Models;

namespace PruebaTecnicaN.Mappers
{
    public static class DatasetMapper
    {
        public static DatasetsDto ToDto(Datasets dataset)
        {
            return new DatasetsDto
            {
                DataSetID = dataset.DataSetID,
                DataSetName = dataset.DataSetName,
                Description = dataset.Description,
                ProcedureID = dataset.ProcedureID,
                FieldId = dataset.FieldId,
                CreatedDate = dataset.CreatedDate,
                LastModifiedDate = dataset.LastModifiedDate
            };
        }

        public static Datasets ToModel(DatasetsDto datasetDto)
        {
            return new Datasets
            {
                DataSetID = datasetDto.DataSetID,
                DataSetName = datasetDto.DataSetName,
                Description = datasetDto.Description,
                ProcedureID = datasetDto.ProcedureID,
                FieldId = datasetDto.FieldId,
                CreatedDate = datasetDto.CreatedDate,
                LastModifiedDate = datasetDto.LastModifiedDate
            };
        }
    }
}
