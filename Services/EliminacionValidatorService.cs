namespace ClinicaPrivada.Services
{
    public class EliminacionValidatorService
    {
        private readonly CitaService _citaService;
        private readonly MedicoService _medicoService;

        public EliminacionValidatorService(CitaService citaService, MedicoService medicoService)
        {
            _citaService = citaService;
            _medicoService = medicoService;
        }

        public bool PacientePuedeEliminarse(int pacienteId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.PacienteId == pacienteId);
        }

        public bool MedicoPuedeEliminarse(int medicoId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.MedicoId == medicoId);
        }

        public bool ConsultorioPuedeEliminarse(int consultorioId)
        {
            return !_citaService.ObtenerTodos().Any(c => c.ConsultorioId == consultorioId);
        }

        public bool EspecialidadPuedeEliminarse(int especialidadId)
        {
            return !_medicoService.ObtenerTodos().Any(m => m.EspecialidadId == especialidadId);
        }
    }
}