namespace Hybrid.Shared
{
    public readonly record struct MethodResult(bool IsSuccess, string? Error)
    {
        public static MethodResult Success() => new(true, null);
        public static MethodResult Fail(string? error) => new(false, error);
    }

    public class MethodResult<TData>
    {
        public MethodResult(bool isSuccess, object value, TData? data)
        {
            IsSuccess = isSuccess;
            Value = value;
            Data = data;
        }

        public bool IsSuccess { get; }
        public object Value { get; }
        public TData? Data { get; }

        public static MethodResult<TData> Success(TData data) => new(true, null!, data);
        public static MethodResult<TData> Fail(string? error) => new(true, error!, default);
    }
}
