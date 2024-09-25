using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class ProgramCardDto
    {
        [Required]
        public ICollection<FieldDto> Fields { get; set; }

        public class FieldDto
        {
            public string FieldName { get; set; }
            public ICollection<ProgramNamesDto> ProgramNames { get; set; }
        }
        public class ProgramNamesDto
        {
            public string programname { get; set; }
            public ICollection<CheckBoxesDto> CheckBoxes { get; set; }

        }
        public class CheckBoxesDto
        {
            public string ChackBoxName { get; set; }
        }

    }
}