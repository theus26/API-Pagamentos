using API_Produtos.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API_Produtos.Manager.Validate
{
    public class PurchaseValidate :  AbstractValidator<PurchaseProductsDTO>
    {
        public PurchaseValidate()
        {
            RuleFor(p => p.produto_id).GreaterThan(0).WithMessage("Id Produtos não podem ter valor 0.");
            RuleFor(p => p.qtde_comprada).GreaterThan(0).WithMessage("Quantidade de compras deve ser maior que 0.");
            RuleFor(p => p.cartao.titular).NotEmpty().WithMessage("O campo Cartão não pode ser nulo ou vazio.")
                .MinimumLength(3).WithMessage("O campo Cartão deve ter no mínimo 3 caracteres.");
            RuleFor(p => p.cartao.data_expiracao).NotEmpty().WithMessage("O campo Data expiração não pode ser nulo ou vazio.")
                .MinimumLength(3).WithMessage("O campo Data expiração deve ter no mínimo 3 caracteres."); ;
            RuleFor(p => p.cartao.numero).NotEmpty().WithMessage("O campo Numero não pode ser nulo ou vazio.")
                .MinimumLength(3).WithMessage("O campo Numero deve ter no mínimo 3 caracteres."); ;
            RuleFor(p => p.cartao.bandeira).NotEmpty().WithMessage("O campo Bandeira não pode ser nulo ou vazio.")
                .MinimumLength(3).WithMessage("O campo Bandeira deve ter no mínimo 3 caracteres."); ;
            RuleFor(p => p.cartao.cvv).NotEmpty().WithMessage("O campo CVV não pode ser nulo ou vazio.")
                .MinimumLength(3).WithMessage("O campo CVV deve ter no mínimo 3 caracteres.");

            //Usando Regex
            RuleFor(p => p.cartao.numero).Must(BeValidCard).WithMessage("Cartão Inválido");
            RuleFor(p => p.cartao.data_expiracao).Must(BeValidDate).WithMessage("Data Inválida");
            RuleFor(p => p.cartao.bandeira).Must(BeValidString).WithMessage("Bandeira Inválida");
            RuleFor(p => p.cartao.titular).Must(BeValidString).WithMessage("Titular Inválido");
        }

        private bool BeValidCard(string card)
        {
            string pattern = @"^\d{16}$";
            return Regex.IsMatch(card, pattern);
        }
        private bool BeValidDate(string date)
        {
            string pattern = @"^(0[1-9]|1[0-2])\/\d{4}$";
            return Regex.IsMatch(date, pattern);
        }
        private bool BeValidString(string s)
        {
            string pattern = @"[a-zA-Z]+";
            return Regex.IsMatch(s, pattern);  
        }
    }
}
