namespace Parcial1_P4_Leanny.Models
{
    public record NumberRecord
    {
        public int Id { get; init; }
        public DateTime Fecha { get; init; }
        public double Numero { get; init; }
        public double Resultado { get; init; }
    }
}
