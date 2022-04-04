﻿namespace CoreModule.Application.Common.Contracts;

public class Result
{
    internal Result(bool succeeded)
    {
        Succeeded = succeeded;
    }
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }

    public static Result Success()
    {
        return new Result(true);
    }
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
