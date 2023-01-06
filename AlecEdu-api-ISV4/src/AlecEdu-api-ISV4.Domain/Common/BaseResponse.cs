using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Domain.Common;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public EResultCode ResultCode { get; set; }
    public object Message { get; set; }
    public List<string> ValidationErrors { get; set; }
    public T Data { get; set; }

    public BaseResponse()
    {
        Success = true;
        ResultCode = EResultCode.GET;
    }
    
    public BaseResponse(bool success)
    {
        Success = success;
    }

    public BaseResponse(string message)
    {
        Success = true;
        Message = message;
        ResultCode = EResultCode.GET;
    }

    public BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }

    public BaseResponse(string message, bool success, EResultCode resultCode)
    {
        Success = success;
        Message = message;
        ResultCode = resultCode;
    }

    public BaseResponse(T data, string message = null)
    {
        Success = true;
        Message = message;
        ResultCode = EResultCode.GET;
        Data = data;
    }

    public BaseResponse(T data, EResultCode resultCode, string message = null)
    {
        Success = true;
        Message = message;
        ResultCode = resultCode;
        Data = data;
    }
}
