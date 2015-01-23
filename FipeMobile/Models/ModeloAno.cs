namespace FipeMobile.Models
{
    public class ModeloAno
    {
        public string name { get; set; }
        public string referencia { get; set; }
        public string fipe_codigo { get; set; }
        public string preco { get; set; }
        public string veiculo { get; set; }
        public int id { get; set; }
        
        public string ano
        {
            get
            {
                return name.Split(' ')[0];
            }
        }

        public string combustivel
        {
            get
            {
                return name.Split(' ')[1];
            }
        }

        public string key
        {
            get
            {
                return string.Format("{0}-{1}", ano, id.ToString());
            }
        }
        public Modelo Modelo { get; set; }
    }
}