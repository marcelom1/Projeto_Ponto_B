using PontoB.DAO;
using PontoB.Models;
using System.Web.Mvc;
using System.Web.Security;
using PontoB.Email;
using System.Web.Services;
using System.Web.Script.Services;
using PontoB.DAO.Cadastros;
using System.Net;
using Newtonsoft.Json;
using System;

namespace PontoB.Controllers
{
    

    public class LoginController : Controller
    {
        private ColaboradorDAO dbColaborador = new ColaboradorDAO();
        private RecuperarSenhaDAO dbRecuperarSenha = new RecuperarSenhaDAO();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Erro = "";
            return View();
        }

        public ActionResult Autentica(string login, string senha)
        {
            
            
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

        public ActionResult RecuperarSenhaEmail(string email)
        {
            CaptchaReponse response = ValidateCaptcha(Request["g-recaptcha-response"]); 
            if (response.Success)
            {
                var colaborador = dbColaborador.BuscarEmail(email);
                
                if (colaborador != null)
                {
                    var solicitacoes = dbRecuperarSenha.BuscarPorColaborador(colaborador);
                    if (solicitacoes.Count < 3)
                    {
                        new EnvioEmail().EsqueciMinhaSenha(colaborador);
                        foreach (var solcitacao in solicitacoes)
                        {
                            solcitacao.Validade = DateTime.Now;
                            dbRecuperarSenha.Atualiza(solcitacao);
                        }

                    }
                    else
                    {
                        return Json("Para manter sua conta segura, só permitimos que você solicite uma nova senha determinadas vezes por dia. Aguarde 24 horas e tente redefinir sua senha novamente. Além disso, verifique a pasta de spam ou lixo eletrônico do seu email.", JsonRequestBehavior.AllowGet);
                    }
                }
                return PartialView();
               
                    
            }
            return Json("Por favor resolva o captcha!", JsonRequestBehavior.AllowGet);
        }

        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AlterarSenhaViaEmail(string token, string novaSenha, string confirmacaoSenha, string email)
        {
           
                var chave = dbRecuperarSenha.BuscarPorChave(token,email);
                if (chave != null)
                {
                    if (novaSenha.Equals(confirmacaoSenha))
                    {
                        var colaborador = dbColaborador.BuscarPorId(chave.ColaboradorId);
                        colaborador.Senha = novaSenha;
                        dbColaborador.Atualiza(colaborador);
                        dbRecuperarSenha.ExcluirChave(chave);
                        return Json("Senha alterada com sucesso!", JsonRequestBehavior.AllowGet);
                    }
                    return Json("Erro nova senha e a confirmação não são as mesmas", JsonRequestBehavior.AllowGet);
                }

                return Json("Erro token ou email não estão corretos!", JsonRequestBehavior.AllowGet);
           
        }
        
        public static CaptchaReponse ValidateCaptcha(string response)
        {
            string secret = "6Le_N7YUAAAAAKiu_3xKXm3uUtV7P_WFpbB_Qe7d";
            var client = new WebClient();
            var jsonResult = client.DownloadString(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret, response));
            var resposta = JsonConvert.DeserializeObject<CaptchaReponse>(jsonResult.ToString());

            return resposta;
        }


    }
}
