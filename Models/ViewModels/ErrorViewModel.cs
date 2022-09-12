namespace SalesWebMvc.Models.ViewModels;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    //Atributo abaixo criado para personalizar mensagens de erro
    public string Message { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
