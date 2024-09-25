using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class UniCardDto
    {
        [Required]
        public string url { get; set; }
        [Required]
        public string title { get; set; }
        public string mainText { get; set; }
        public string history { get; set; }
        public string forPupil { get; set; }
        public string scholarshipAndFunding { get; set; }
        public string exchangePrograms { get; set; }
        public string labs { get; set; }
        public string studentsLife { get; set; }
        public string paymentMethods { get; set; }
        public List<EventDto> events { get; set; }
        public List<SectionDto> sections { get; set; }
        public List<Section2Dto> sections2 { get; set; }
        public List<ArchevitiSavaldebuloSaganiDto> archevitiSavaldebuloSaganebi { get; set; }

        public class EventDto
        {
            public string url { get; set; }
            public string title { get; set; }
            public string text { get; set; }
        }

        public class SectionDto
        {
            public string title { get; set; }
            public List<ProgramnameDto> programNames { get; set; }
        }

        public class Section2Dto
        {
            public string title { get; set; }
            public List<SavaldebuloSagnebiDto> savaldebuloSagnebi { get; set; }
        }

        public class ArchevitiSavaldebuloSaganiDto
        {
            public string title { get; set; }
            public List<ArchevitiSavaldebuloSagnebiDto> archevitiSavaldebuloSagnebi { get; set; }
        }

        public class ProgramnameDto
        {
            public string programName { get; set; }
            public string Jobs { get; set; }
            public string SwavlebisEna { get; set; }
            public string Kvalifikacia { get; set; }
            public string Dafinanseba { get; set; }
            public string KreditebisRaodenoba { get; set; }
            public string AdgilebisRaodenoba { get; set; }
            public string Fasi { get; set; }
            public string Kodi { get; set; }
            public string ProgramisAgwera { get; set; }
        }

        public class SavaldebuloSagnebiDto
        {
            public string sagnisSaxeli { get; set; }
            public string koeficienti { get; set; }
            public string minimaluriZgvari { get; set; }
            public string prioriteti { get; set; }
            public string AdgilebisRaodenoba { get; set; }

        }

        public class ArchevitiSavaldebuloSagnebiDto
        {
            public string sagnisSaxeli { get; set; }
            public string koeficienti { get; set; }
            public string minimaluriZgvari { get; set; }
            public string prioriteti { get; set; }
            public string AdgilebisRaodenoba { get; set; }

        }
    }
}

