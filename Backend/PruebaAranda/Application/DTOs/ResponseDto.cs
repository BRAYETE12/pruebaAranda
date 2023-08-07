namespace Application.DTOs
{
    public class ResponseDto
    {
        public bool Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResponseDto(bool status, object data)
        {
            Status = status;
            Data = data;
        }

        public ResponseDto(bool status, String message)
        {
            Status = status;
            Message = message;
        }
    }

}
