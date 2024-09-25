namespace MyUni.Models.Entities
{
    public class ProgramCard
    {
        public int Id { get; set; }
        public ICollection<Field> Fields { get; set; }

        public class Field
        {
            public int Id { get; set; }
            public string FieldName { get; set; }
            public ICollection<ProgramNames> ProgramNames { get; set; }
        }
        public class ProgramNames
        {
            public int Id { get; set; }
            public string programname { get; set; }
            public ICollection<CheckBoxes> CheckBoxes { get; set; }

        }
        public class CheckBoxes
        {
            public int Id { get; set; }
            public string ChackBoxName { get; set; }
        }

    }
}
