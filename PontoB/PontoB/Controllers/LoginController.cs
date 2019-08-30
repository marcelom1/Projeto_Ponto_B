using PontoB.DAO;
using PontoB.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace PontoB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Erro = "";
            return View();
        }

        public ActionResult Autentica(string login, string senha)
        {
            
            ColaboradorDAO dbColaborador = new ColaboradorDAO();
            Colaborador autenticado = dbColaborador.ConfirmacaoAutenticacao(login, senha);
            if (autenticado != null)
            {
               
                FormsAuthentication.SetAuthCookie(autenticado.Email, false);
                if (autenticado.Master)
                {
                    
                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction(Request.QueryString["ReturnUrl"].ToString());
                }
                else
                {
                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("IndexColaborador", "Home");
                    else
                        return RedirectToAction(Request.QueryString["ReturnUrl"].ToString());
                }
            }
            else
            {
                ViewBag.Erro = "Falha no login";
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        [Authorize]
        public ActionResult ModalAlterarSenha()
        {
            return View();
        }
    }
}
