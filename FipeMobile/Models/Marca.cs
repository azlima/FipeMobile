namespace FipeMobile.Models
{
    using Enums;

    public class Marca
    {
        public string name { get; set; }
        public string fipe_name { get; set; }
        public int order { get; set; }
        public int id { get; set; }
        public string key
        {
            get
            {
                return string.Format("{0}-{1}", fipe_name.ToLower(), id.ToString());
            }
        }
        public TipoVeiculo tipo { get; set; }
    }
}