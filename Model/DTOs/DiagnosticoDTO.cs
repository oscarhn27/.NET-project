

namespace Model.DTOs
{
    public class DiagnosticoDTO
    {
        public virtual int IdDiagnostico { get; set; }
        public virtual string ValoracionEspecialista { get; set; }
        public virtual string Enfermedad { get; set; }

        public DiagnosticoDTO() { }

    }
}
