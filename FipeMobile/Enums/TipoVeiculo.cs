namespace FipeMobile.Enums
{
    using System.ComponentModel;

    public enum TipoVeiculo
    {
        [Description("Carros")]
        Carros = 1,
        [Description("Motos")]
        Motos = 2,
        [Description("Caminhões")]
        Caminhoes = 3
    }
}