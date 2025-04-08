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


    public class RelationEntity : BaseEntity
    {
        public int SourceEntityId { get; set; }
        public int TargetEntityId { get; set; }

        public string RelationType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? Context { get; set; }

        // Raw JSON string, stored in DB
        public string? MetaDataJson { get; set; }

        // Optional: compressed or binary serialized
        public byte[]? MetaDataBinary { get; set; }

        // Not mapped: deserialized object for in-app use
        [NotMapped]
        public RelationMeta? Meta
        {
            get => string.IsNullOrEmpty(MetaDataJson)
                ? null
                : JsonSerializer.Deserialize<RelationMeta>(MetaDataJson!);

            set => MetaDataJson = value == null ? null : JsonSerializer.Serialize(value);
        }
    }
}
