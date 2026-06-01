using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ONENET.WebAPI.Common
{
    public class CustomProblemDetails : ProblemDetails
    {
        public new string? Title { get; set; }
        public new int? Status { get; set; }
        public new string? Detail { get; set; }
        public new string? Instance { get; set; }
        public bool Success { get; set; } = false;
        public List<ApiError> Errors { get; set; } = new List<ApiError>();
    }
}