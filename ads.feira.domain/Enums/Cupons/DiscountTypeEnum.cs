using System.ComponentModel.DataAnnotations;

namespace ads.feira.domain.Enums.Cupons
{
    public enum DiscountTypeEnum
    {
        [Display(Name = "Valor Fixo")]
        Fixed,

        [Display(Name = "Porcentagem ex: 10%")]
        Percentage,
    }
}
