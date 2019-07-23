using PontoB.DAO;
using PontoB.Models.RegistroPontoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoB.Controllers
{
    [Authorize]
    public class RegistroPontoController : Controller
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RegistroPontoDAO dbRegistroPonto = new RegistroPontoDAO();
        // GET: RegistroPonto
        public ActionResult Index()
        {
            var model = new RegistroPonto();
            model.Colaborador = dbColaborador.BuscarEmail(User.Identity.Name);
            return View(model);
        }

        public DateTime DataHoraAtual()
        {
            return DateTime.Now;
        }
        public bool Registro()
        {
            RegistroPonto registro = new RegistroPonto();
            registro.Colaborador = dbColaborador.BuscarEmail(User.Identity.Name);
            registro.ColaboradorId = registro.Colaborador.Id;
            registro.DataRegistro = DateTime.Now;
            registro.HoraRegistro = registro.DataRegistro.Hour;
            registro.MinutoRegistro = registro.DataRegistro.Minute;
            dbRegistroPonto.Adiciona(registro);
            return true;
        }
    }
}