namespace ads.feira.api.Models.Errors
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public DateTime Data { get; set; }
        public string Message { get; set; }

        public ErrorResponse(string id, string requestId)
        {
            Id = id;
            RequestId = requestId;
            Data = DateTime.UtcNow;
            Message = "Erro Inesperado";
        }
    }
}
