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

    public class ModeloController : Controller
    {

        public IList<Modelo> modelos = new List<Modelo>();

        public ActionResult Index(string veiculoName, string veiculoId)
        {
            var marca = (Marca)Session["marca"];
            ViewBag.Path = string.Format("{0} | {1}", Enum.GetName(typeof(TipoVeiculo), (marca.tipo)).ToUpper(), marca.name.ToUpper());
            ViewBag.PathSequence = veiculoName.ToUpper();

            Session["veiculo"] = new Veiculo() { id = veiculoId, name = veiculoName, Marca = marca };

            //Classe de suporte a consumo de HTTP namespace System.Net.Http
            HttpClient client = new HttpClient();
            //Adicionando no Header o token e o media type JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uri = string.Format("http://fipeapi.appspot.com/api/1/{2}/veiculo/{0}/{1}.json", marca.id, veiculoId, Enum.GetName(typeof(TipoVeiculo), (int)marca.tipo));
            //Fazendo a requisição
            HttpResponseMessage response = client.GetAsync(uri).Result;
            //Conferindo código 200 de sucesso
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    //Parse de JSON para o objeto Modelos
                    var resultsCount = response.Content.ReadAsAsync<object[]>().Result.Count();
                    if (resultsCount > 1)
                    {
                        foreach (var modelo in client.GetAsync(uri).Result.Content.ReadAsAsync<IList<Modelo>>().Result)
                        {
                            modelo.Veiculo = (Veiculo)Session["veiculo"];
                            modelos.Add(modelo);
                        }
                    }
                    else if (resultsCount == 1)
                    {
                        var modelo = client.GetAsync(uri).Result.Content.ReadAsAsync<Modelo>().Result;
                        modelo.Veiculo = (Veiculo)Session["veiculo"];
                        modelos.Add(modelo);
                    }
                }
                catch
                {
                    return View(modelos);
                }
            }
            else
            {
                //Exibindo a exceção com o código Http respectivo.
                //throw new Exception(string.Concat(response.StatusCode.ToString(), " - ", response.ReasonPhrase));
                return View(modelos);
            }

            return View(modelos);
        }
    }
}
