namespace HospitalVeterinario.Services
{
    public interface INominaService
    {
        decimal CalcularSalarioBruto(decimal salarioBase, string tipoPago);
        decimal CalcularIGSS(decimal salarioBruto);
        decimal CalcularISR(decimal salarioBruto);
        decimal CalcularSalarioNeto(decimal salarioBruto, decimal igss, decimal isr);
    }

    public class NominaService : INominaService
    {
        public decimal CalcularSalarioBruto(decimal salarioBase, string tipoPago)
        {
            if (tipoPago == "Mensual")
                return salarioBase;

            if (tipoPago == "Quincenal")
                return salarioBase / 2;

            if (tipoPago == "Semanal")
                return salarioBase / 4;

            return salarioBase;
        }

        public decimal CalcularIGSS(decimal salarioBruto)
        {
            return salarioBruto * 0.0483m;
        }

        public decimal CalcularISR(decimal salarioBruto)
        {
            if (salarioBruto <= 4000)
                return 0;

            return (salarioBruto - 4000) * 0.05m;
        }

        public decimal CalcularSalarioNeto(decimal salarioBruto, decimal igss, decimal isr)
        {
            return salarioBruto - igss - isr;
        }
    }
}