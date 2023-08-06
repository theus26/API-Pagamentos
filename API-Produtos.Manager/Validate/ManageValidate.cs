using API_Produtos.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API_Produtos.Manager.Validate
{
    public class ManageValidate : AbstractValidator<ProdutosDTO>
    {
        public ManageValidate()
        {
            RuleFor(P => P.nome).NotEmpty().WithMessage("Produtos não podem ser nulos ou vazios.")
                .MinimumLength(3).WithMessage("Nome do produto deve possuir mais de 3 caracteres.");
            RuleFor(P => P.nome).Must(BeValidName).WithMessage("Por favor insira somente letras");
            RuleFor(P => P.valor_unitario).GreaterThan(0).WithMessage("Produtos não podem ter valor unitario como 0.");
        }

        private bool BeValidName(string name)
        {
            string pattern = @"[a-zA-Z]+";
            return Regex.IsMatch(name, pattern);
        }
    }
}
