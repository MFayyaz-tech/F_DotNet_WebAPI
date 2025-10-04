using System;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.ItemList
{
    [Table("fe_agency")]
    public class Fe_item_list : BaseEntity
    {
        [Key]
        public long List_item_id { get; set; }
        public string List_type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long Display_order { get; set; }
        public string Document_path { get; set; }


    }
}

