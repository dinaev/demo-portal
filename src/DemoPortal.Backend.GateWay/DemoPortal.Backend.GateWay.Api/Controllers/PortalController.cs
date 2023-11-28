using System.Net;
using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DemoPortal.Backend.GateWay.Api.Controllers;

public class PortalController : ControllerBase
{
    private const string UserIdClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    
    protected readonly IMapper Mapper;
    private readonly ILogger _logger;

    public PortalController(IMapper mapper, ILogger logger)
    {
        _logger = logger;
        Mapper = mapper;
    }
    
    /// <summary>
    /// Current user ID
    /// </summary>
    protected Guid UserId => Guid.TryParse(User.FindFirst(UserIdClaim)?.Value, out var userGuid) ? userGuid : Guid.Empty;

    private readonly IReadOnlyDictionary<string, int> _errorKeysMap = new Dictionary<string, int>(StandardErrorKeysMap);
        
    private static readonly IReadOnlyDictionary<string, int> StandardErrorKeysMap = new Dictionary<string, HttpStatusCode>
    {
        [DocumentsErrorModelKeys.DocumentNotFound] = HttpStatusCode.NotFound
    }.ToDictionary(x => x.Key, x => (int) x.Value);

    /// <summary>
    /// Renders result as Json and 200 status code if result is successful.
    /// Renders result as Json and with corresponding status code if result has error.
    /// </summary>
    /// <param name="result">Write result</param>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> is successful and result data is null</exception>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> has error and error model is null</exception>
    protected ActionResult RenderResult<TSource, TDestination>(BusinessResult<TSource> result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        if (!result.IsSuccessful)
            return RenderError(result.Error);

        var response = Mapper.Map<TDestination>(result.ResultData);
        return new JsonResult(response);
    }
    
    /// <summary>
    /// Renders result as Json and 200 status code if result is successful.
    /// Renders result as Json and with corresponding status code if result has error.
    /// </summary>
    /// <param name="result">Write result</param>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> is successful and result data is null</exception>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> has error and error model is null</exception>
    protected ActionResult RenderResult<T>(BusinessResult<T> result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        if (!result.IsSuccessful)
            return RenderError(result.Error);

        if (result.ResultData == null)
            throw new ArgumentNullException(nameof(result.ResultData));

        return new JsonResult(result.ResultData);
    }
    
    /// <summary>
    /// Renders result as NoContent response with 204 status code if result is successful.
    /// Renders result as Json and with corresponding status code if result has error.
    /// </summary>
    /// <param name="result">Write result</param>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> is null</exception>
    /// <exception cref="ArgumentNullException">if <paramref name="result"/> has error and error model is null</exception>
    protected ActionResult RenderResult(BusinessResult result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));

        return result.IsSuccessful ? new NoContentResult() : RenderError(result.Error);
    }
    
    /// <summary>
    /// Renders result as Json and with corresponding status code
    /// </summary>
    /// <param name="error">Error model with key and message</param>
    /// <exception cref="ArgumentNullException">If <paramref name="error"/> is null</exception>
    protected ActionResult RenderError(ErrorModel error)
    {
        if (error == null)
            throw new ArgumentNullException(nameof(error));

        _logger.LogError("Key: {errorKey}, Message: {errorMessage}", error.Key, error.Message);

        var problemDetails = new ProblemDetails
        {
            Type = error.Key,
            Detail = error.Message,
            Status = GetErrorStatusCode(error)
        };

        return new JsonResult(problemDetails) {StatusCode = problemDetails.Status};
    }
    
    private int GetErrorStatusCode(ErrorModel errorModel) =>
        _errorKeysMap.TryGetValue(errorModel.Key, out var result) ? result : (int) HttpStatusCode.BadRequest;

    
    
}