using Microsoft.EntityFrameworkCore;
using static MyUni.Models.Entities.HomeUniCard;

namespace MyUni.Models.Entities
{
    public class HomeUniCard
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string MainText { get; set; }
        public string History { get; set; }
        public string ForPupil { get; set; }
        public string ScholarshipAndFunding { get; set; }
        public string ExchangePrograms { get; set; }
        public string Labs { get; set; }
        public string Jobs { get; set; }
        public string StudentsLife { get; set; }
        public string PaymentMethods { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<Section2> Sections2 { get; set; }
        public ICollection<ArchevitiSavaldebuloSagani> ArchevitiSavaldebuloSaganebi { get; set; }




        public class Event
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
        }

        public class Section
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public ICollection<Programname> ProgramNames { get; set; }
        }
        public class Section2
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public ICollection<SavaldebuloSagnebi> SavaldebuloSagnebi { get; set; }
        }
        public class ArchevitiSavaldebuloSagani
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public ICollection<ArchevitiSavaldebuloSagnebi> ArchevitiSavaldebuloSagnebi { get; set; }
        }

        public class SavaldebuloSagnebi
        {
            public int Id { get; set; }
            public string SagnisSaxeli { get; set; }
            public string Koeficienti { get; set; }
            public string MinimaluriZgvari { get; set; }
            public string Prioriteti { get; set; }
        }
        public class ArchevitiSavaldebuloSagnebi
        {
            public int Id { get; set; }
            public string SagnisSaxeli { get; set; }
            public string Koeficienti { get; set; }
            public string MinimaluriZgvari { get; set; }
            public string Prioriteti { get; set; }
        }
        public class Programname
        {
            public int Id { get; set; }
            public string ProgramName { get; set; }
        }
    }
}