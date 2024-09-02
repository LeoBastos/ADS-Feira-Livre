using System.ComponentModel.DataAnnotations;

namespace ads.feira.domain.Enums.Products
{
    public enum TypeCategoryEnum
    {
        [Display(Name = "Comidas Salgadas")]
        ComidaSalgada,
        
        [Display(Name = "Doces/Bolos")]
        ComidaDoce,
        
        [Display(Name = "Frutas")]
        Frutas,
        
        [Display(Name = "Legumes e Verduras")]
        Legumes,

        [Display(Name = "Embutidos")]
        Embutidos,

        [Display(Name = "Roupas Masculinas")]
        RoupasMasculinas,

        [Display(Name = "Roupas Femininas")]
        RoupasFemininas,

        [Display(Name = "Calçados")]
        Calcados,

        [Display(Name = "Coisas para Casa")]
        CoisasParaCasa,

        [Display(Name = "Conserto de Panelas")]
        Consertos,

        [Display(Name = "Brinquedos")]
        Brinquedos,

        [Display(Name = "Artesanatos")]
        Artesanatos,

        [Display(Name = "Outros")]
        Outros
    }
}
