namespace EmpleosSur.WebAPI.DTOs
{
    public class EmpleoReadOnlyDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string? Requisitos { get; set; }

        public string Ubicacion { get; set; }

        public decimal Salario { get; set; }

        public DateTime FechaPublicacion { get; set; }
        public EmpresaReadOnlyDTO Empresa { get; set; }
    }
}
