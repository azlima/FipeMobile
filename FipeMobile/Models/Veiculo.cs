namespace FipeMobile.Models
{
    public class Veiculo
    {
        public string fipe_marca { get; set; }
        public string name { get; set; }
        public string marca { get; set; }
        public string key { get; set; }
        public string id { get; set; }
        public string fipe_name { get; set; }
        public Marca Marca { get; set; }
    }
}