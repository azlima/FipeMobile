namespace FipeMobile.Controllers
{
    using FipeMobile.Enums;
    using FipeMobile.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Mvc;

    public class VeiculoController : Controller
    {
        public IList<Veiculo> veiculos = new List<Veiculo>();

        public ActionResult Index(int tipo, string marcaName, int marcaId, string fipeName)
        {
            ViewBag.Path = string.Format("{0} | {1}", Enum.GetName(typeof(TipoVeiculo), (tipo)).ToUpper(), marcaName.ToUpper());

            Session["marca"] = new Marca() { id = marcaId, name = marcaName, tipo = (TipoVeiculo)tipo, fipe_name = fipeName };

            //Classe de suporte a consumo de HTTP namespace System.Net.Http
            HttpClient client = new HttpClient();
            //Adicionando no Header o token e o media type JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uri = string.Format("http://fipeapi.appspot.com/api/1/{1}/veiculos/{0}.json", marcaId.ToString(), Enum.GetName(typeof(TipoVeiculo), (tipo)).ToLower());
            //Fazendo a requisição
            HttpResponseMessage response = client.GetAsync(uri).Result;
            //Conferindo código 200 de sucesso
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    //Parse de JSON para o objeto Veiculos
                    var resultsCount = response.Content.ReadAsAsync<object[]>().Result.Count();
                    if (resultsCount > 1)
                    {
                        foreach (var veiculo in client.GetAsync(uri).Result.Content.ReadAsAsync<IList<Veiculo>>().Result)
                        {
                            veiculo.Marca = (Marca)Session["marca"];
                            veiculos.Add(veiculo);
                        }
                    }
                    else if (resultsCount == 1)
                    {
                        var veiculo = client.GetAsync(uri).Result.Content.ReadAsAsync<Veiculo>().Result;
                        veiculo.Marca = (Marca)Session["marca"];
                        veiculos.Add(veiculo);
                    }
                }
                catch //(AggregateException aggregateException)
                {
                    return View(veiculos);
                    //throw new Exception(ae.Message);
                }
            }
            else
            {
                //Exibindo a exceção com o código Http respectivo.
                //throw new Exception(string.Concat(response.StatusCode.ToString(), " - ", response.ReasonPhrase));
                return View(veiculos);
            }

            return View(veiculos);
        }
    }
}
