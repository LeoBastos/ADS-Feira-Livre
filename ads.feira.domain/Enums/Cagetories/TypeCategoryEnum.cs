using System.ComponentModel.DataAnnotations;

namespace ads.feira.domain.Enums.Products
{
    public enum TypeCategoryEnum
    {
        [Display(Name = "Comida Salgada")]
        ComidaSalgada,
        
        [Display(Name = "Comida Doce")]
        ComidaDoce,
        
        [Display(Name = "Frutas")]
        Frutas,
        
        [Display(Name = "Legumes")]
        Legumes,

        [Display(Name = "Verduras")]
        Verduras,

        [Display(Name = "Embutidos - Ex: Queijo")]
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
