using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Utils.BaseController;

public class StandardController<TController> : ControllerBase
{
    protected readonly ILogger<TController> _logger;

    protected StandardController(ILogger<TController> logger)
    {
        _logger = logger;
    }

    [NonAction]
    protected ObjectResult Created(object response)
    {
        return StatusCode(StatusCodes.Status201Created, response);
    }

    [NonAction]
    protected ObjectResult NonAuthoritativeInformation(object response)
    {
        return StatusCode(StatusCodes.Status203NonAuthoritative, response);
    }

    [NonAction]
    protected new ObjectResult Unauthorized(object response)
    {
        return StatusCode(StatusCodes.Status401Unauthorized, response);
    }

    [NonAction]
    protected ObjectResult Forbidden(object response)
    {
        return StatusCode(StatusCodes.Status403Forbidden, response);
    }

    [NonAction]
    protected ObjectResult MethodNotAllowed(object response)
    {
        return StatusCode(StatusCodes.Status405MethodNotAllowed, response);
    }

    [NonAction]
    protected ObjectResult NotAcceptable(object response)
    {
        return StatusCode(StatusCodes.Status406NotAcceptable, response);
    }

    [NonAction]
    protected new ObjectResult Conflict(object response)
    {
        return StatusCode(StatusCodes.Status409Conflict, response);
    }

    [NonAction]
    protected ObjectResult PreconditionFailed(object response)
    {
        return StatusCode(StatusCodes.Status412PreconditionFailed, response);
    }

    [NonAction]
    protected ObjectResult RequestEntityTooLarge(object response)
    {
        return StatusCode(StatusCodes.Status413PayloadTooLarge, response);
    }

    [NonAction]
    protected ObjectResult Locked(object response)
    {
        return StatusCode(StatusCodes.Status423Locked, response);
    }

    [NonAction]
    protected ObjectResult TooManyRequests(object response)
    {
        return StatusCode(StatusCodes.Status429TooManyRequests, response);
    }

    [NonAction]
    protected ResponseOperation NewResponseOperation() =>
        new ResponseOperation(Guid.NewGuid(), ControllerActionName());

    private string ControllerActionName()
    {
        try
        {
            return
                $"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return "Controller/Action";
        }
    }

    #region Response204

    [NonAction]
    protected NoContentResult Response204() =>
        NoContent();

    #endregion

    #region Response200

    [NonAction]
    protected OkObjectResult Response200<T>(
        Guid operationId,
        string operationName,
        T data,
        Pagination pagination) =>
        Ok(new Response200<T>(operationId, operationName, data, pagination));

    [NonAction]
    protected OkObjectResult Response200<T>(
        ResponseOperation responseOperation,
        T data,
        Pagination pagination) =>
        Ok(new Response200<T>(responseOperation, data, pagination));

    [NonAction]
    protected OkObjectResult Response200<T>(
        ResponseOperation responseOperation,
        T data) =>
        Ok(new Response200<T>(responseOperation, data));

    #endregion

    #region Response201

    [NonAction]
    protected ObjectResult Response201<T>(
        ResponseOperation responseOperation,
        T data) =>
        Created(new Response201<T>(responseOperation, data));

    [NonAction]
    protected ObjectResult Response201<T>(
        Guid operationId,
        string operationName,
        T data) =>
        Created(new Response201<T>(operationId, operationName, data));

    #endregion

    #region Response202

    [NonAction]
    protected AcceptedResult Response202(
        Guid operationId,
        string operationName,
        string message,
        string id) =>
        Accepted(new Response202(operationId, operationName, message, id));

    [NonAction]
    protected AcceptedResult Response202(
        ResponseOperation responseOperation,
        string message,
        string id) =>
        Accepted(new Response202(responseOperation, message, id));

    [NonAction]
    protected AcceptedResult Response202(
        ResponseOperation responseOperation,
        string message,
        Guid id) =>
        Accepted(new Response202(responseOperation, message, id));

    #endregion

    #region Response203

    [NonAction]
    protected ObjectResult Response203<T>(
        ResponseOperation responseOperation,
        string message,
        T alteration) =>
        NonAuthoritativeInformation(new Response203<T>(responseOperation, message, alteration));

    [NonAction]
    protected ObjectResult Response203<T>(
        Guid operationId,
        string operationName,
        string message,
        T alteration) =>
        NonAuthoritativeInformation(new Response203<T>(operationId, operationName, message, alteration));

    #endregion

    #region Response400

    [NonAction]
    protected BadRequestObjectResult Response400(
        Guid operationId,
        string operationName,
        string message,
        string exception) =>
        BadRequest(new Response400(operationId, operationName, message, exception));

    [NonAction]
    protected BadRequestObjectResult Response400(
        ResponseOperation responseOperation,
        string message,
        string exception) =>
        BadRequest(new Response400(responseOperation, message, exception));

    [NonAction]
    protected BadRequestObjectResult Response400(
        string message,
        string exception) =>
        BadRequest(new Response400(NewResponseOperation(), message, exception));

    #endregion

    #region Response401

    [NonAction]
    protected ObjectResult Response401(
        Guid operationId,
        string operationName,
        string message) =>
        Unauthorized(new Response401(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response401(
        ResponseOperation responseOperation,
        string message) =>
        Unauthorized(new Response401(responseOperation, message));

    [NonAction]
    protected ObjectResult Response401(
        string message) =>
        Unauthorized(new Response401(NewResponseOperation(), message));

    #endregion

    #region Response403

    [NonAction]
    protected ObjectResult Response403(
        Guid operationId,
        string operationName,
        string message) =>
        Forbidden(new Response403(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response403(
        ResponseOperation responseOperation,
        string message) =>
        Forbidden(new Response403(responseOperation, message));

    [NonAction]
    protected ObjectResult Response403(
        string message) =>
        Forbidden(new Response403(NewResponseOperation(), message));

    #endregion

    #region Response404

    [NonAction]
    protected NotFoundObjectResult Response404(
        Guid operationId,
        string operationName,
        string message) =>
        NotFound(new Response404(operationId, operationName, message));

    [NonAction]
    protected NotFoundObjectResult Response404(
        ResponseOperation responseOperation,
        string message) =>
        NotFound(new Response404(responseOperation, message));

    [NonAction]
    protected NotFoundObjectResult Response404(
        string message) =>
        NotFound(new Response404(NewResponseOperation(), message));

    #endregion

    #region Response405

    [NonAction]
    protected ObjectResult Response405(
        Guid operationId,
        string operationName,
        string message) =>
        MethodNotAllowed(new Response405(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response405(
        ResponseOperation responseOperation,
        string message) =>
        MethodNotAllowed(new Response405(responseOperation, message));

    [NonAction]
    protected ObjectResult Response405(
        string message) =>
        MethodNotAllowed(new Response405(NewResponseOperation(), message));

    #endregion

    #region Response406

    [NonAction]
    protected ObjectResult Response406(
        ResponseOperation responseOperation,
        string message,
        IEnumerable<Header> header) =>
        NotAcceptable(new Response406(responseOperation, message, header));

    [NonAction]
    protected ObjectResult Response406(
        Guid operationId,
        string operationName,
        string message,
        IEnumerable<Header> header) =>
        NotAcceptable(new Response406(operationId, operationName, message, header));

    [NonAction]
    protected ObjectResult Response406(
        string message,
        IEnumerable<Header> header) =>
        NotAcceptable(new Response406(NewResponseOperation(), message, header));

    [NonAction]
    protected ObjectResult Response406(
        ResponseOperation responseOperation,
        string message,
        params Header[] header) =>
        Response406(responseOperation, message, header.ToList());

    [NonAction]
    protected ObjectResult Response406(
        Guid operationId,
        string operationName,
        string message,
        params Header[] header) =>
        Response406(operationId, operationName, message, header.ToList());

    [NonAction]
    protected ObjectResult Response406(
        ResponseOperation responseOperation,
        string message) =>
        NotAcceptable(new Response406(responseOperation, message));

    #endregion

    #region Response409

    [NonAction]
    protected ObjectResult Response409(
        Guid operationId,
        string operationName,
        string message) =>
        Conflict(new Response409(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response409(
        ResponseOperation responseOperation,
        string message) =>
        Conflict(new Response409(responseOperation, message));

    [NonAction]
    protected ObjectResult Response409(
        string message) =>
        Conflict(new Response409(NewResponseOperation(), message));

    #endregion

    #region Response412

    [NonAction]
    protected ObjectResult Response412(
        ResponseOperation responseOperation,
        string message,
        IEnumerable<Validation> validation) =>
        PreconditionFailed(new Response412(responseOperation, message, validation));

    [NonAction]
    protected ObjectResult Response412(
        Guid operationId,
        string operationName,
        string message,
        IEnumerable<Validation> validation) =>
        PreconditionFailed(new Response412(operationId, operationName, message, validation));

    [NonAction]
    protected ObjectResult Response412(
        string message,
        IEnumerable<Validation> validation) =>
        PreconditionFailed(new Response412(NewResponseOperation(), message, validation));

    [NonAction]
    protected ObjectResult Response412(
        ResponseOperation responseOperation,
        string message,
        params Validation[] validation) =>
        Response412(responseOperation, message, validation.ToList());

    [NonAction]
    protected ObjectResult Response412(
        Guid operationId,
        string operationName,
        string message,
        params Validation[] validation) =>
        Response412(operationId, operationName, message, validation.ToList());

    #endregion

    #region Response413

    [NonAction]
    protected ObjectResult Response413(
        Guid operationId,
        string operationName,
        string message) =>
        RequestEntityTooLarge(new Response413(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response413(
        ResponseOperation responseOperation,
        string message) =>
        RequestEntityTooLarge(new Response413(responseOperation, message));

    [NonAction]
    protected ObjectResult Response413(
        string message) =>
        RequestEntityTooLarge(new Response413(NewResponseOperation(), message));

    #endregion

    #region Response422
    private void LogWarningResponse422(
        string message,
        IEnumerable<string> error) =>
        _logger.LogWarning("Response422: {MessageResponse422} => {ErrorResponse422}", message, string.Join(" | ", error));

    private void LogWarningResponse422(
        string message,
        IEnumerable<Error> error)
    {
        IEnumerable<string> strError = error.Select(c => $"{c.Field}: {c.Message}");
        _logger.LogWarning("Response422: {MessageResponse422} => {ErrorResponse422}", message, string.Join(" | ", strError));
    }

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        ResponseOperation responseOperation,
        string message,
        IEnumerable<string> error)
    {
        LogWarningResponse422(message, error);
        return UnprocessableEntity(new Response422(responseOperation, message, error));
    }

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        Guid operationId,
        string operationName,
        string message,
        IEnumerable<Error> error)
    {
        LogWarningResponse422(message, error);
        return UnprocessableEntity(new Response422(operationId, operationName, message, error));
    }

    protected UnprocessableEntityObjectResult Response422(
        ResponseOperation responseOperation,
        string message,
        IEnumerable<Error> error)
    {
        LogWarningResponse422(message, error);
        return UnprocessableEntity(new Response422(responseOperation, message, error));
    }

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        Guid operationId,
        string operationName,
        string message,
        IEnumerable<string> error)
    {
        LogWarningResponse422(message, error);
        return UnprocessableEntity(new Response422(operationId, operationName, message, error));
    }

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        string message,
        IEnumerable<string> error)
    {
        LogWarningResponse422(message, error);
        return UnprocessableEntity(new Response422(NewResponseOperation(), message, error));
    }

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        Guid operationId,
        string operationName,
        string message,
        params string[] error) =>
        Response422(operationId, operationName, message, error.ToList());

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        ResponseOperation responseOperation,
        string message,
        params string[] error) =>
        Response422(responseOperation, message, error.ToList());

    [NonAction]
    protected UnprocessableEntityObjectResult Response422(
        string message,
        params string[] error) =>
        Response422(message, error.ToList());

    #endregion

    #region Response423

    [NonAction]
    protected ObjectResult Response423(
        Guid operationId,
        string operationName,
        string message) =>
        Locked(new Response423(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response423(
        ResponseOperation responseOperation,
        string message) =>
        Locked(new Response423(responseOperation, message));

    [NonAction]
    protected ObjectResult Response423(
        string message) =>
        Locked(new Response423(NewResponseOperation(), message));

    #endregion

    #region Response429

    [NonAction]
    protected ObjectResult Response429(
        Guid operationId,
        string operationName,
        string message) =>
        TooManyRequests(new Response429(operationId, operationName, message));

    [NonAction]
    protected ObjectResult Response429(
        ResponseOperation responseOperation,
        string message) =>
        TooManyRequests(new Response429(responseOperation, message));

    [NonAction]
    protected ObjectResult Response429(
        string message) =>
        TooManyRequests(new Response429(NewResponseOperation(), message));

    #endregion
}