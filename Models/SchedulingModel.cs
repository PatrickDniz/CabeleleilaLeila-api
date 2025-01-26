namespace Scheduling.Models
{
    public class SchedulingModel
    {
        public int Id { get; set; } 
       
        public string Name { get; private set; }
        
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int Periodos { get; set; } 

          public SchedulingModel(string name, DateTime inicio, DateTime fim, int periodos)
        {
            Name = name;
            Inicio = inicio;
            Fim = fim;
            Periodos = periodos;
        }
        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
