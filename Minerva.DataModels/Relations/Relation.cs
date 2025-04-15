using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minerva.DataModels.Relations
{

    public record Relation : BaseRecord
    {
        public int Id { get; init; }
        public int SourceEntityId1 { get; init; }

        public int SourceEntityId { get; init; }
        public int TargetEntityId { get; init; }
        public string RelationType { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? Context { get; init; }
        public string? MetaDataJson { get; init; }
        public byte[]? MetaDataBinary { get; init; }
    }

}
