using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Persistence.Entities.CreativeWorks
{
    public class CreativeWork : BaseEntity
    {
        public String Title {  get; set; }
        public String Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public String Language { get; set; }
        public String License { get; set; }
    }


    public class Film : CreativeWork
    {
        public int Duration { get; set; }
    }

    public class Buch : CreativeWork
    {
        public String ISBN { get; set; }
    }
}

