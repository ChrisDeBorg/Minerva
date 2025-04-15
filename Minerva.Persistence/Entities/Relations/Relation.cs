using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Minerva.Persistence.Entities.Relations
{


    public class Relation : BaseEntity
    {
        public int SourceEntityId { get; set; }
        public int TargetEntityId { get; set; }

        public string RelationType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? Context { get; set; }



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
