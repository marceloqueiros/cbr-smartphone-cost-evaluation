using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modulo2IA_CBR_Telemoveis.Models;

namespace Modulo2IA_CBR_Telemoveis.Controllers
{
    public class HomeController : Controller
    {
        Data D = new Data();
        // GET: Home
        public ActionResult Avaliação()
        {
            ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
            ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
            ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "idResoluçãoEcra", "designação");
            return View();
        }

        [HttpPost]
        public ActionResult Avaliação(FormCollection collection)
        {

            if (string.IsNullOrEmpty(collection["idMarca"]))
                ModelState.AddModelError("idMarca", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["ram"]))
                ModelState.AddModelError("ram", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("ram", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("ram", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["interna"]))
                ModelState.AddModelError("interna", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["interna"]) <= 0)
                        ModelState.AddModelError("interna", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("interna", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Ecra.idResoluçãoEcra"]))
                ModelState.AddModelError("Ecra.idResoluçãoEcra", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["tamanho"]))
                ModelState.AddModelError("tamanho", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["tamanho"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("tamanho", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("tamanho", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["velocidade"]))
                ModelState.AddModelError("velocidade", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["velocidade"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("velocidade", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("velocidade", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["nucleos"]))
                ModelState.AddModelError("nucleos", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["nucleos"]) <= 0)
                        ModelState.AddModelError("nucleos", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("nucleos", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["frontal"]))
                ModelState.AddModelError("frontal", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["frontal"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("frontal", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("frontal", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["traseira"]))
                ModelState.AddModelError("traseira", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["traseira"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("traseira", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("traseira", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["bateria"]))
                ModelState.AddModelError("bateria", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["bateria"]) <= 0)
                        ModelState.AddModelError("bateria", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("bateria", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["idEstado"]))
                ModelState.AddModelError("idEstado", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["idade"]))
                ModelState.AddModelError("idade", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["idade"]) <= 0)
                        ModelState.AddModelError("idade", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("idade", "Valor inválido.");
                }
            }


            ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
            ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
            ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "idResoluçãoEcra", "designação");

            if (ModelState.IsValid)
            {
                D.SetTelemovel(Convert.ToInt32(collection["idMarca"]), Convert.ToInt32(collection["Ecra.idResoluçãoEcra"]), Convert.ToInt32(collection["idEstado"]),
                    Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["interna"]),
                    Convert.ToInt32(collection["bateria"]), Convert.ToDouble(collection["tamanho"], System.Globalization.CultureInfo.InvariantCulture),
                    Convert.ToDouble(collection["velocidade"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["nucleos"]), Convert.ToDouble(collection["frontal"],
                    System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(collection["traseira"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["idade"]));
                D.Avaliação();
                int i = D.DataBase.Telemovels.OrderByDescending(p => p.idTelemovel).ToList().First().idTelemovel;

                ViewBag.Avaliação = "Avaliação: " + D.DataBase.Telemovels.Single(x => x.idTelemovel == i).valorFinal + " €";
                return View("ValorAvaliação", D.DataBase.Telemovels.Single(x => x.idTelemovel == i));
            }
            else
            {
                return View();
            }
        }
        public ActionResult ValorAvaliação()
        {
            return View();
        }
        public ActionResult AdicionarCaso()
        {
            ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
            ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
            ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "idResoluçãoEcra", "designação");
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarCaso(FormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["idMarca"]))
                ModelState.AddModelError("idMarca", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["ram"]))
                ModelState.AddModelError("ram", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("ram", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("ram", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["interna"]))
                ModelState.AddModelError("interna", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["interna"]) <= 0)
                        ModelState.AddModelError("interna", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("interna", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Ecra.idResoluçãoEcra"]))
                ModelState.AddModelError("Ecra.idResoluçãoEcra", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["tamanho"]))
                ModelState.AddModelError("tamanho", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["tamanho"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("tamanho", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("tamanho", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["velocidade"]))
                ModelState.AddModelError("velocidade", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["velocidade"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("velocidade", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("velocidade", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["nucleos"]))
                ModelState.AddModelError("nucleos", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["nucleos"]) <= 0)
                        ModelState.AddModelError("nucleos", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("nucleos", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["frontal"]))
                ModelState.AddModelError("frontal", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["frontal"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("frontal", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("frontal", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["traseira"]))
                ModelState.AddModelError("traseira", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["traseira"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("traseira", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("traseira", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["bateria"]))
                ModelState.AddModelError("bateria", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["bateria"]) <= 0)
                        ModelState.AddModelError("bateria", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("bateria", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["idEstado"]))
                ModelState.AddModelError("idEstado", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["idade"]))
                ModelState.AddModelError("idade", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["idade"]) <= 0)
                        ModelState.AddModelError("idade", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("idade", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["valor"]))
                ModelState.AddModelError("valor", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["valor"]) <= 0)
                        ModelState.AddModelError("valor", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("valor", "Valor inválido.");
                }
            }

            ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
            ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
            ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "IdResoluçãoEcra", "designação");

            if (ModelState.IsValid)
            {
                D.SetTelemovel(Convert.ToInt32(collection["idMarca"]), Convert.ToInt32(collection["Ecra.idResoluçãoEcra"]), Convert.ToInt32(collection["idEstado"]),
                    Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["interna"]),
                    Convert.ToInt32(collection["bateria"]), Convert.ToDouble(collection["tamanho"], System.Globalization.CultureInfo.InvariantCulture),
                    Convert.ToDouble(collection["velocidade"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["nucleos"]), Convert.ToDouble(collection["frontal"],
                    System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(collection["traseira"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["idade"]));
                D.tm.valorFinal = Convert.ToInt32(collection["valor"]);
                D.AdicionarCaso();

                TempData["Caso_Adicionado"] = "Caso adicionado com sucesso!";
                return View();
            }
            else
            {
                return View();
            }
        }

        // GET: Home/Details/5
        public ActionResult ListarCasos()
        {
            return View(D.DataBase.Telemovels);
        }



        // GET: Home/Edit/5
        public ActionResult EditarCaso(int id)
        {
            ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
            ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
            ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "idResoluçãoEcra", "designação");
            return View(D.DataBase.Telemovels.Single(x => x.idTelemovel == id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult EditarCaso(int id, FormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["idMarca"]))
                ModelState.AddModelError("idMarca", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["ram"]))
                ModelState.AddModelError("ram", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("ram", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("ram", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["memoriaInterna"]))
                ModelState.AddModelError("memoriaInterna", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["memoriaInterna"]) <= 0)
                        ModelState.AddModelError("memoriaInterna", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("memoriaInterna", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Ecra.idResoluçãoEcra"]))
                ModelState.AddModelError("Ecra.idResoluçãoEcra", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["Ecra.tamanho"]))
                ModelState.AddModelError("Ecra.tamanho", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["Ecra.tamanho"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("Ecra.tamanho", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("Ecra.tamanho", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Processador.velocidadeProcessador"]))
                ModelState.AddModelError("Processador.velocidadeProcessador", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["Processador.velocidadeProcessador"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("Processador.velocidadeProcessador", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("Processador.velocidadeProcessador", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Processador.nucleosProcessador"]))
                ModelState.AddModelError("Processador.nucleosProcessador", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["Processador.nucleosProcessador"]) <= 0)
                        ModelState.AddModelError("Processador.nucleosProcessador", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("Processador.nucleosProcessador", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Camera.resolucaoFrontal"]))
                ModelState.AddModelError("Camera.resolucaoFrontal", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["Camera.resolucaoFrontal"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("Camera.resolucaoFrontal", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("Camera.resolucaoFrontal", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["Camera.resolucaoTraseira"]))
                ModelState.AddModelError("Camera.resolucaoTraseira", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["Camera.resolucaoTraseira"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                        ModelState.AddModelError("Camera.resolucaoTraseira", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("Camera.resolucaoTraseira", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["mAhBateria"]))
                ModelState.AddModelError("bateria", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToDouble(collection["mAhBateria"]) <= 0)
                        ModelState.AddModelError("mAhBateria", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("mAhBateria", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["idEstado"]))
                ModelState.AddModelError("idEstado", "Falta preencher este ou mais campos!");

            if (string.IsNullOrEmpty(collection["idade"]))
                ModelState.AddModelError("idade", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["idade"]) <= 0)
                        ModelState.AddModelError("idade", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("idade", "Valor inválido.");
                }
            }

            if (string.IsNullOrEmpty(collection["valorFinal"]))
                ModelState.AddModelError("valorFinal", "Falta preencher este ou mais campos!");
            else
            {
                try
                {
                    if (Convert.ToInt32(collection["valorFinal"]) <= 0)
                        ModelState.AddModelError("valorFinal", "Este campo tem de ter valores positivos!");
                }
                catch
                {
                    ModelState.AddModelError("valorFinal", "Valor inválido.");
                }
            }
            if (ModelState.IsValid)
            {
                D.EditarCaso(id, Convert.ToInt32(collection["idMarca"]), Convert.ToInt32(collection["Ecra.idResoluçãoEcra"]), Convert.ToInt32(collection["idEstado"]),
                    Convert.ToDouble(collection["ram"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["memoriaInterna"]),
                    Convert.ToInt32(collection["mAhBateria"]), Convert.ToDouble(collection["Ecra.tamanho"], System.Globalization.CultureInfo.InvariantCulture),
                    Convert.ToDouble(collection["Processador.velocidadeProcessador"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["Processador.nucleosProcessador"]), Convert.ToDouble(collection["Camera.resolucaoFrontal"],
                    System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(collection["Camera.resolucaoTraseira"], System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(collection["idade"]), Convert.ToInt32(collection["valorFinal"]));
                TempData["Caso_Editado"] = "Caso Editado com sucesso!";
                return View("ListarCasos", D.DataBase.Telemovels);
            }
            else
            {
                ViewBag.ListaMarcas = new SelectList(D.DataBase.marcas, "idMarca", "nome");
                ViewBag.ListaEstados = new SelectList(D.DataBase.Estados, "idEstado", "designação");
                ViewBag.ListaRes = new SelectList(D.DataBase.ResoluçãoEcras, "idResoluçãoEcra", "designação");
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult EliminarCaso(int id)
        {
            return View(D.DataBase.Telemovels.Single(x => x.idTelemovel == id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult EliminarCaso(int id, FormCollection C)
        {
            try
            {
                int idecra = D.DataBase.Telemovels.Single(x => x.idTelemovel == id).idEcra;
                int idpro = D.DataBase.Telemovels.Single(x => x.idTelemovel == id).idProcessador;
                int idcam = D.DataBase.Telemovels.Single(x => x.idTelemovel == id).idCameras;
                D.DataBase.Telemovels.DeleteOnSubmit(D.DataBase.Telemovels.Single(x => x.idTelemovel == id));
                D.DataBase.SubmitChanges();

                D.DataBase.Ecras.DeleteOnSubmit(D.DataBase.Ecras.Single(x => x.idEcra == idecra));
                D.DataBase.Cameras.DeleteOnSubmit(D.DataBase.Cameras.Single(x => x.idCameras == idcam));
                D.DataBase.Processadors.DeleteOnSubmit(D.DataBase.Processadors.Single(x => x.idProcessador == idpro));
                D.DataBase.SubmitChanges();
                TempData["Caso_Eliminado"] = "Caso eliminado com sucesso!";
            }
            catch
            {
                TempData["Caso_Não_Eliminado"] = "Erro: Caso não eliminado!";
            }
            return View("ListarCasos", D.DataBase.Telemovels);

        }

        public ActionResult Contactos()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }
    }
}