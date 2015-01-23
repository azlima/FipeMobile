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

    public class MarcaController : Controller
    {
        public IList<Marca> marcas = new List<Marca>();

        public ActionResult Index()
        {
            //Classe de suporte a consumo de HTTP namespace System.Net.Http
            HttpClient client = new HttpClient();
            //Adicionando no Header o token e o media type JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < 3; i++)
            {
                string tipo = Enum.GetName(typeof(TipoVeiculo), (i + 1));

                string uri = string.Format("http://fipeapi.appspot.com/api/1/{0}/marcas.json", tipo.ToLower());
                //Fazendo a requisição
                HttpResponseMessage response = client.GetAsync(uri).Result;
                //Conferindo código 200 de sucesso
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        //Parse de JSON para o objeto Marcas
                        foreach (var result in response.Content.ReadAsAsync<IList<Marca>>().Result)
                        {
                            result.tipo = (TipoVeiculo)(i + 1);
                            marcas.Add(result);
                        }
                    }
                    catch
                    {
                        return View(marcas);
                    }
                }
                else
                {
                    //Exibindo a exceção com o código Http respectivo.
                    //throw new Exception(string.Concat(response.StatusCode.ToString(), " - ", response.ReasonPhrase));
                    return View(marcas);
                }
            }

            return View(marcas);
        }
    }
}
