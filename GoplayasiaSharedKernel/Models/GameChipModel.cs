using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class GameChipModel
    {
        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public int? CategoryId { get; set; }
        public int? Chip1 { get; set; }
        public string Chip1Display { get; set; }
        public int? Chip2 { get; set; }
        public string Chip2Display { get; set; }
        public int? Chip3 { get; set; }
        public string Chip3Display { get; set; }
        public int? Chip4 { get; set; }
        public string Chip4Display { get; set; }
        public int? Chip5 { get; set; }
        public string Chip5Display { get; set; }
        public int? Chip6 { get; set; }
        public string Chip6Display { get; set; }
        public int? Chip7 { get; set; }
        public string Chip7Display { get; set; }
        public int? Chip8 { get; set; }
        public string Chip8Display { get; set; }
        public int? Chip9 { get; set; }
        public string Chip9Display { get; set; }
        public int? Chip10 { get; set; }
        public string Chip10Display { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long? CreatedById { get; set; }
        public long? ModifiedById { get; set; }
    }
}
