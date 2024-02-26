namespace MultiTenantManagement.Abstractions.Models.Dto.Common
{
    public abstract class ResponseBaseDto
    {
        public IEnumerable<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }
    }

    public abstract class ResponseBaseDto<T>  where T : class
    {
        public T? Attributes { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
}
