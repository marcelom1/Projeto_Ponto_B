using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PontoB.Models
{
    public class Empresa
    {
        
        public int Id { get;  set; }

        [Required(ErrorMessage = "Razão Social é um campo obrigatório.")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "CNPJ é um campo obrigatório.")]
        public string Cnpj { get;  set; }
        public string NomeFantasia { get;  set; }
        [Required(ErrorMessage = "Endereço é um campo obrigatório.")]
        public virtual Endereco EnderecoEmpresa { get; set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }

        public virtual ICollection<Colaborador> Colaboradores { get; set; }


    }
}