using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ResponseModel
{
    public class BaseResposeModel
    {
        [NotMapped]
        public Int64 TotalRecordCount { get; set; }
        [NotMapped]
        public Int64 PageIndex { get; set; }
        [NotMapped]
        public Int64 PageSize { get; set; }

    }

    public class ResponseMessageModel
    {
        [NotMapped]
        [Ignore]
        public Int64 MessageCode { get; set; }

        [NotMapped]
        [Ignore]
        public string? Message { get; set; }
    }
}
