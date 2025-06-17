using System.Net;
using Lyra.Result;

namespace Lyra
{
    public static class LyraResult
    {
        // success
        public static ILyraResult Ok<T>(T value) => Json(value);
        public static ILyraResult Created<T>(T value) => Json(value, (int)HttpStatusCode.Created);
        public static ILyraResult Accepted() => Empty((int)HttpStatusCode.Accepted);

        // error
        public static ILyraResult BadRequest(string detail = "Bad Request") => BadRequest(new DetailResponse(detail));
        public static ILyraResult BadRequest<T>(T value) => Json(value, (int)HttpStatusCode.BadRequest);
        public static ILyraResult Unauthorized(string detail = "Unauthorized") => Unauthorized(new DetailResponse(detail));
        public static ILyraResult Unauthorized<T>(T value) => Json(value, (int)HttpStatusCode.Unauthorized);
        public static ILyraResult Forbidden(string detail = "Forbidden") => Forbidden(new DetailResponse(detail));
        public static ILyraResult Forbidden<T>(T value) => Json(value, (int)HttpStatusCode.Forbidden);
        public static ILyraResult NotFound(string detail = "Not Found") => NotFound(new DetailResponse(detail));
        public static ILyraResult NotFound<T>(T value) => Json(value, (int)HttpStatusCode.NotFound);
        public static ILyraResult Conflict(string detail = "Conflict") => Conflict(new DetailResponse(detail));
        public static ILyraResult Conflict<T>(T value) => Json(value, (int)HttpStatusCode.Conflict);
        public static ILyraResult InternalServerError(string detail = "Internal Server Error") => InternalServerError(new DetailResponse(detail));
        public static ILyraResult InternalServerError<T>(T value) => Json(value, (int)HttpStatusCode.InternalServerError);

        // utility
        public static ILyraResult Empty(int status = (int)HttpStatusCode.NoContent) => new EmptyResult(status);
        public static ILyraResult Status(int status) => new EmptyResult(status);
        
        // others
        public static ILyraResult Json<T>(T content, int status = (int)HttpStatusCode.OK)
            => new JsonResult<T>(content, status);
        public static ILyraResult Text(string content, int status = (int)HttpStatusCode.OK)
            => new TextResult(content, status);
        public static ILyraResult Redirect(string url, bool permanent = false) => new RedirectResult(url, permanent);
    }
}
