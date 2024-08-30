namespace ads.feira.api.Models
{
    public class ResponseModel<TEntity>
    {
        public TEntity? Data { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
