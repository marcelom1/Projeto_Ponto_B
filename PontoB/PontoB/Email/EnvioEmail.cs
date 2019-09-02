using PontoB.DAO.Cadastros;
using PontoB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PontoB.Email
{
    public class EnvioEmail
    {
        private RecuperarSenhaDAO dbRecuperarSenha = new RecuperarSenhaDAO();
        
        public bool EsqueciMinhaSenha(Colaborador colaborador)
        {
            var token = SalvarChave(colaborador);
            
            return SendMail(token,colaborador);
        }

        private string SalvarChave(Colaborador colaborador)
        {
            var token = new RecuperarSenha
            {
                Chave = GerarChave(),
                Colaborador = colaborador,
                ColaboradorId = colaborador.Id,
                Validade = DateTime.Now.AddMinutes(10)
            };
            string chave = token.Chave;

            dbRecuperarSenha.Adiciona(token);
            return chave;
        }

        private string GerarChave()
        {
            int Tamanho = 7; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }

        private bool SendMail(string chave, Colaborador colaborador)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("esqueci.minha.senha.ponto.b@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(colaborador.Email);
                _mailMessage.Subject = "Código de redefinição de senha";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = colaborador.NomeCompleto + " Aqui está o seu código: "+chave +
                                    "<br><br>" +
                                    "Obrigado,<br>"+
                                    "Equipe PontoB";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("esqueci.minha.senha.ponto.b@gmail.com", "emailpontob");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}