namespace FipeMobile.Controllers
{
    using FipeMobile.Enums;
    using FipeMobile.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Mvc;

    public class ModeloAnoController : Controller
    {
        public ModeloAno modeloAno = new ModeloAno();

        public ActionResult Index(string modeloName, string modeloId)
        {
            var veiculo = (Veiculo)Session["veiculo"];
            ViewBag.FipeMarca = veiculo.Marca.fipe_name;
            ViewBag.Path = string.Format("{0} | {1}", Enum.GetName(typeof(TipoVeiculo), (veiculo.Marca.tipo)).ToUpper(), veiculo.Marca.name.ToUpper());
            ViewBag.PathSequence = string.Format("{0} | {1}", veiculo.name.ToUpper(), modeloName.ToUpper());

            Session["modelo"] = new Modelo() { id = modeloId, name = modeloName, Veiculo = veiculo };

            HttpClient client = new HttpClient();
            //Adicionando no Header o token e o media type JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uri = string.Format("http://fipeapi.appspot.com/api/1/{3}/veiculo/{0}/{1}/{2}.json", veiculo.Marca.id, veiculo.id, modeloId, Enum.GetName(typeof(TipoVeiculo), (int)veiculo.Marca.tipo));
            //Fazendo a requisição
            HttpResponseMessage response = client.GetAsync(uri).Result;
            //Conferindo código 200 de sucesso
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    //Parse de JSON para o objeto ModeloAno
                    modeloAno = client.GetAsync(uri).Result.Content.ReadAsAsync<ModeloAno>().Result;
                    modeloAno.Modelo = (Modelo)Session["modelo"];
                }
                catch
                {
                    return View(modeloAno);
                }
            }
            else
            {
                //Exibindo a exceção com o código Http respectivo.
                //throw new Exception(string.Concat(response.StatusCode.ToString(), " - ", response.ReasonPhrase));
                return View(modeloAno);
            }
            return View(modeloAno);
        }
    }
}
